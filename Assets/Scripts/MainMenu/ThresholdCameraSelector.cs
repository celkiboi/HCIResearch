using OpenCvSharp.Demo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ThresholdCameraSelector : WebCamera
{
    public static bool ShouldRun { get; set; } = false;
    [Settings("displayThresholdBars")]
    public static bool DisplayThresholdBars = true; // used only for gameplay

    public Mat Image;
    [SerializeField]
    RawImage cameraImage;

    [SerializeField]
    TextMeshProUGUI jumpThresholdText;
    [SerializeField]
    TextMeshProUGUI duckThresholdText;

    [SerializeField]
    Slider jumpThresholdSlider;
    [SerializeField]
    Slider duckThresholdSlider;

    [SerializeField]
    Toggle displayThresholdBarsToggle;

    [Settings("jumpThreshold")]
    static int jumpThreshold = 160;
    [Settings("duckThreshold")]
    static int duckThreshold = 280;

    private void Start()
    {
        if (!ShouldRun)
        {
            if (webCamTexture != null)
                webCamTexture.Stop();
            this.gameObject.SetActive(false);
        }

        webCamTexture.requestedWidth = 640;
        webCamTexture.requestedHeight = 480;

        DoInitialHUDStuff();
    }

    private void DoInitialHUDStuff()
    {
        OnJumpThresholdChanged(jumpThreshold);
        OnDuckThresholdChanged(duckThreshold);
        jumpThresholdSlider.value = jumpThreshold;
        duckThresholdSlider.value = duckThreshold;
        displayThresholdBarsToggle.isOn = DisplayThresholdBars;
    }

    protected override bool ProcessTexture(WebCamTexture input, ref Texture2D output)
    {
        Image = OpenCvSharp.Unity.TextureToMat(input);
        Texture2D cameraTexture = cameraImage.texture as Texture2D;
        if (cameraTexture != null)
        {
            Point jumpCoords = new(0, jumpThreshold);
            Point duckCoords = new(0, duckThreshold);
            OpenCvSharp.Rect jumpLine = new(jumpCoords, new(Image.Width, 1));
            OpenCvSharp.Rect duckLine = new(duckCoords, new(Image.Width, 1));
            // COLORS ARE NOT RGB, THEY ARE BGR
            Image.Rectangle(jumpLine, new Scalar(242, 16, 250), 2);
            Image.Rectangle(duckLine, new Scalar(16, 239, 250), 2);
        }

        if (output == null)
            output = OpenCvSharp.Unity.MatToTexture(Image);
        else
            OpenCvSharp.Unity.MatToTexture(Image, output);

        return true;
    }

    public void Stop()
    {
        ShouldRun = false;
        if (webCamTexture != null)
            webCamTexture.Stop();
    }

    public void KickStart()
    {
        webCamTexture.Play();
        DoInitialHUDStuff();
    }

    public void OnJumpThresholdChanged(float value)
    {
        jumpThreshold = (int)value;
        FaceDetectionController.Instance.SetThreshold(
            duck: FaceDetectionController.Instance.DuckThreshold,
            jump: (int)value);
        ColorDetectionController.Instance.SetThreshold(
            duck: FaceDetectionController.Instance.DuckThreshold,
            jump: (int)value);
        jumpThresholdText.text = FaceDetectionController.Instance.JumpThreshold.ToString();
    }

    public void OnDuckThresholdChanged(float value)
    {
        duckThreshold = (int)value;
        FaceDetectionController.Instance.SetThreshold(
            jump: FaceDetectionController.Instance.JumpThreshold,
            duck: (int)value);
        ColorDetectionController.Instance.SetThreshold(
            jump: FaceDetectionController.Instance.JumpThreshold,
            duck: (int)value);
        duckThresholdText.text = FaceDetectionController.Instance.DuckThreshold.ToString();
    }

    public void ToggleThresholdBarsDisplay(bool value)
    {
        DisplayThresholdBars = value;
    }
}
