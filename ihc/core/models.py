
from django.db import models
import string
import secrets


class User(models.Model):
    TOKEN_SIZE = 10

    username = models.CharField(max_length=128, unique=True)
    password = models.CharField(max_length=128)
    token = models.CharField(max_length=TOKEN_SIZE, unique=True)
    coins = models.IntegerField(default=0)
    fullname = models.CharField(max_length=128, null=True, blank=True)
    unlocked_animals = models.IntegerField(default=0)
    answered_questions = models.IntegerField(default=0)

    @classmethod
    def generate_valid_user_token(cls):
        alphabet = string.digits
        valid_token = False
        token = None
        while not valid_token:
            token = ''.join(secrets.choice(alphabet) for _ in range(cls.TOKEN_SIZE))
            valid_token = not cls.objects.filter(token=token).exists()
        return token


class Group(models.Model):
    TOKEN_SIZE = 10
    GROUP_SIZE = 2

    name = models.CharField(max_length=TOKEN_SIZE, unique=True)

    @classmethod
    def generate_valid_group_name(cls):
        alphabet = string.digits
        valid_name = False
        name = None
        while not valid_name:
            name = ''.join(secrets.choice(alphabet) for _ in range(cls.TOKEN_SIZE))
            valid_name = not cls.objects.filter(name=name).exists()
        return name


class Client(models.Model):
    channel_ws = models.CharField(max_length=256)
    user = models.ForeignKey(User,
                             null=True,
                             blank=True,
                             on_delete=models.SET_NULL,
                             related_name='clients')
    group = models.ForeignKey(Group,
                              null=True,
                              blank=True,
                              related_name='clients',
                              on_delete=models.SET_NULL)
    ready = models.BooleanField(default=False)


class Question(models.Model):

    ANIMAL_ANSWERS = {
        'human': 1,
        'rhino': 2,
        'whale': 3,
        'elephant': 4,
        'tiger': 5,
        'gorilla': 6,
        'deer': 7,
        'bear': 8,
        'shark': 9,
        'octopus': 10,
        'owl': 11,
    }

    QUESTION_KIND_CAM = 'question_cam'
    QUESTION_KIND_OPTIONS = 'question_options'
    KIND_QUESTIONS_CHOICES = [
        (QUESTION_KIND_CAM, 'Question Cam'),
        (QUESTION_KIND_OPTIONS, 'Question Options')
    ]

    value = models.CharField(max_length=512)
    option1 = models.CharField(max_length=256, null=True, blank=True)
    option2 = models.CharField(max_length=256, null=True, blank=True)
    option3 = models.CharField(max_length=256, null=True, blank=True)
    answer = models.IntegerField(default=0)
    kind = models.CharField(max_length=64, choices=KIND_QUESTIONS_CHOICES)

    def validate_answer(self, answer):
        return answer == self.answer

    @classmethod
    def generate_group_questions(cls, group):
        group_users = group.group_users.all()
        questions = Question.objects.order_by('?')[:Group.GROUP_SIZE]
        questions = [question.pk for question in questions]
        for index, group_user in enumerate(group_users):
            group_user.question = Question.objects.get(pk=questions[index])
            group_user.save()


class GroupUser(models.Model):
    user = models.ForeignKey(User,
                             on_delete=models.CASCADE,
                             related_name='group_users')
    group = models.ForeignKey(Group,
                              on_delete=models.CASCADE,
                              related_name='group_users')
    question = models.ForeignKey(Question,
                                 on_delete=models.CASCADE,
                                 null=True,
                                 blank=True,
                                 related_name='group_users')
    wasted = models.BooleanField(default=False)


class AnswerUser(models.Model):
    user = models.ForeignKey(User,
                             on_delete=models.CASCADE,
                             related_name='answer_users')
    group = models.ForeignKey(Group,
                              on_delete=models.CASCADE,
                              related_name='answer_users')
    question = models.ForeignKey(Question,
                                 on_delete=models.CASCADE,
                                 null=True,
                                 blank=True,
                                 related_name='answer_users')
    answer = models.IntegerField(default=0)

    def validate_answer(self):
        return self.question.validate_answer(self.answer)
