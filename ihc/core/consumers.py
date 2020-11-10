
import json
from channels.generic.websocket import WebsocketConsumer
from ihc.core.models import Client, Group
from asgiref.sync import async_to_sync

ACTION = 0
VALUE = 0


class UserConsumer(WebsocketConsumer):
    client = None
    group = None

    def connect(self):
        self.client = Client.objects.create(channel_ws=self.channel_name,
                                            token=Client.generate_valid_client_token())
        self.accept()

    def disconnect(self, close_code):
        if self.group and self.group.owner == self.client:
            self.group.delete()
        self.client.delete()

    def receive(self, text_data=None, bytes_data=None):
        data = text_data.split(':')
        if data[ACTION] == 'create_group':
            group_name = Group.generate_valid_group_name()
            async_to_sync(self.channel_layer.group_add)(group_name, self.channel_name)
            self.group = Group.objects.create(name=group_name,
                                              owner=self.client)
            self.client.group = self.group
            self.client.save()
            self.send(text_data=json.dumps({'group_id': group_name}))
        elif data[ACTION] == 'join_group':
            self.group = Group.objects.filter(name=data[VALUE]).first()
            self.client.group = self.group
            self.client.save()
        elif data[ACTION] == 'exit_group':
            self.client.group = None
            self.client.save()

    def send_message(self, message):
        # async_to_sync(self.channel_layer.send)(self.client.channel_ws, {
        #     'type': 'send_message',
        #     'message': json.dumps(action)
        # })
        self.send(text_data=message['message'])
