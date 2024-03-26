using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NavigationController : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("GameLevel");
    }

    public void PlayTutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }
}
