
from django.db import models
import string
import secrets


class Group(models.Model):
    TOKEN_SIZE = 10

    name = models.CharField(max_length=TOKEN_SIZE, unique=True)
    owner = models.ForeignKey('Client',
                              related_name='owner_group',
                              on_delete=models.CASCADE)

    @classmethod
    def generate_valid_group_name(cls):
        alphabet = string.digits
        valid_name = False
        name = None
        while not valid_name:
            name = ''.join(secrets.choice(alphabet) for _ in range(cls.TOKEN_SIZE))
            valid_name = not cls.objects.filter(token=name).exists()
        return name


class Client(models.Model):
    TOKEN_SIZE = 10

    channel_ws = models.CharField(max_length=256)
    token = models.CharField(max_length=TOKEN_SIZE, unique=True)
    group = models.ForeignKey(Group,
                              null=True,
                              blank=True,
                              related_name='clients',
                              on_delete=models.SET_NULL)

    @classmethod
    def generate_valid_client_token(cls):
        alphabet = string.digits
        valid_token = False
        token = None
        while not valid_token:
            token = ''.join(secrets.choice(alphabet) for _ in range(cls.TOKEN_SIZE))
            valid_token = not cls.objects.filter(token=token).exists()
        return token
