# Generated by Django 3.1.3 on 2020-11-13 19:43

from django.db import migrations, models


class Migration(migrations.Migration):

    dependencies = [
        ('core', '0005_remove_group_owner'),
    ]

    operations = [
        migrations.AddField(
            model_name='user',
            name='answered_questions',
            field=models.IntegerField(default=0),
        ),
        migrations.AddField(
            model_name='user',
            name='unlocked_animals',
            field=models.IntegerField(default=0),
        ),
    ]