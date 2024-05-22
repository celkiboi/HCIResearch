using OpenCvSharp.Demo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;
using UnityEngine;
using UnityEngine.UI;

public class MenuCameraColorSelector : WebCamera
{
    public static bool ShouldRun { get; set; } = false;

    public static Color SelectedColor { get; private set; } = new(0, 0, 0);

    public Mat Image;
    [SerializeField]
    RawImage cameraImage;
    Color currentColor;
    [SerializeField]
    Image currentColorDisplay;
    [SerializeField]
    Image selectedColorDisplay;
    

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
        Texture2D cameraTexture = cameraImage.texture as Texture2D;
        if (cameraTexture != null)
        {
            Color[] pixels = cameraTexture.GetPixels();
            // mid point is not simply pixels / 2
            // if we had a 10*10 picture, we would get an array of 100 len
            // the middle point wouldn't be 50, as that would be all the way to the left
            // what we need is 10 (rows) / 2 = 5 * 10 (width) to get 50
            // then add 10 (width) / 2 to that value, so we get the mid point, 55
            int midPoint = Image.Height / 2 * Image.Width + Image.Width / 2;
            currentColor = pixels[midPoint];
            currentColorDisplay.color = currentColor;
            selectedColorDisplay.color = SelectedColor;

            Point coords = new(Image.Width / 2 - 3, Image.Height / 2 - 3);
            OpenCvSharp.Rect rect = new(coords, new(8, 8));
            Image.Rectangle(rect, new Scalar(0, 0, 0), 2);
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

    public void KickStart()
    {
        webCamTexture.Play();
    }

    public void OnSelectCurrentColorButtonClicked()
    {
        SelectedColor = currentColor;
    }
}
