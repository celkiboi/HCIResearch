using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NavigationController : MonoBehaviour
{
    [SerializeField]
    Canvas initial;
    [SerializeField]
    Canvas input;
    [SerializeField]
    Canvas settings;

    public void Start()
    {
        initial.gameObject.SetActive(true);
        input.gameObject.SetActive(false);
        settings.gameObject.SetActive(false);
    }

    public void Play()
    {
        input.gameObject.SetActive(true);
        settings.gameObject.SetActive(false);
        initial.gameObject.SetActive(false);
    }

    public void GoToSettings()
    {
        input.gameObject.SetActive(false);
        initial.gameObject.SetActive(false);
        settings.gameObject.SetActive(true);
    }

    public void GoBack()
    {
        initial.gameObject.SetActive(true);
        input.gameObject.SetActive(false);
        settings.gameObject.SetActive(false);
    }

    public void PlayKeyboard()
    {
        WebCamProcessor.ShouldRun = false;
        ControllerManager.SelectedController = KeyboardPlayerController.Instance;
        InputThumbnailManager.SelectedThumbnails = KeyboardThumbnailFactory.Instance;
        SceneManager.LoadScene("GameLevel");
    }

    public void PlayController()
    {
        WebCamProcessor.ShouldRun = false;
        ControllerManager.SelectedController = XboxPlayerController.Instance;
        InputThumbnailManager.SelectedThumbnails = ControllerThumbnailFactory.Instance;
        SceneManager.LoadScene("GameLevel");
    }

    public void PlayFaceDetection()
    {
        WebCamProcessor.ShouldRun = true;
        ControllerManager.SelectedController = FaceDetectionController.Instance;
        InputThumbnailManager.SelectedThumbnails = ControllerThumbnailFactory.Instance;
        SceneManager.LoadScene("GameLevel");
    }
}
