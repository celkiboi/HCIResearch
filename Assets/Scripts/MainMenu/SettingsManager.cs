using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsManager : MonoBehaviour
{
    public static int FaceDetectionFrequency { get; private set; } = 18;

    [SerializeField]
    TextMeshProUGUI faceDetectionFrequencyText;

    public void OnFaceDetectionFrequencyChanged(float value)
    {
        FaceDetectionFrequency = 6 + (int)(value * 24);
    }

    private void Update()
    {
        faceDetectionFrequencyText.text = FaceDetectionFrequency.ToString() + " Hz";
    }
}
