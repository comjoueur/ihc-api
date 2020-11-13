from django.contrib import admin
from ihc.core.models import Client, Group, User


@admin.register(Client)
class ClientModelAdmin(admin.ModelAdmin):
    pass


@admin.register(Group)
class GroupModelAdmin(admin.ModelAdmin):
    pass


@admin.register(User)
class UserModelAdmin(admin.ModelAdmin):
    pass
