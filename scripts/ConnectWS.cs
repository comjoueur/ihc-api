using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NativeWebSocket;
using SimpleJSON;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Globalization;
using System.Diagnostics;
using Debug = UnityEngine.Debug;
using System.Security;
using System.Runtime.CompilerServices;

[Serializable]
public class GeneralRequest
{
    public string action;
    public string token;
}


[Serializable]
public class JoinGroupRequest
{
    public string action;
    public string token;
    public string groupName;
}


[Serializable]
public class ExitGroupRequest
{
    public string action;
    public string token;
}


[Serializable]
public class StatusRequest
{
    public string action;
    public string token;
    public bool ready;
}


[Serializable]
public class GetQuestion
{
    public string action;
    public string token;
}


[Serializable]
public class ValidateAnswer
{
    public string action;
    public string token;
    public int questionID;
    public int answer;
}


public class Animalseleccionasdo
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

public class ConnectWS : MonoBehaviour
{
    public static WebSocket websocket;
    public static bool cambio = false;
    public static int opcion_mia_de_mi = -1;
    int opcion_ar_jugador1 = -1;
    int opcion_ar_jugador2 = -1;
    int opcion_ar_jugador3 = -1;

    int SIZE_QUESTIONS = 9;

    bool multipregunta;

    public static int queID;
    public static string questionAR;
    public static string player1;
    public static string player2;
    public static string player3;

    public static bool QuestionARCorrectAnswered = false;
    public static bool QuestionARIsAnswered = false;
    public static int currentAnswerQuestionAR;

    string token;
    bool owner;
    int counterQuestion;
    int stateAnswer;

    public Text user1Name;
    public Text user1Description;

    public Text user2Name;
    public Text user2Description;

    public Text user3Name;
    public Text user3Description;

    public Text groupCode;

    public Text groupNameInput;

    public Text questionValue;

    int questionID;

    public Text AR_nombre1;
    public Text AR_nombre2;
    public Text AR_nombre3;

    public Text optionOne;
    public Text optionTwo;
    public Text optionThree;

    public Button Send;
    public Button Suggest;

    public GameObject Environment;

    public GameObject PreguntaAR1;
    public GameObject PreguntaAR2;
    public GameObject MainCanvas;
    public GameObject MultiPreguntas;

    public GameObject createJoinMock;
    public GameObject waitGroupMock;
    public GameObject QuestionGroupMock;

    public GameObject Ready;
    public GameObject Cancel;

    public GameObject PanelAnswer;
    public GameObject CorrectAnswer;
    public GameObject IncorrectAnswer;

    public GameObject PanelAnswerAR;
    public GameObject CorrectAnswerAR;
    public GameObject IncorrectAnswerAR;

    public GameObject StatePlayer1;
    public GameObject StatePlayer2;
    public GameObject StatePlayer3;

    public GameObject ScoreCanvas;
    public Text ScoreName1;
    public Text ScoreName2;
    public Text ScoreName3;

    public Text ScorePoint1;
    public Text ScorePoint2;
    public Text ScorePoint3;

    public Text RewardName1;
    public Text RewardName2;
    public Text RewardName3;

    public Text RewardPoint1;
    public Text RewardPoint2;
    public Text RewardPoint3;

    public Text PreguntaAR;

    public GameObject TurnPlayer1;
    public GameObject TurnPlayer2;
    public GameObject TurnPlayer3;

    //public GameObject[] StatePlayer;

    public GameObject SuggestOption1;
    public GameObject SuggestOption2;
    public GameObject SuggestOption3;

    public Image TimeBarOption;
    public Image TimeBarAR;
    float MaxTimeOption;
    float TimeLeftOption;
    float MaxTimeAR;
    float TimeLeftAR;
    bool isAR;
    bool isOption;

    public GameObject player1icon1;//tigre
    public GameObject player1icon2;//oso
    public GameObject player1icon3;//venado
    public GameObject player1icon4;//pulpo
    public GameObject player1icon5;//ballena
    public GameObject player1icon6;//tigre

    public GameObject player2icon1;//tigre
    public GameObject player2icon2;//oso
    public GameObject player2icon3;//venado
    public GameObject player2icon4;//pulpo
    public GameObject player2icon5;//ballena
    public GameObject player2icon6;//tigre

