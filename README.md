# IHC-API

Project make to manage unity colaboration of IHC UCSP course

## Set-up

```sh
$ pip install -r requirements.txt
$ python manage.py migrate
$ python manage.py runserver
```

## Remote Test deployment

```sh
$ pip install -r requirements.txt
$ python manage.py migrate
$ python manage.py runserver 0.0.0.0:8000
```

## Connect Instance EC2

```sh
$ chmod 400 pfc1-keys.pem
$ ssh -i "pfc1-keys.pem" ubuntu@ec2-54-88-50-230.compute-1.amazonaws.com
$ cd ihc-api/
$ source venv/bin/activate
$ git pull origin master
$ python manage.py migrate
$ python manage.py runserver 0.0.0.0:8000
```

## Enlace de la Demostración
[Video YouTube](https://youtu.be/9QsVWaAbdt0) 
