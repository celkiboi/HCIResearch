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
    public OpenCvSharp.Rect[] Faces { get; private set; }
    CascadeClassifier cascade;
    [SerializeField]
    WebCamProcessor webCamProcessor;

    void Start()
    {
        if (!WebCamProcessor.ShouldRun || 
            (
                !(ControllerManager.SelectedController == FaceDetectionController.Instance) 
                && !(ControllerManager.SelectedController == FaceMovementController.Instance)
            )
        )
        {
            this.gameObject.SetActive(false);
            return;
        }

        string xmlFilePath = Path.Combine(Application.streamingAssetsPath,
            "haarcascade/haarcascade_frontalface_default.xml");
        cascade = new CascadeClassifier(xmlFilePath);
        // empty array to prevent NullException on the first rendered frame
        Faces = new OpenCvSharp.Rect[0];
        int detectTimesPerSecond = SettingsManager.FaceDetectionFrequency;
        StartCoroutine(DoPeriodicFacialDetection(detectTimesPerSecond));
    }

    IEnumerator DoPeriodicFacialDetection(int timesPerSecond)
    {
        for (;;)
        {
            yield return new WaitForSeconds(1.0f / timesPerSecond);
            Mat frame = webCamProcessor.Image;
            if (frame != null) 
                Faces = cascade.DetectMultiScale(frame, 1.3, 4, HaarDetectionType.ScaleImage);
            if (Faces.Length >= 1)
                Height = Faces[0].Center.Y;
        }
    }
}
