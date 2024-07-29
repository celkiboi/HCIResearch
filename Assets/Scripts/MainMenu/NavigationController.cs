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
    [SerializeField]
    Canvas info;
    [SerializeField]
    Canvas selectColor;
    [SerializeField]
    Canvas setThreshold;

    [SerializeField]
    MenuCameraColorSelector colorSelector;
    [SerializeField]
    ThresholdCameraSelector thresholdSelector;

    GameSettingsFile gameSettingsFile = GameSettingsFile.Instance;

    public void Start()
    {
        initial.gameObject.SetActive(true);
        input.gameObject.SetActive(false);
        settings.gameObject.SetActive(false);
        info.gameObject.SetActive(true);
        selectColor.gameObject.SetActive(false);
        setThreshold.gameObject.SetActive(false);
        colorSelector.Stop();
        thresholdSelector.Stop();
    }

    public void Play()
    {
        input.gameObject.SetActive(true);
        settings.gameObject.SetActive(false);
        initial.gameObject.SetActive(false);
        info.gameObject.SetActive(true);
        selectColor.gameObject.SetActive(false);
        setThreshold.gameObject.SetActive(false);
        colorSelector.Stop();
        thresholdSelector.Stop();
    }

    public void GoToSettings()
    {
        input.gameObject.SetActive(false);
        initial.gameObject.SetActive(false);
        settings.gameObject.SetActive(true);
        info.gameObject.SetActive(false);
        selectColor.gameObject.SetActive(false);
        setThreshold.gameObject.SetActive(false);
        colorSelector.Stop();
        thresholdSelector.Stop();
    }

    public void GoBack()
    {
        initial.gameObject.SetActive(true);
        input.gameObject.SetActive(false);
        settings.gameObject.SetActive(false);
        info.gameObject.SetActive(true);
        selectColor.gameObject.SetActive(false);
        setThreshold.gameObject.SetActive(false);
        colorSelector.Stop();
        thresholdSelector.Stop();
        GameSettingsFile.Instance.Save();
    }

    public void GoToSelectColor()
    {
        initial.gameObject.SetActive(false);
        input.gameObject.SetActive(false);
        settings.gameObject.SetActive(false);
        info.gameObject.SetActive(false);
        selectColor.gameObject.SetActive(true);
        thresholdSelector.Stop();
        colorSelector.KickStart();
    }

    public void GoSetThreshold()
    {
        initial.gameObject.SetActive(false);
        input.gameObject.SetActive(false);
        settings.gameObject.SetActive(false);
        info.gameObject.SetActive(false);
        selectColor.gameObject.SetActive(false);
        setThreshold.gameObject.SetActive(true);
        colorSelector.Stop();
        thresholdSelector.KickStart();
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
        InputThumbnailManager.SelectedThumbnails = CameraThumbnailFactory.Instance;
        SceneManager.LoadScene("GameLevel");
    }

    public void PlayColorDetection()
    {
        WebCamProcessor.ShouldRun = true;
        ControllerManager.SelectedController = ColorDetectionController.Instance;
        InputThumbnailManager.SelectedThumbnails = CameraThumbnailFactory.Instance;
        SceneManager.LoadScene("GameLevel");
    }
}
