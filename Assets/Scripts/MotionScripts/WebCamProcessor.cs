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
    }

    protected override bool ProcessTexture(WebCamTexture input, ref Texture2D output)
    {
        Image = OpenCvSharp.Unity.TextureToMat(input);

        if (ControllerManager.SelectedController == FaceDetectionController.Instance)
        {
            if (faceDetector.faces.Length > 0)
            {
                Image.Rectangle(faceDetector.faces[0], new Scalar(0, 250, 0), 2);
            }
        }
        else if (ControllerManager.SelectedController == ColorDetectionController.Instance) 
        {
            Point detectCoords = new(ColorDetector.Width, ColorDetector.Height);
            OpenCvSharp.Rect detectionRect = new(detectCoords, new(5, 5));
            Image.Rectangle(detectionRect, new Scalar(0, 250, 0), 2);
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