    public GameObject player3icon1;//tigre
    public GameObject player3icon2;//oso
    public GameObject player3icon3;//venado
    public GameObject player3icon4;//pulpo
    public GameObject player3icon5;//ballena
    public GameObject player3icon6;//tigre

    string tempGroupName;

    void RestartSuggest()
    {
        StatePlayer1.transform.GetChild(1).gameObject.SetActive(true);
        StatePlayer1.transform.GetChild(2).gameObject.SetActive(false);
        StatePlayer2.transform.GetChild(1).gameObject.SetActive(true);
        StatePlayer2.transform.GetChild(2).gameObject.SetActive(false);
        StatePlayer3.transform.GetChild(1).gameObject.SetActive(true);
        StatePlayer3.transform.GetChild(2).gameObject.SetActive(false);

        SuggestOption1.transform.GetChild(1).gameObject.SetActive(false);
        SuggestOption1.transform.GetChild(2).gameObject.SetActive(false);
        SuggestOption1.transform.GetChild(3).gameObject.SetActive(false);
        SuggestOption2.transform.GetChild(1).gameObject.SetActive(false);
        SuggestOption2.transform.GetChild(2).gameObject.SetActive(false);
        SuggestOption2.transform.GetChild(3).gameObject.SetActive(false);
        SuggestOption3.transform.GetChild(1).gameObject.SetActive(false);
        SuggestOption3.transform.GetChild(2).gameObject.SetActive(false);
        SuggestOption3.transform.GetChild(3).gameObject.SetActive(false);
    }

