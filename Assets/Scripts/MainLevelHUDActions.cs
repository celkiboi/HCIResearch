using OpenCvSharp.Demo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainLevelHUDActions : MonoBehaviour
{
    [SerializeField]
    WebCamProcessor webCamProcessor;
    public void PlayAgain()
    {
        if(WebCamProcessor.ShouldRun)
        {
            webCamProcessor.Stop();
            WebCamProcessor.ShouldRun = true;
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        webCamProcessor.Stop();
        SceneManager.LoadScene("MainMenu");
    }
}
