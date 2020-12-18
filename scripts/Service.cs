using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.UI;
using SimpleJSON;
using UnityEngine;

public class Service : MonoBehaviour
{
    string baseUrl = "http://127.0.0.1:8000/api/";
    Text questionText;
    Text answerText;

    public void getQuestions()
    {
        StartCoroutine(APIgetQuestions());
    }

    IEnumerator APIgetQuestions()
    {
        UnityWebRequest request = UnityWebRequest.Get(baseUrl + "questions/");
        yield return request.SendWebRequest();
        if (request.isNetworkError)
        {
            Debug.Log("Error: " + request.error);
        }
        else
        {
            string response = request.downloadHandler.text;
            Debug.Log(response);
        }
    }

    public void getQuestion(int idQuestion)
    {
        StartCoroutine(APIgetQuestion(idQuestion));
    }

    IEnumerator APIgetQuestion(int idQuestion)
    {
        UnityWebRequest request = UnityWebRequest.Get(baseUrl + "questions/" + idQuestion);
        yield return request.SendWebRequest();
        if (request.isNetworkError)
        {
            Debug.Log("Error: " + request.error);
        }
        else
        {
            string response = request.downloadHandler.text;
            Debug.Log(response);
            var json_response = JSON.Parse(response);
            Debug.Log(json_response["question"]);
        }
    }
}