    async void Start()
    {
        Debug.Log("general connection performing...");

        MaxTimeOption = 10f;
        TimeLeftOption = MaxTimeOption;
        MaxTimeAR = 10f;
        TimeLeftAR = MaxTimeAR;

        isOption = false;
        isAR = false;

        user1Name.text = "";
        user2Name.text = "";
        user3Name.text = "";
        user1Description.text = "";
        user2Description.text = "";
        user3Description.text = "";
        questionValue.text = "";
        optionOne.text = "";
        optionTwo.text = "";
        optionThree.text = "";

        ScoreName1.text = "";
        ScoreName2.text = "";
        ScoreName3.text = "";

        ScorePoint1.text = "2";
        ScorePoint2.text = "2";
        ScorePoint3.text = "1";

        RewardName1.text = "";
        RewardName2.text = "";
        RewardName3.text = "";

        RewardPoint1.text = "30";
        RewardPoint2.text = "30";
        RewardPoint3.text = "15";

        StatePlayer1.transform.GetChild(0).gameObject.GetComponent<Text>().text = "";
        StatePlayer2.transform.GetChild(0).gameObject.GetComponent<Text>().text = "";
        StatePlayer3.transform.GetChild(0).gameObject.GetComponent<Text>().text = "";

        RestartSuggest();

        Send.interactable = false;
        Send.gameObject.SetActive(false);
        Suggest.interactable = false;
        Suggest.gameObject.SetActive(false);
        //PanelBlocked.SetActive(false);

        owner = false;
        stateAnswer = -1;
        counterQuestion = 0;

        websocket = new WebSocket("ws://ec2-54-88-50-230.compute-1.amazonaws.com:8000/user_websocket/");//sudo kill -9 $(sudo lsof -t -i:8000)

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
            Debug.Log(message);
            var messageJSON = JSON.Parse(message);
            if (messageJSON["action"] != null)
            {
                if (messageJSON["action"] == "joinGroup")
                {
                    tempGroupName = messageJSON["groupName"];
                    var users = messageJSON["users"];
                    for (int i = 0; i < messageJSON["num_users"]; i++)
                    {
                        if (i == 0)
                        {
                            user1Name.text = users[0]["username"];
                            user1Description.text = "Animales desbloqueados " + users[0]["unlockedAnimals"] + "\nPregunta Respuestas " + users[0]["answeredQuestions"];
                            ScoreName1.text = users[0]["username"];
                            RewardName1.text = users[0]["username"];
                        }
                        if (i == 1)
                        {
                            user2Name.text = users[1]["username"];
                            user2Description.text = "Animales desbloqueados " + users[1]["unlockedAnimals"] + "\nPregunta Respuestas " + users[1]["answeredQuestions"];
                            ScoreName2.text = users[1]["username"];
                            RewardName2.text = users[1]["username"];
                        }
                        if (i == 2)
                        {
                            user3Name.text = users[2]["username"];
                            user3Description.text = "Animales desbloqueados " + users[2]["unlockedAnimals"] + "\nPregunta Respuestas " + users[2]["answeredQuestions"];
                            ScoreName3.text = users[2]["username"];
                            RewardName3.text = users[2]["username"];
                        }
                    }
                    groupCode.text = messageJSON["groupName"];
                    createJoinMock.SetActive(false);
                    waitGroupMock.SetActive(true);
                }

                if (messageJSON["action"] == "playerReady")
                {
                    if (owner)
                    {
                        getQuestion();
                    }
                    waitGroupMock.SetActive(false);
                    QuestionGroupMock.SetActive(true);
                }

                if (messageJSON["action"] == "getQuestion")
                {
                    counterQuestion += 1;
                    user1Name.text = "";
                    user2Name.text = "";
                    user3Name.text = "";
                    user1Description.text = "";
                    user2Description.text = "";
                    user3Description.text = "";
                    Ready.SetActive(true);
                    Cancel.SetActive(false);
                    opcion_ar_jugador1 = -1;
                    opcion_ar_jugador2 = -1;
                    opcion_ar_jugador3 = -1;
                    reseticons();
                    RestartSuggest();

                    if (messageJSON["kind"] == "question_cam")
                    {
                        isAR = true;
                        isOption = false;
                        TimeLeftAR = MaxTimeAR;
                        multipregunta = false;
                        PreguntaAR1.SetActive(true);
                        PreguntaAR2.SetActive(true);
                        MultiPreguntas.SetActive(false);
                        MainCanvas.SetActive(false);

                        questionID = messageJSON["questionID"];
                        queID = questionID;
                        PreguntaAR.text = messageJSON["question"];

                        var users = messageJSON["users"];
                        AR_nombre1.text = users[0]["username"];
                        AR_nombre2.text = users[1]["username"];
                        AR_nombre3.text = users[2]["username"];

                        if (messageJSON["userName"] == AR_nombre1.text)
                        {
                            TurnPlayer1.SetActive(true);
                            TurnPlayer2.SetActive(false);
                            TurnPlayer3.SetActive(false);
                        }
                        else if (messageJSON["userName"] == AR_nombre2.text)
                        {
                            TurnPlayer1.SetActive(false);
                            TurnPlayer2.SetActive(true);
                            TurnPlayer3.SetActive(false);
                        }
                        else if (messageJSON["userName"] == AR_nombre3.text)
                        {
                            TurnPlayer1.SetActive(false);
                            TurnPlayer2.SetActive(false);
                            TurnPlayer3.SetActive(true);
                        }
                    }
                    else if (messageJSON["kind"] == "question_options")
                    {
                        isOption = true;
                        isAR = false;
                        TimeLeftOption = MaxTimeOption;
                        multipregunta = true;
                        MainCanvas.SetActive(false);
                        PreguntaAR1.SetActive(false);
                        PreguntaAR2.SetActive(false);
                        MultiPreguntas.SetActive(true);

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
                }

                if (messageJSON["action"] == "statusAnswer")
                {
                    Debug.Log("status");
                    var users = messageJSON["groupUserAnswers"];
                    if (multipregunta)
                    {
                        //                        RestartSuggest();
                        for (int i = 0; i < messageJSON["num_groupUserAnswers"]; i++)
                        {
                            if (StatePlayer1.transform.GetChild(0).gameObject.GetComponent<Text>().text == users[i]["username"])
                            {
                                if (users[i]["answer"] == 1)
                                {
                                    SuggestOption1.transform.GetChild(1).gameObject.SetActive(true);
                                    SuggestOption2.transform.GetChild(1).gameObject.SetActive(false);
                                    SuggestOption3.transform.GetChild(1).gameObject.SetActive(false);
                                }
                                if (users[i]["answer"] == 2)
                                {
                                    SuggestOption1.transform.GetChild(1).gameObject.SetActive(false);
                                    SuggestOption2.transform.GetChild(1).gameObject.SetActive(true);
                                    SuggestOption3.transform.GetChild(1).gameObject.SetActive(false);
                                }
                                if (users[i]["answer"] == 3)
                                {
                                    SuggestOption1.transform.GetChild(1).gameObject.SetActive(false);
                                    SuggestOption2.transform.GetChild(1).gameObject.SetActive(false);
                                    SuggestOption3.transform.GetChild(1).gameObject.SetActive(true);
                                }
                            }
                            else if (StatePlayer2.transform.GetChild(0).gameObject.GetComponent<Text>().text == users[i]["username"])
                            {
                                if (users[i]["answer"] == 1)
                                {
                                    SuggestOption1.transform.GetChild(2).gameObject.SetActive(true);
                                    SuggestOption2.transform.GetChild(2).gameObject.SetActive(false);
                                    SuggestOption3.transform.GetChild(2).gameObject.SetActive(false);
                                }
                                if (users[i]["answer"] == 2)
                                {
                                    SuggestOption1.transform.GetChild(2).gameObject.SetActive(false);
                                    SuggestOption2.transform.GetChild(2).gameObject.SetActive(true);
                                    SuggestOption3.transform.GetChild(2).gameObject.SetActive(false);
                                }
                                if (users[i]["answer"] == 3)
                                {
                                    SuggestOption1.transform.GetChild(2).gameObject.SetActive(false);
                                    SuggestOption2.transform.GetChild(2).gameObject.SetActive(false);
                                    SuggestOption3.transform.GetChild(2).gameObject.SetActive(true);
                                }
                            }
                            else if (StatePlayer3.transform.GetChild(0).gameObject.GetComponent<Text>().text == users[i]["username"])
                            {
                                if (users[i]["answer"] == 1)
                                {
                                    SuggestOption1.transform.GetChild(3).gameObject.SetActive(true);
                                    SuggestOption2.transform.GetChild(3).gameObject.SetActive(false);
                                    SuggestOption3.transform.GetChild(3).gameObject.SetActive(false);
                                }
                                if (users[i]["answer"] == 2)
                                {
                                    SuggestOption1.transform.GetChild(3).gameObject.SetActive(false);
                                    SuggestOption2.transform.GetChild(3).gameObject.SetActive(true);
                                    SuggestOption3.transform.GetChild(3).gameObject.SetActive(false);
                                }
                                if (users[i]["answer"] == 3)
                                {
                                    SuggestOption1.transform.GetChild(3).gameObject.SetActive(false);
                                    SuggestOption2.transform.GetChild(3).gameObject.SetActive(false);
                                    SuggestOption3.transform.GetChild(3).gameObject.SetActive(true);
                                }
                            }
                        }

                        users = messageJSON["groupUserState"];

                        for (int i = 0; i < messageJSON["num_groupUserState"]; i++)
                        {
                            if (StatePlayer1.transform.GetChild(0).gameObject.GetComponent<Text>().text == users[i]["username"])
                            {
                                if (users[i]["answered"] == 1 || users[i]["answered"] == "True")
                                {
                                    StatePlayer1.transform.GetChild(1).gameObject.SetActive(false);
                                    StatePlayer1.transform.GetChild(2).gameObject.SetActive(true);
                                }
                                else
                                {
                                    StatePlayer1.transform.GetChild(1).gameObject.SetActive(true);
                                    StatePlayer1.transform.GetChild(2).gameObject.SetActive(false);
                                }
                            }
                            else if (StatePlayer2.transform.GetChild(0).gameObject.GetComponent<Text>().text == users[i]["username"])
                            {
                                if (users[i]["answered"] == 1 || users[i]["answered"] == "True")
                                {
                                    StatePlayer2.transform.GetChild(1).gameObject.SetActive(false);
                                    StatePlayer2.transform.GetChild(2).gameObject.SetActive(true);
                                }
                                else
                                {
                                    StatePlayer2.transform.GetChild(1).gameObject.SetActive(true);
                                    StatePlayer2.transform.GetChild(2).gameObject.SetActive(false);
                                }
                            }
                            else if (StatePlayer3.transform.GetChild(0).gameObject.GetComponent<Text>().text == users[i]["username"])
                            {
                                if (users[i]["answered"] == 1 || users[i]["answered"] == "True")
                                {
                                    StatePlayer3.transform.GetChild(1).gameObject.SetActive(false);
                                    StatePlayer3.transform.GetChild(2).gameObject.SetActive(true);
                                }
                                else
                                {
                                    StatePlayer3.transform.GetChild(1).gameObject.SetActive(true);
                                    StatePlayer3.transform.GetChild(2).gameObject.SetActive(false);
                                }
                            }
                        }
                    }
                    else
                    {
                        Debug.Log("no multi");
                        opcion_ar_jugador1 = -1;
                        opcion_ar_jugador2 = -1;
                        opcion_ar_jugador3 = -1;
                        for (int i = 0; i < messageJSON["num_groupUserAnswers"]; i++)
                        {
                            Debug.Log(AR_nombre1.text);
                            Debug.Log(users[i]["username"]);
                            if (AR_nombre1.text == users[i]["username"])
                            {
                                int caseSwitch = users[i]["answer"];
                                opcion_ar_jugador1 = caseSwitch;
                                Debug.Log(opcion_ar_jugador1);
                                Debug.Log(opcion_ar_jugador2);
                                Debug.Log(opcion_ar_jugador3);
                            }
                            else if (AR_nombre2.text == users[i]["username"])
                            {
                                int caseSwitch = users[i]["answer"];
                                Debug.Log("Soy persona 2");
                                opcion_ar_jugador2 = caseSwitch;
                            }
                            else if (AR_nombre3.text == users[i]["username"])
                            {
                                int caseSwitch = users[i]["answer"];
                                Debug.Log("Soy persona 3");
                                opcion_ar_jugador3 = caseSwitch;
                            }
                        }

                    }
                }

                if (messageJSON["action"] == "validateAnswer")
                {
                    if (multipregunta)
                    {
                        //OPTION
                        PanelAnswer.SetActive(true);
                        if (messageJSON["valid"] == "correct")
                        {
                            CorrectAnswer.SetActive(true);
                        }
                        else
                        {
                            IncorrectAnswer.SetActive(true);
                        }
                        Debug.Log("antes del wait");
                        StartCoroutine(WaitAnswerOption(3));
                    }
                    else
                    {
                        // AR
                        PanelAnswerAR.SetActive(true);
                        if (messageJSON["valid"] == "correct")
                        {
                            CorrectAnswerAR.SetActive(true);
                        }
                        else
                        {
                            IncorrectAnswerAR.SetActive(true);
                        }
                        Debug.Log("antes del wait");
                        StartCoroutine(WaitAnswerAR(3));
                    }

                }
            }
            else
            {
                Debug.Log("Error");
            }
        };
        await websocket.Connect();
    }

