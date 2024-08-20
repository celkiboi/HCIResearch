using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenCvSharp;
using OpenCvSharp.Demo;

public class WebCamProcessor : WebCamera
{
    public Mat Image;

    public static bool ShouldRun { get; set; } = false;

    [SerializeField]
    FaceDetector faceDetector;

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
    }

    protected override bool ProcessTexture(WebCamTexture input, ref Texture2D output)
    {
        Image = OpenCvSharp.Unity.TextureToMat(input);

        if (ControllerManager.SelectedController == FaceHeightController.Instance 
            || ControllerManager.SelectedController == FaceMovementController.Instance)
        {
            if (faceDetector.Faces.Length > 0)
            {
                Image.Rectangle(faceDetector.Faces[0], new Scalar(0, 250, 0), 2);
            }
        }
        else if (ControllerManager.SelectedController == ColorDetectionController.Instance) 
        {
            Point detectCoords = new(ColorDetector.Width, ColorDetector.Height);
            OpenCvSharp.Rect detectionRect = new(detectCoords, new(5, 5));
            Image.Rectangle(detectionRect, new Scalar(0, 250, 0), 2);
        }
        if (ThresholdCameraSelector.DisplayThresholdBars 
            &&  ControllerManager.SelectedController != FaceMovementController.Instance)
        {
            Point jumpCoords = new(0, FaceHeightController.Instance.JumpThreshold);
            Point duckCoords = new(0, FaceHeightController.Instance.DuckThreshold);
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
}
