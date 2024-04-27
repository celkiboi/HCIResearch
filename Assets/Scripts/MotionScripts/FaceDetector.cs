using OpenCvSharp;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using UnityEngine;

public class FaceDetector : MonoBehaviour
{
    public static float Height { get; private set; }

    float lastHeight = 0;
    OpenCvSharp.Rect[] faces;
    Mat frame;

    [SerializeField]
    int detectTimesPerSecond = 10;
    //[SerializeField]
    //WebCamManager webCamManager;

    CascadeClassifier cascade;

    float GetFaceHeight()
    {
        if (faces.Length >= 1)
        {
            Debug.Log($"{faces[0].Height}");
            lastHeight = faces[0].Height;
        }
        return lastHeight;
    }


    void Start()
    {
        if (!WebCamManager.ShouldRun)
        {
            this.gameObject.SetActive(false);
            return;
        }

        //ovo
        //texture = (Texture2D)webCamManager.GetComponent<Renderer>().material.mainTexture;

        string xmlFilePath = Path.Combine(Application.streamingAssetsPath,
            "haarcascade/haarcascade_frontalface_default.xml");
        cascade = new CascadeClassifier(xmlFilePath);

        StartCoroutine(DoPeriodicFacialDetection(detectTimesPerSecond));
    }

    private void Update()
    {
        Height = GetFaceHeight();
        //Display();
    }

    IEnumerator DoPeriodicFacialDetection(int timesPerSecond)
    {
        for (;;)
        {
            Debug.Log("Triggered");
            frame = OpenCvSharp.Unity.TextureToMat(WebCamManager.webCamTexture);
            faces = cascade.DetectMultiScale(frame, 1.1, 2, HaarDetectionType.ScaleImage);
            yield return new WaitForSeconds(1.0f / timesPerSecond);
        }
    }


    //Texture2D texture;
    //public void Display()
    //{
    //    if (faces.Length >= 1)
    //    {
    //        frame.Rectangle(faces[0], new Scalar(255, 0, 0), 2);
    //        OpenCvSharp.Unity.MatToTexture(frame, texture);
    //        webCamManager.GetComponent<Renderer>().material.mainTexture = texture;
    //    }
    //}
}
