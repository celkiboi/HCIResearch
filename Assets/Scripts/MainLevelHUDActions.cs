using OpenCvSharp.Demo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainLevelHUDActions : MonoBehaviour
{
    [SerializeField]
    WebCamManager webCamManager;
    public void PlayAgain()
    {
        if(WebCamManager.ShouldRun)
        {
            webCamManager.Stop();
            WebCamManager.ShouldRun = true;
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        webCamManager.Stop();
        SceneManager.LoadScene("MainMenu");
    }
}
