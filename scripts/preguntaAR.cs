using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using NativeWebSocket;
using SimpleJSON;
using UnityEngine.SceneManagement;
using TMPro;
using System.Diagnostics;
using Debug = UnityEngine.Debug;
using System.Security;


[Serializable]
public class ValidateAnswer2
{
    public string action;
    public string token;
    public int questionID;
    public int answer;
}

[Serializable]
public class ExitGroupRequest2
{
    public string action;
    public string token;
}

public class Animalseleccionado
{
    public static int opcion1 = -1;
    public static bool elefanteseleccionado = false;
    public static bool ososeleccionado = false;
    public static bool tigreseleccionado = false;
    public static bool gorilaseleccionado = false;
    public static bool humanseleccionado = false;
    public static bool rhinoseleccionado = false;
    public static bool whaleseleccionado = false;
    public static bool deerseleccionado = false;
    public static bool sharkseleccionado = false;
    public static bool pulposeleccionado = false;
    public static bool owlseleccionado = false;
}

public class preguntaAR : MonoBehaviour
{
    public GameObject elefante;
    public GameObject oso;
    public GameObject tigre;
    public GameObject venado;
    public GameObject correcto;
    public GameObject incorrecto;

    public GameObject player1icon1;//tigre
    public GameObject player1icon2;//oso
    public GameObject player1icon3;//venado
    public GameObject player1icon4;//pulpo
    public GameObject player1icon5;//ballena
    public GameObject player1icon6;//tigre

    public GameObject player2icon1;
    public GameObject player2icon2;
    public GameObject player2icon3;
    public GameObject player2icon4;
    public GameObject player2icon5;
    public GameObject player2icon6;

    public GameObject player3icon1;
    public GameObject player3icon2;
    public GameObject player3icon3;
    public GameObject player3icon4;
    public GameObject player3icon5;
    public GameObject player3icon6;

    public Text namePlayer1;
    public Text namePlayer2;
    public Text namePlayer3;

    public Text suggestPlayer1;
    public Text suggestPlayer2;
    public Text suggestPlayer3;

    int answer;
    string token;
    public Text questionAR;
    int IDquestion;

    int tplayer1;
    int tplayer2;
    int tplayer3;

