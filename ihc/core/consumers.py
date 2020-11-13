
import json
from channels.generic.websocket import WebsocketConsumer
from ihc.core.models import Client, Group, User
from asgiref.sync import async_to_sync


class UserConsumer(WebsocketConsumer):
    client = None
    group = None

    def connect(self):
        self.client = Client.objects.create(channel_ws=self.channel_name)
        self.accept()

    def disconnect(self, close_code):
        self.client.delete()
        if self.group and self.group.clients.all().count() == 0:
            self.group.delete()

    def receive(self, text_data=None, bytes_data=None):
        data = json.loads(text_data)
        print(data)
        token = data['token']
        self.client.user = User.objects.filter(token=token).first()
        self.client.save()

        if not self.client.user:
            self.send(text_data=json.dumps({'errors': 'User not found'}))
            return

        if data['action'] == 'joinGroup':
            if not data.get('groupName', None):
                group_name = Group.generate_valid_group_name()
                self.group = Group.objects.create(name=group_name)
            else:
                group_name = data['groupName']
                self.group = Group.objects.filter(name=group_name).first()
                if not self.group:
                    self.send(text_data=json.dumps({'errors': 'Group not found'}))
                    return

            async_to_sync(self.channel_layer.group_add)(group_name, self.channel_name)
            self.client.group = self.group
            self.client.save()
            clients = self.group.clients.all()
            user_names = [{
                'name': client.user.fullname,
                'username': client.user.username,
                'id': client.user.pk,
                'token': client.user.token
            } for client in clients]

            async_to_sync(self.channel_layer.group_send)(
                self.group.name,
                {
                    "type": "send.message",
                    "text": json.dumps({
                        'groupName': self.group.name,
                        'action': 'joinGroup',
                        'users': user_names,
                        'num_users': len(user_names)
                    }),
                },
            )
        elif data['action'] == 'exitGroup':
            self.client.group = None
            self.client.save()
            if self.group and self.group.clients.all().count() == 0:
                self.group.delete()

    def send_message(self, text):
        self.send(text_data=text['text'])


class AuthConsumer(WebsocketConsumer):
    def connect(self):
        self.accept()

    def disconnect(self, close_code):
        pass

    def receive(self, text_data=None, bytes_data=None):
        credentials = json.loads(text_data)
        user = User.objects.filter(username=credentials['username'],
                                   password=credentials['password']).first()
        if user:
            self.send(text_data=json.dumps({'token': user.token,
                                            'coins': user.coins,
                                            'id': user.pk}))
        else:
            self.send(text_data=json.dumps({'errors': 'User not found'}))
