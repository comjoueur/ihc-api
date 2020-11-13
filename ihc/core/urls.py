
from django.urls import path
from ihc.core import consumers


urlpatterns = [
]

websocket_urlpatterns = [
    path('user_websocket/', consumers.UserConsumer.as_asgi()),
    path('auth/', consumers.AuthConsumer.as_asgi()),
]
