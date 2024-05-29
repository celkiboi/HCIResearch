using OpenCvSharp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class ColorDetector : MonoBehaviour
{
    public static float Height { get; private set; }
    public static float Width { get; private set; }
    Mat frame;
    public int NumberOfPixelsToSkip { get; set; } = 1;
    public static Color WantedColor;

    static Vector3 colorTolerance = new(0.1f, 0.1f, 0.1f);

    [SerializeField]
    RawImage cameraImage;

    [SerializeField]
    int detectTimesPerSecond = 50;

    private void Start()
    {
        if (!WebCamProcessor.ShouldRun || ControllerManager.SelectedController != ColorDetectionController.Instance)
        {
            this.gameObject.SetActive(false);
            return;
        }

        WantedColor = MenuCameraColorSelector.SelectedColor;
        NumberOfPixelsToSkip = SettingsManager.ColorDetectionPixelsToSkip;
        StartCoroutine(DoPeriodicColorDetection(detectTimesPerSecond));
    }

    [SerializeField]
    WebCamProcessor webCamProcessor;

    IEnumerator DoPeriodicColorDetection(int timesPerSecond)
    {
        for (;;)
        {
            yield return new WaitForSeconds(1.0f / timesPerSecond);
            frame = webCamProcessor.Image;
            if (frame != null)
            {
                Texture2D cameraTexture = cameraImage.texture as Texture2D;
                (bool success, int height, int width) result = FindColorHeight(cameraTexture);
                if (result.success)
                {
                    Height = result.height;
                    Width = result.width;
                }
            }
        }   
    }

    (bool success, int height, int width) FindColorHeight(Texture2D texture)
    {
        Color[] pixels = texture.GetPixels();
        int width = texture.width;
        int height = texture.height;

        for (int i = 0; i < height; i+=NumberOfPixelsToSkip)
        {
            for (int j = 0; j < width; j+=NumberOfPixelsToSkip)
            {
                Color currentColor = pixels[i * width + j];
                if (CompareWithTolerance(currentColor, WantedColor, colorTolerance))
                {
                    return (true, height - i, j);
                }
            }
        }

        return (false, 0, 0);
    }

    bool CompareWithTolerance(Color current, Color wanted, Vector3 tolerance)
    {
        return Math.Abs(current.r - wanted.r) < tolerance.x
            && Math.Abs(current.g - wanted.g) < tolerance.y
            && Math.Abs(current.b - wanted.b) < tolerance.z;
    }

    public static void SetColorToleranceRed(float red)
    {
        colorTolerance.x = red; 
    }

    public static void SetColorToleranceGreen(float green) 
    {
        colorTolerance.y = green;
    }

    public static void SetColorToleranceBlue(float blue) 
    {
        colorTolerance.z = blue;
    }

    public static Vector3 GetColorTolerance()
    {
        return colorTolerance;
    }

}