    IEnumerator WaitAnswerOption(int time)
    {
        Debug.Log("dentro del wait");
        
        yield return new WaitForSecondsRealtime(time);
        Debug.Log("gdfgdf");
        PanelAnswer.SetActive(false);
        Debug.Log("saf");
        IncorrectAnswer.SetActive(false);
        CorrectAnswer.SetActive(false);
        Debug.Log("antes de restart");
        RestartSuggest();
        Debug.Log("antes del if");
        stateAnswer = -1;
        if (owner && counterQuestion < SIZE_QUESTIONS)
        {
            Debug.Log("debe solicitar");
            getQuestion();
        }
        else if (counterQuestion == SIZE_QUESTIONS)
        {
            RestartSuggest();
            PreguntaAR1.SetActive(false);
            PreguntaAR2.SetActive(false);
            MultiPreguntas.SetActive(false);
            counterQuestion = 0;
            ExitGroupRequest exitGroup = new ExitGroupRequest();
            exitGroup.action = "exitGroup";
            exitGroup.token = token;
            SendWebSocketMessage(JsonUtility.ToJson(exitGroup));

            ScoreCanvas.SetActive(true);
            Coins.coins += 60;
            Global.count += 60;
            //Environment.SetActive(true);
            QuestionGroupMock.SetActive(false);
        }
        else
        {
            Debug.Log("No entra");
        }
    }