    // Start is called before the first frame update
    async void Start()
    {
        player1icon1.SetActive(false);
        player1icon2.SetActive(false);
        player1icon3.SetActive(false);
        player1icon4.SetActive(false);
        player1icon5.SetActive(false);
        player1icon6.SetActive(false);

        player2icon1.SetActive(false);
        player2icon2.SetActive(false);
        player2icon3.SetActive(false);
        player2icon4.SetActive(false);
        player2icon5.SetActive(false);
        player2icon6.SetActive(false);

        player3icon1.SetActive(false);
        player3icon2.SetActive(false);
        player3icon3.SetActive(false);
        player3icon4.SetActive(false);
        player3icon5.SetActive(false);
        player3icon6.SetActive(false);

        namePlayer1.text = ConnectWS.player1;
        namePlayer2.text = ConnectWS.player2;
        namePlayer3.text = ConnectWS.player3;

        suggestPlayer1.text = "";
        suggestPlayer2.text = "";
        suggestPlayer3.text = "";

        tplayer1 = 0;
        tplayer2 = 0;
        tplayer3 = 0;

        questionAR.text = ConnectWS.questionAR;
        IDquestion = ConnectWS.queID;
        token = PlayerPrefs.GetString("token");

        ConnectWS.websocket.OnMessage += (bytes) =>
        {
            var message = System.Text.Encoding.UTF8.GetString(bytes);
            Debug.Log(message);
            var messageJSON = JSON.Parse(message);
            if (messageJSON["action"] != null)
            {
                /*if (messageJSON["action"] == "getQuestion")
                {
                    counterQuestion += 1;
                    RestartSuggest();

                    if (messageJSON["kind"] == "question_cam")
                    {
                        questionID = messageJSON["questionID"];
                        queID = questionID;
                        questionAR = messageJSON["question"];
                        var users = messageJSON["users"];
                        player1 = users[0]["username"];
                        player2 = users[1]["username"];
                        player3 = users[2]["username"];
                        SceneManager.LoadScene("MultiPregunta");
                    }
                    else if (messageJSON["kind"] == "question_options")
                    {
                        questionID = messageJSON["questionID"];
                        questionValue.text = messageJSON["question"];
                        optionOne.text = messageJSON["option1"];
                        optionTwo.text = messageJSON["option2"];
                        optionThree.text = messageJSON["option3"];

                        var users = messageJSON["users"];
                        StatePlayer1.transform.GetChild(0).gameObject.GetComponent<Text>().text = users[0]["username"];
                        StatePlayer2.transform.GetChild(0).gameObject.GetComponent<Text>().text = users[1]["username"];
                        StatePlayer3.transform.GetChild(0).gameObject.GetComponent<Text>().text = users[2]["username"];

                        if (token == messageJSON["userToken"])
                        {
                            Send.gameObject.SetActive(true);
                            Suggest.gameObject.SetActive(false);
                        }
                        else
                        {
                            Send.gameObject.SetActive(false);
                            Suggest.gameObject.SetActive(true);
                        }
                    }
                }*/

                if (messageJSON["action"] == "statusAnswer")
                {
                    var users = messageJSON["groupUserAnswers"];

                    for (int i = 0; i < messageJSON["num_groupUserAnswers"]; i++)
                    {
                        if (namePlayer1.text == users[i]["username"])
                        {
                            int caseSwitch = users[i]["answer"];
                            //suggestPlayer1.text = users[i]["answer"];
                            switch (caseSwitch)
                            {
                                case 1:
                                    player1icon1.SetActive(true);
                                    break;
                                case 2:
                                    player1icon2.SetActive(true);
                                    break;
                                case 3:
                                    player1icon3.SetActive(true);
                                    break;
                                case 4:
                                    player1icon4.SetActive(true);
                                    break;
                                case 5:
                                    player1icon5.SetActive(true);
                                    break;
                                case 6:
                                    player1icon6.SetActive(true);
                                    break;
                                default:
                                    break;
                            }
                        }
                        else if (namePlayer2.text == users[i]["username"])
                        {
                            int caseSwitch = users[i]["answer"];
                            //suggestPlayer2.text = users[i]["answer"];
                            switch (caseSwitch)
                            {
                                case 1:
                                    player2icon1.SetActive(true);
                                    break;
                                case 2:
                                    player2icon2.SetActive(true);
                                    break;
                                case 3:
                                    player2icon3.SetActive(true);
                                    break;
                                case 4:
                                    player2icon4.SetActive(true);
                                    break;
                                case 5:
                                    player2icon5.SetActive(true);
                                    break;
                                case 6:
                                    player2icon6.SetActive(true);
                                    break;
                                default:
                                    break;
                            }
                        }
                        else if (namePlayer3.text == users[i]["username"])
                        {
                            int caseSwitch = users[i]["answer"];
                            //suggestPlayer3.text = users[i]["answer"];
                            switch (caseSwitch)
                            {
                                case 1:
                                    player3icon1.SetActive(true);
                                    break;
                                case 2:
                                    player3icon2.SetActive(true);
                                    break;
                                case 3:
                                    player3icon3.SetActive(true);
                                    break;
                                case 4:
                                    player3icon4.SetActive(true);
                                    break;
                                case 5:
                                    player3icon5.SetActive(true);
                                    break;
                                case 6:
                                    player3icon6.SetActive(true);
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    /*
                    users = messageJSON["groupUserState"];

                    for (int i = 0; i < messageJSON["num_groupUserState"]; i++)
                    {
                        if (namePlayer1.text == users[i]["username"])
                        {

                            StatePlayer1.transform.GetChild(1).gameObject.SetActive(false);
                            StatePlayer1.transform.GetChild(2).gameObject.SetActive(true);
                        }
                        else if (namePlayer2.text == users[i]["username"])
                        {
                            StatePlayer2.transform.GetChild(1).gameObject.SetActive(false);
                            StatePlayer2.transform.GetChild(2).gameObject.SetActive(true);
                        }
                        else if (namePlayer3.text == users[i]["username"])
                        {
                            StatePlayer3.transform.GetChild(1).gameObject.SetActive(false);
                            StatePlayer3.transform.GetChild(2).gameObject.SetActive(true);
                        }
                    }*/
                }

                if (messageJSON["action"] == "validateAnswer")
                {
                    //PanelAnswer.SetActive(true);
                    if (messageJSON["valid"] == "correct")
                    {
                        //CorrectAnswer.SetActive(true);
                        correcto.SetActive(false);
                        //incorrecto.SetActive(true);
                    }
                    else
                    {
                        incorrecto.SetActive(true);
                        //IncorrectAnswer.SetActive(true);
                    }

                    StartCoroutine(WaitAnswer(3));
                }
            }
            else
            {
                Debug.Log("Error");
            }
        };
        await ConnectWS.websocket.Connect();
    }

    IEnumerator WaitAnswer(int time)
    {
        yield return new WaitForSeconds(time);
        //PanelAnswer.SetActive(false);
        //IncorrectAnswer.SetActive(false);
        //CorrectAnswer.SetActive(false);
        correcto.SetActive(false);
        incorrecto.SetActive(false);

        /*if (owner && counterQuestion < 2)
        {
            getQuestion();
            RestartSuggest();
            stateAnswer = 0;
        }
        else if (counterQuestion == 2)
        {*/
        ExitGroupRequest2 exitGroup = new ExitGroupRequest2();
        exitGroup.action = "exitGroup";
        exitGroup.token = token;
        SendWebSocketMessage(JsonUtility.ToJson(exitGroup));

        SceneManager.LoadScene("Main Scene");
        //}
    }

    public void SendAnswerAR()
    {
        ValidateAnswer2 validateAnswer = new ValidateAnswer2();
        validateAnswer.action = "validateAnswer";
        validateAnswer.token = token;
        validateAnswer.questionID = IDquestion;
        validateAnswer.answer = answer;
        SendWebSocketMessage(JsonUtility.ToJson(validateAnswer));
    }

    public void PressT(int t)
    {
        if (t == 1)
        {
            tplayer1 += 1;
            switch (tplayer1)
            {
                case 1:
                    player1icon1.SetActive(true);
                    break;
                case 2:
                    player1icon2.SetActive(true);
                    break;
                case 3:
                    player1icon3.SetActive(true);
                    break;
                case 4:
                    player1icon4.SetActive(true);
                    break;
                case 5:
                    player1icon5.SetActive(true);
                    break;
                case 6:
                    player1icon6.SetActive(true);
                    break;
                default:
                    break;
            }
        }
        if (t == 2)
        {
            tplayer2 += 1;
            switch (tplayer2)
            {
                case 1:
                    player2icon1.SetActive(true);
                    break;
                case 2:
                    player2icon2.SetActive(true);
                    break;
                case 3:
                    player2icon3.SetActive(true);
                    break;
                case 4:
                    player2icon4.SetActive(true);
                    break;
                case 5:
                    player2icon5.SetActive(true);
                    break;
                case 6:
                    player2icon6.SetActive(true);
                    break;
                default:
                    break;
            }
        }
        else if (t == 3)
        {
            tplayer3 += 1;
            switch (tplayer3)
            {
                case 1:
                    player3icon1.SetActive(true);
                    break;
                case 2:
                    player3icon2.SetActive(true);
                    break;
                case 3:
                    player3icon3.SetActive(true);
                    break;
                case 4:
                    player3icon4.SetActive(true);
                    break;
                case 5:
                    player3icon5.SetActive(true);
                    break;
                case 6:
                    player3icon6.SetActive(true);
                    break;
                default:
                    break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //public GameObject player1icon1;//tigre
        //public GameObject player1icon2;//oso
        //public GameObject player1icon3;//venado
        //public GameObject player1icon4;//pulpo
        //public GameObject player1icon5;//ballena
        //public GameObject player1icon6;//tigre
        if (Animalseleccionado.elefanteseleccionado == true)
        {
            if (!ConnectWS.QuestionARIsAnswered)
            {
                ConnectWS.QuestionARIsAnswered = true;
                ConnectWS.currentAnswerQuestionAR = 4;
                answer = 4;

                SendAnswerAR();
            }
            player2icon6.SetActive(true);
            correcto.SetActive(true);
            incorrecto.SetActive(false);
        }
        else if (Animalseleccionado.deerseleccionado)
        {
            if (!ConnectWS.QuestionARIsAnswered)
            {
                ConnectWS.QuestionARIsAnswered = true;
                ConnectWS.currentAnswerQuestionAR = 1;

                SendAnswerAR();
            }
            player2icon3.SetActive(true);
            correcto.SetActive(false);
            incorrecto.SetActive(true);
        }
        else if (Animalseleccionado.pulposeleccionado)
        {
            if (!ConnectWS.QuestionARIsAnswered)
            {
                ConnectWS.QuestionARIsAnswered = true;
                ConnectWS.currentAnswerQuestionAR = 1;

                SendAnswerAR();
            }
            player2icon4.SetActive(true);
            correcto.SetActive(false);
            incorrecto.SetActive(true);
        }
        else if (Animalseleccionado.tigreseleccionado)
        {
            if (!ConnectWS.QuestionARIsAnswered)
            {
                ConnectWS.QuestionARIsAnswered = true;
                ConnectWS.currentAnswerQuestionAR = 1;

                SendAnswerAR();
            }
            player2icon1.SetActive(true);
            correcto.SetActive(false);
            incorrecto.SetActive(true);
        }
        else if (Animalseleccionado.ososeleccionado)
        {
            if (!ConnectWS.QuestionARIsAnswered)
            {
                ConnectWS.QuestionARIsAnswered = true;
                ConnectWS.currentAnswerQuestionAR = 1;

                SendAnswerAR();
            }
            player2icon2.SetActive(true);
            correcto.SetActive(false);
            incorrecto.SetActive(true);
        }
        else
        {
            player2icon1.SetActive(false);
            player2icon2.SetActive(false);
            player2icon3.SetActive(false);
            player2icon4.SetActive(false);
            player2icon5.SetActive(false);
            player2icon6.SetActive(false);

            correcto.SetActive(false);
            incorrecto.SetActive(false);
        }

    }

    async void SendWebSocketMessage(string message)
    {
        if (ConnectWS.websocket.State == WebSocketState.Open)
        {
            await ConnectWS.websocket.SendText(message);
        }
    }

}