using OpenCvSharp;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class FaceDetector : MonoBehaviour
{
    public static float Height { get; private set; } = 0;
    public OpenCvSharp.Rect[] faces;
    Mat frame;
    CascadeClassifier cascade;

    void Start()
    {
        if (!WebCamProcessor.ShouldRun || ControllerManager.SelectedController != FaceDetectionController.Instance)
        {
            this.gameObject.SetActive(false);
            return;
        }

        string xmlFilePath = Path.Combine(Application.streamingAssetsPath,
            "haarcascade/haarcascade_frontalface_default.xml");
        cascade = new CascadeClassifier(xmlFilePath);
        frame = webCamProcessor.Image;
        int detectTimesPerSecond = SettingsManager.FaceDetectionFrequency;
        StartCoroutine(DoPeriodicFacialDetection(detectTimesPerSecond));
    }

    private void Update()
    {
        if (faces.Length >= 1)
            Height = faces[0].Center.Y;
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
