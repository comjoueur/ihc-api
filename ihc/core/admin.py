from django.contrib import admin
from ihc.core.models import Client, Group


@admin.register(Client)
class ClientModelAdmin(admin.ModelAdmin):
    pass


@admin.register(Group)
class GroupModelAdmin(admin.ModelAdmin):
    pass