    IEnumerator WaitAnswerAR(int time)
    {
        Debug.Log("dentro del wait");
        yield return new WaitForSecondsRealtime(time);
        Debug.Log("gdfgdf");
        PanelAnswerAR.SetActive(false);
        Debug.Log("gdfgdf");
        IncorrectAnswerAR.SetActive(false);
        Debug.Log("gdfgdf");
        CorrectAnswerAR.SetActive(false);
        Debug.Log("gdfgdf");
        RestartSuggest();
        Debug.Log("gdfgdf");
        stateAnswer = -1;
        if (owner && counterQuestion < SIZE_QUESTIONS)
        {
            Debug.Log("debe solicitar");
            getQuestion();
        }
        else if (counterQuestion == SIZE_QUESTIONS)
        {
            PreguntaAR1.SetActive(false);
            PreguntaAR2.SetActive(false);
            MultiPreguntas.SetActive(false);
            RestartSuggest();
            counterQuestion = 0;
            ExitGroupRequest exitGroup = new ExitGroupRequest();
            exitGroup.action = "exitGroup";
            exitGroup.token = token;
            SendWebSocketMessage(JsonUtility.ToJson(exitGroup));

            ScoreCanvas.SetActive(true);

            Coins.coins += 60;
            Global.count += 60;

            //Environment.SetActive(true);
            QuestionGroupMock.SetActive(false);
        }
        else
        {
            Debug.Log("No entra");
        }
    }

