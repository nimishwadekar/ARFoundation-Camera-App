using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class CaptureScreen : MonoBehaviour
{
    private string imageDirPath;
    private ARCameraManager cameraManager;
    private ARCameraBackground cameraBackground;
    private XRCameraConfiguration? currentConfig;

    public event Action<Texture2D> SwitchToImageCanvas;

    private WebCamTexture webCamTexture;

    private void Start()
    {
        cameraManager = FindObjectOfType<ARCameraManager>();
        cameraBackground = FindObjectOfType<ARCameraBackground>();
        imageDirPath = Application.persistentDataPath + Path.DirectorySeparatorChar + "Images" + Path.DirectorySeparatorChar;
        if (!Directory.Exists(imageDirPath)) Directory.CreateDirectory(imageDirPath);

        /*webCamTexture = new WebCamTexture();
        GetComponent<Renderer>().material.mainTexture = webCamTexture;
        webCamTexture.Play();*/
    }

    public void OnImageCapture()
    {
        /*if(!cameraManager.TryAcquireLatestCpuImage(out XRCpuImage image))
        {
            Log.Print("Image not acquired.");
            return;
        }

        StartCoroutine(GetImage(image));
        image.Dispose();*/

        StartCoroutine(TakePhoto());
    }

    private IEnumerator GetImage(XRCpuImage image)
    {
        var request = image.ConvertAsync(new XRCpuImage.ConversionParams
        {
            inputRect = new RectInt(0, 0, image.width, image.height),
            outputDimensions = new Vector2Int(image.width, image.height),
            outputFormat = TextureFormat.RGB24,
            transformation = XRCpuImage.Transformation.MirrorX
        });

        while (!request.status.IsDone()) yield return null;

        if (request.status != XRCpuImage.AsyncConversionStatus.Ready)
        {
            Log.Printf("Request failed with status {0}", request.status);
            request.Dispose();
            yield break;
        }

        var data = request.GetData<byte>();

        Texture2D m_Texture = new Texture2D(
            request.conversionParams.outputDimensions.x,
            request.conversionParams.outputDimensions.y,
            request.conversionParams.outputFormat,
            false);

        m_Texture.LoadRawTextureData(data);
        m_Texture.Apply();

        m_Texture = m_Texture.RotateClockwise90();

        byte[] bytes = m_Texture.EncodeToPNG();

        string filename = "capture.png";
        string filepath = imageDirPath + filename;

        if (File.Exists(filepath)) File.Delete(filepath);
        FileInfo file = new FileInfo(imageDirPath + "capture.png");
        using (var writer = file.OpenWrite())
        {
            foreach (byte b in bytes) writer.WriteByte(b);
        }

        request.Dispose();

        SwitchToImageCanvas(m_Texture);
    }

    private IEnumerator TakePhoto()
    {
        yield return new WaitForEndOfFrame();

        ScreenCapture.CaptureScreenshot("Images" + Path.DirectorySeparatorChar + "image.png");
        //ScreenCapture.CaptureScreenshot("image.png");
    }
}
