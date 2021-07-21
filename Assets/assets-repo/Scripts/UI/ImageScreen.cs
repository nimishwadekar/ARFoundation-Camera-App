using System;
using UnityEngine;
using UnityEngine.UI;

public class ImageScreen : MonoBehaviour
{
    [SerializeField]
    private Image image;

    public event Action SwitchToCaptureCanvas;
    public event Action SwitchToTextCanvas;

    public void SetImage(Texture2D texture)
    {
        image.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
    }

    public void OnBackButton()
    {
        SwitchToCaptureCanvas();
    }

    public void OnNextButton()
    {
        SwitchToTextCanvas();
    }

    private void Update()
    {
        if (Application.platform == RuntimePlatform.Android && Input.GetKeyDown(KeyCode.Escape)) OnBackButton();
    }
}