    public void createGroup()
    {
        owner = true;
        token = PlayerPrefs.GetString("token");
        GeneralRequest generalRequest = new GeneralRequest();
        generalRequest.action = "joinGroup";
        generalRequest.token = token;
        SendWebSocketMessage(JsonUtility.ToJson(generalRequest));
    }

    public void joinGroup()
    {
        token = PlayerPrefs.GetString("token");
        JoinGroupRequest joinGroupRequest = new JoinGroupRequest();
        joinGroupRequest.action = "joinGroup";
        joinGroupRequest.token = token;
        tempGroupName = groupNameInput.text;
        joinGroupRequest.groupName = tempGroupName;
        SendWebSocketMessage(JsonUtility.ToJson(joinGroupRequest));
        groupNameInput.text = "";
    }

    public void statusPlayer()
    {
        StatusRequest statusRequest = new StatusRequest();
        Ready.SetActive(false);
        Cancel.SetActive(true);
        statusRequest.action = "playerReady";
        statusRequest.ready = true;
        statusRequest.token = token;
        SendWebSocketMessage(JsonUtility.ToJson(statusRequest));
    }

    public void getQuestion()
    {
        GetQuestion getQuestion = new GetQuestion();
        getQuestion.action = "getQuestion";
        getQuestion.token = token;
        SendWebSocketMessage(JsonUtility.ToJson(getQuestion));
    }

    public void PressOption(int answer)
    {
        stateAnswer = answer;
        Send.interactable = true;
        Suggest.interactable = true;
    }

    public void SendAnswer()
    {
        ValidateAnswer validateAnswer = new ValidateAnswer();
        validateAnswer.action = "validateAnswer";
        validateAnswer.token = token;
        validateAnswer.questionID = questionID;
        validateAnswer.answer = stateAnswer;
        SendWebSocketMessage(JsonUtility.ToJson(validateAnswer));
    }
    /*
    public void SendAnswerAR()
    {
        ValidateAnswer validateAnswer = new ValidateAnswer();
        validateAnswer.action = "validateAnswer";
        validateAnswer.token = token;
        validateAnswer.questionID = questionID;
        validateAnswer.answer = currentAnswerQuestionAR;
        SendWebSocketMessage(JsonUtility.ToJson(validateAnswer));
    }*/

    public void MultiPregunta()
    {
        SceneManager.LoadScene("MultiPregunta");
    }

    void Update()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        websocket.DispatchMessageQueue();
#endif

        if (isOption)
        {
            if (TimeLeftOption > 0)
            {
                TimeLeftOption -= Time.deltaTime;
                TimeBarOption.fillAmount = TimeLeftOption / MaxTimeOption;
            }
            else
            {
                Time.timeScale = 0;
            }
        }
        if (isAR)
        {
            if (TimeLeftAR > 0)
            {
                TimeLeftAR -= Time.deltaTime;
                TimeBarAR.fillAmount = TimeLeftAR / MaxTimeAR;
            }
            else
            {
                Time.timeScale = 0;
            }
        }


        if (cambio)
        {
            Debug.Log("Envie algoksabhsa bhdas");
            PressOption(opcion_mia_de_mi);
            SendAnswer();
            cambio = false;
        }
        reseticons();
        switch (opcion_ar_jugador1)
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

        switch (opcion_ar_jugador3)
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

        switch (opcion_ar_jugador2)
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
    void reseticons()
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

    }

    async void SendWebSocketMessage(string message)
    {
        if (websocket.State == WebSocketState.Open)
        {
            await websocket.SendText(message);
        }
    }

    private async void OnApplicationQuit()
    {
        await websocket.Close();
    }
}
