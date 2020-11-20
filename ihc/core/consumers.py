
import json
from channels.generic.websocket import WebsocketConsumer
from ihc.core.models import Client, Group, User, Question
from asgiref.sync import async_to_sync


class MixinConsumer(WebsocketConsumer):
    client = None
    group = None

    def send_group_message(self, json_body):
        async_to_sync(self.channel_layer.group_send)(
            self.group.name,
            {
                "type": "send.message",
                "text": json.dumps(json_body),
            },
        )

    def send_client_message(self, json_body):
        self.send(text_data=json.dumps(json_body))

    def send_message(self, text):
        self.send(text_data=text['text'])

    def group_disconnect(self):
        if self.group:
            async_to_sync(self.channel_layer.group_discard)(self.group.name, self.channel_name)


class UserConsumer(MixinConsumer):

    def connect(self):
        self.client = Client.objects.create(channel_ws=self.channel_name)
        self.accept()

    def disconnect(self, close_code):
        self.client.delete()
        self.group_disconnect()
        if self.group and self.group.clients.all().count() == 0:
            self.group.delete()

    def receive(self, text_data=None, bytes_data=None):
        data = json.loads(text_data)
        print(data)
        token = data['token']
        self.client.user = User.objects.filter(token=token).first()
        self.client.save()

        if not self.client.user:
            self.send_client_message({'errors': 'User not found'})
            return

        if data['action'] == 'joinGroup':
            if not data.get('groupName', None):
                group_name = Group.generate_valid_group_name()
                self.group = Group.objects.create(name=group_name)
            else:
                group_name = data['groupName']
                self.group = Group.objects.filter(name=group_name).first()
                if not self.group:
                    self.send_client_message({'errors': 'Group not found'})
                    return

            async_to_sync(self.channel_layer.group_add)(group_name, self.channel_name)
            self.client.group = self.group
            self.client.save()
            clients = self.group.clients.all()
            user_names = [{
                'name': client.user.fullname,
                'username': client.user.username,
                'id': client.user.pk,
                'token': client.user.token,
                'unlockedAnimals': client.user.unlocked_animals,
                'answeredQuestions': client.user.answered_questions
            } for client in clients]

            self.send_group_message({
                'groupName': self.group.name,
                'action': 'joinGroup',
                'users': user_names,
                'num_users': len(user_names)
            })

        elif data['action'] == 'exitGroup':
            self.client.group = None
            self.client.save()
            self.group_disconnect()
            if self.group and self.group.clients.all().count() == 0:
                self.group.delete()

        elif data['action'] == 'playerReady':
            self.client.ready = data['ready']
            self.client.save()
            group_clients = self.group.clients.all().filter(ready=True)
            if group_clients.count() == Group.GROUP_SIZE:
                self.send_group_message({
                    'action': 'playerReady',
                    'status': 'continue',
                    'clients': [client.token for client in group_clients],
                    'num_clients': group_clients.count()
                })

        elif data['action'] == 'getQuestion':
            question = Question.objects.order_by('?').first()
            user = self.group.clients.all().order_by('?').first().user
            if question and user:
                if question.kind == Question.QUESTION_KIND_OPTIONS:
                    self.send_group_message({
                        'action': 'getQuestion',
                        'kind': question.kind,
                        'question': question.value,
                        'option1': question.option1,
                        'option2': question.option2,
                        'option3': question.option3,
                        'userName': user.username,
                        'userToken': user.token,
                    })
                elif question.kind == Question.QUESTION_KIND_CAM:
                    self.send_group_message({
                        'action': 'getQuestion',
                        'kind': question.kind,
                        'question': question.value,
                        'questionID': question.pk,
                        'userName': user.username,
                        'userToken': user.token,
                    })

        elif data['action'] == 'validateAnswer':
            question_id = int(data['questionID'])
            question = Question.objects.filter(pk=question_id).first()
            if question and question.validate_answer(data['answer']):
                self.send_client_message({
                    'action': 'validateAnswer',
                    'valid': 'correct'
                })
                self.send_group_message({
                    'action': 'statusAnswer',
                    'valid': 'correct'
                })
            else:
                self.send_client_message({
                    'action': 'validateAnswer',
                    'valid': 'wrong'
                })
                self.send_group_message({
                    'action': 'statusAnswer',
                    'valid': 'wring'
                })


class AuthConsumer(MixinConsumer):
    def connect(self):
        self.accept()

    def disconnect(self, close_code):
        pass

    def receive(self, text_data=None, bytes_data=None):
        credentials = json.loads(text_data)
        user = User.objects.filter(username=credentials['username'],
                                   password=credentials['password']).first()
        if user:
            self.send_client_message({'token': user.token,
                                      'coins': user.coins,
                                      'id': user.pk})
        else:
            self.send_client_message({'errors': 'User not found'})
