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
    FaceDetector detector;

    private void Start()
    {
        if (!ShouldRun)
        {
            this.gameObject.SetActive(false);
        }
    }

    protected override bool ProcessTexture(WebCamTexture input, ref Texture2D output)
    {
        Image = OpenCvSharp.Unity.TextureToMat(input);

        if (detector.faces.Length > 0 ) 
        {
            Image.Rectangle(detector.faces[0], new Scalar(0, 250, 0), 2);
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
        webCamTexture.Stop();
    }
}
