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
    public OpenCvSharp.Rect[] faces;
    Mat frame;

    [SerializeField]
    int detectTimesPerSecond = 10;

    CascadeClassifier cascade;

    float GetFaceHeight()
    {
        if (faces.Length >= 1)
        {
            lastHeight = faces[0].Center.Y;
            //Debug.Log(lastHeight);
        }
        return lastHeight;
    }


    void Start()
    {
        if (!WebCamProcessor.ShouldRun)
        {
            this.gameObject.SetActive(false);
            return;
        }

        string xmlFilePath = Path.Combine(Application.streamingAssetsPath,
            "haarcascade/haarcascade_frontalface_default.xml");
        cascade = new CascadeClassifier(xmlFilePath);
        frame = webCamProcessor.Image;
        detectTimesPerSecond = SettingsManager.FaceDetectionFrequency;
        StartCoroutine(DoPeriodicFacialDetection(detectTimesPerSecond));
    }

    private void Update()
    {
        Height = GetFaceHeight();
    }

    [SerializeField]
    WebCamProcessor webCamProcessor;

    IEnumerator DoPeriodicFacialDetection(int timesPerSecond)
    {
        for (;;)
        {
            yield return new WaitForSeconds(1.0f / timesPerSecond);
            frame = webCamProcessor.Image;
            if (frame != null) 
                faces = cascade.DetectMultiScale(frame, 1.1, 2, HaarDetectionType.ScaleImage);
        }
    }
}
