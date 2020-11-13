
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
