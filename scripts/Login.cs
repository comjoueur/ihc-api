using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NativeWebSocket;
using SimpleJSON;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[Serializable]
public class Credentials
{
    public string username;
    public string password;
}

public class Login : MonoBehaviour
{
    public Text usernameText;
    public InputField passwordText;
    public GameObject next;
    public GameObject current;
    WebSocket websocket;

    async void Start()
    {
        websocket = new WebSocket("ws://ec2-54-88-50-230.compute-1.amazonaws.com:8000/auth/");
        websocket.OnOpen += () =>
        {
            Debug.Log("Connection open!");
        };

        websocket.OnError += (e) =>
        {
            Debug.Log("Error! " + e);
        };

        websocket.OnClose += (e) =>
        {
            Debug.Log("Connection closed!");
        };

        websocket.OnMessage += (bytes) =>
        {
            var message = System.Text.Encoding.UTF8.GetString(bytes);
            var tokenJSON = JSON.Parse(message);
            if (tokenJSON["token"] != null)
            {
                string token = tokenJSON["token"];
                PlayerPrefs.SetString("token", token);
                Debug.Log(PlayerPrefs.GetString("token"));
                PlayerPrefs.SetInt("coins", tokenJSON["coins"]);
                websocket.Close();
                Debug.Log("Logged!");
                next.SetActive(true);
                current.SetActive(false);
            }
            else
            {
                Debug.Log("Error");
            }
        };
        await websocket.Connect();
    }

    void Update()
    {
        #if !UNITY_WEBGL || UNITY_EDITOR
                websocket.DispatchMessageQueue();
        #endif
    }

    async void SendWebSocketMessage(string message)
    {
        if (websocket.State == WebSocketState.Open)
        {
            await websocket.SendText(message);
        }
    }

    public void login()
    {
        Credentials credentials = new Credentials();
        credentials.username = usernameText.text;
        credentials.password = passwordText.text;
        SendWebSocketMessage(JsonUtility.ToJson(credentials));
    }

    private async void OnApplicationQuit()
    {
        await websocket.Close();
    }
}
