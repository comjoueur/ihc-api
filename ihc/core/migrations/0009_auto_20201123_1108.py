# Generated by Django 3.1.3 on 2020-11-23 16:08

from django.db import migrations, models


class Migration(migrations.Migration):

    dependencies = [
        ('core', '0008_question'),
    ]

    operations = [
        migrations.AlterField(
            model_name='question',
            name='kind',
            field=models.CharField(choices=[('question_cam', 'Question Cam'), ('question_options', 'Question Options')], max_length=64),
        ),
    ]
