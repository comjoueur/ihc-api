using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class cambioescena : MonoBehaviour
{
    public void MainScene()
    {
        SceneManager.LoadScene("Main Scene");
    }
    public void CameraScene()
    {
        SceneManager.LoadScene("CameraMain");
    }
    public void VideoScene()
    {
        SceneManager.LoadScene("CameraVideo");
    }
    public void QuizScene()
    {
        SceneManager.LoadScene("CameraQuiz");
    }
    public void HelpScene()
    {
        SceneManager.LoadScene("Help Scene");
    }
    public void CardScene()
    {
        SceneManager.LoadScene("Tarjetas");
    }
    public void StoreScene()
    {
        SceneManager.LoadScene("Store Scene");
    }
    public void QUIT()
    {
        Application.Quit();
    }
}
