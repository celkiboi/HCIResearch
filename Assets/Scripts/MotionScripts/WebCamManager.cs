using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenCvSharp;

public class WebCamManager : MonoBehaviour
{
    public static bool ShouldRun { get; set; }

    public static WebCamTexture webCamTexture;

    Color[] colors;

    void Start()
    {
        if (ShouldRun)
        {
            WebCamDevice[] devices = WebCamTexture.devices;

            if (webCamTexture == null)
            {
                webCamTexture = new WebCamTexture(devices[0].name);
                webCamTexture.requestedWidth = 1920;
                webCamTexture.requestedHeight = 1080;
            }

            webCamTexture.Play();
        }
    }

    void Update()
    {
        if (ShouldRun)
        { 
            GetComponent<Renderer>().material.mainTexture = webCamTexture;
        }
    }

    public void Stop()
    {
        if (ShouldRun)
        {
            webCamTexture.Stop();
            ShouldRun = false;
        }
    }
}
