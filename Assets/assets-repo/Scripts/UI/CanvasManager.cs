using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    [SerializeField]
    private GameObject captureCanvas, imageCanvas, logCanvas, textCanvas, audioCanvas;

    private CaptureScreen captureScreen;
    private ImageScreen imageScreen;
    private TextScreen textScreen;
    private AudioScreen audioScreen;

    private void Start()
    {
        captureScreen = FindObjectOfType<CaptureScreen>();
        imageScreen = FindObjectOfType<ImageScreen>();
        textScreen = FindObjectOfType<TextScreen>();
        audioScreen = FindObjectOfType<AudioScreen>();

        logCanvas.SetActive(true);
        captureScreen.SwitchToImageCanvas += CaptureToImage;
        imageScreen.SwitchToCaptureCanvas += ImageToCapture;
        imageScreen.SwitchToTextCanvas += ImageToText;
        textScreen.SwitchToCaptureCanvas += TextToCapture;
        textScreen.SwitchToAudioCanvas += TextToAudio;
        audioScreen.SwitchToTextCanvas += AudioToText;
    }

    private void CaptureToImage(Texture2D texture)
    {
        captureCanvas.SetActive(false);
        imageCanvas.SetActive(true);
        imageScreen.SetImage(texture);
    }

    private void ImageToCapture()
    {
        imageCanvas.SetActive(false);
        captureCanvas.SetActive(true);
    }

    private void ImageToText()
    {
        imageCanvas.SetActive(false);
        textCanvas.SetActive(true);
    }

    private void TextToCapture()
    {
        textCanvas.SetActive(false);
        captureCanvas.SetActive(true);
    }

    private void TextToAudio(SelectedLanguage language, string text)
    {
        textCanvas.SetActive(false);
        audioCanvas.SetActive(true);
        audioScreen.Initialise(language, text);
    }

    private void AudioToText()
    {
        audioCanvas.SetActive(false);
        textCanvas.SetActive(true);
    }
}
