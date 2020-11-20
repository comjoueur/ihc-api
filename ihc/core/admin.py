from django.contrib import admin
from ihc.core.models import Client, Group, User, Question


@admin.register(Client)
class ClientModelAdmin(admin.ModelAdmin):
    pass


@admin.register(Group)
class GroupModelAdmin(admin.ModelAdmin):
    pass


@admin.register(User)
class UserModelAdmin(admin.ModelAdmin):
    pass


@admin.register(Question)
class QuestionModelAdmin(admin.ModelAdmin):
    pass
