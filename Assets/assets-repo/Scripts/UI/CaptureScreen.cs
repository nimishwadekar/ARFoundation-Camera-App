using System;
using System.Collections;
using System.IO;
using UnityEngine;

public class CaptureScreen : MonoBehaviour
{
    [SerializeField]
    private GameObject captureCanvas;

    private string imageDirPath, imageDir;

    public event Action<Texture2D> SwitchToImageCanvas;

    private void Start()
    {
        imageDir = "Images" + Path.DirectorySeparatorChar;
        imageDirPath = Application.persistentDataPath + Path.DirectorySeparatorChar + imageDir;
        if (!Directory.Exists(imageDirPath)) Directory.CreateDirectory(imageDirPath);
    }

    public void OnImageCapture()
    {
        StartCoroutine(TakePhoto());
    }

    private IEnumerator TakePhoto()
    {
        yield return new WaitForEndOfFrame();

        captureCanvas.SetActive(false);
        yield return new WaitForEndOfFrame();

        Texture2D texture = ScreenCapture.CaptureScreenshotAsTexture();
        yield return new WaitForEndOfFrame();

        captureCanvas.SetActive(true);
        yield return new WaitForEndOfFrame();

        string imageName = "image.png";
        string imagePath = imageDirPath + imageName;
        File.WriteAllBytes(imagePath, texture.EncodeToPNG());
        SwitchToImageCanvas(texture);
    }
}
