using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NativeWebSocket;
using SimpleJSON;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class cambio : MonoBehaviour
{
    WebSocket websocket;
    public GameObject estado1_jugador1;
    public GameObject estado1_jugador3;
    public GameObject estado1_jugador2;
    public GameObject estado2_jugador1;
    public GameObject estado2_jugador3;
    public GameObject estado2_jugador2;

    public GameObject opcion1_jugador1;
    public GameObject opcion1_jugador2;
    public GameObject opcion1_jugador3;
    public GameObject opcion2_jugador1;
    public GameObject opcion2_jugador2;
    public GameObject opcion2_jugador3;
    public GameObject opcion3_jugador1;
    public GameObject opcion3_jugador2;
    public GameObject opcion3_jugador3;

    int seleccion_jugador1=-1;
    int seleccion_jugador3=-1;
    int seleccion_jugador2=-1;

    void respuesta_correcta()
    {
        //verificar respuesta
        estado2_jugador1.SetActive(true);
        estado1_jugador1.SetActive(false);
    }

    void respuesta_incorrecta()
    {
        //verificar respuesta
        estado2_jugador1.SetActive(false);
        estado1_jugador1.SetActive(true);
    }


    public void seleccionar_jugador(int opcion)
    {
        seleccion_jugador1 = opcion;
        //ConnectWS.PressOption(opcion);
        //SendWebSocketMessage(opcion.ToString());
        
        bool correcta;//= verificarRespuestaServer(opcion);
        
        return;
        if (correcta)
        {
            respuesta_correcta();
        }
        else
        {
            respuesta_correcta();
        }
    }
    /*
    public void seleccionar_jugador2(int opcion)
    {
        seleccion_jugador2 = opcion;
    }

    public void seleccionar_jugador3(int opcion)
    {
        seleccion_jugador3 = opcion;
    }/*/

    // Start is called before the first frame update
    void Start()
    {
        seleccion_jugador1 = -1;
        seleccion_jugador2 = -1;
        seleccion_jugador3 = -1;

        estado2_jugador1.SetActive(false);
        estado2_jugador2.SetActive(false);
        estado2_jugador3.SetActive(false);

        opcion1_jugador1.SetActive(false);
        opcion2_jugador1.SetActive(false);
        opcion3_jugador1.SetActive(false);
        opcion1_jugador2.SetActive(false);
        opcion2_jugador2.SetActive(false);
        opcion3_jugador2.SetActive(false);
        opcion1_jugador3.SetActive(false);
        opcion2_jugador3.SetActive(false);
        opcion3_jugador3.SetActive(false);
    }

    void recibimensaje(int player,int option)
    {
        if (player == 2)
        {
            seleccion_jugador2 = option;
        }
        else if (player == 3)
        {
            seleccion_jugador3 = option;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (true)
        {
            int d;
        }
        else if (seleccion_jugador1 == 1)
        {
            opcion1_jugador1.SetActive(true);
            opcion2_jugador1.SetActive(false);
            opcion3_jugador1.SetActive(false);
        }
        else if (seleccion_jugador1 == 2)
        {
            opcion1_jugador1.SetActive(false);
            opcion2_jugador1.SetActive(true);
            opcion3_jugador1.SetActive(false);
        }
        else if (seleccion_jugador1 == 3)
        {
            opcion1_jugador1.SetActive(false);
            opcion2_jugador1.SetActive(false);
            opcion3_jugador1.SetActive(true);
        }
        if (seleccion_jugador2 == 1)
        {
            opcion1_jugador2.SetActive(true);
            opcion2_jugador2.SetActive(false);
            opcion3_jugador2.SetActive(false);
        }
        else if (seleccion_jugador2 == 2)
        {
            opcion1_jugador2.SetActive(false);
            opcion2_jugador2.SetActive(true);
            opcion3_jugador2.SetActive(false);
        }
        else if (seleccion_jugador2 == 3)
        {
            opcion1_jugador2.SetActive(false);
            opcion2_jugador2.SetActive(false);
            opcion3_jugador2.SetActive(true);
        }

        if (seleccion_jugador3 == 1)
        {
            opcion1_jugador3.SetActive(true);
            opcion2_jugador3.SetActive(false);
            opcion3_jugador3.SetActive(false);
        }
        else if (seleccion_jugador3 == 2)
        {
            opcion1_jugador3.SetActive(false);
            opcion2_jugador3.SetActive(true);
            opcion3_jugador3.SetActive(false);
        }
        else if (seleccion_jugador3 == 3)
        {
            opcion1_jugador3.SetActive(false);
            opcion2_jugador3.SetActive(false);
            opcion3_jugador3.SetActive(true);
        }
    }
}
