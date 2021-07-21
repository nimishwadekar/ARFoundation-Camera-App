using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextScreen : MonoBehaviour
{
    [SerializeField]
    private Color offColour, onColour;

    [SerializeField]
    private Toggle englishToggle, hindiToggle;
    private Image englishImage, hindiImage;

    [SerializeField]
    private GameObject textBox;
    private Text text;

    [SerializeField]
    private Text warningText;

    private string englishText, hindiText;
    private SelectedLanguage selectedLanguage;

    public event Action SwitchToCaptureCanvas;
    public event Action<SelectedLanguage, string> SwitchToAudioCanvas;

    private void Start()
    {
        warningText.enabled = false;
        text = textBox.transform.GetComponentInChildren<Text>(true);
        englishImage = englishToggle.transform.GetComponentInChildren<Image>(true);
        hindiImage = hindiToggle.transform.GetComponentInChildren<Image>(true);

        englishText = "An eye for an eye only ends up making the whole world blind.";
        hindiText = "आँख के बदले आँख का सिद्धांत सारी दुनिया को अंधा कर देगा |";

        englishImage.color = offColour;
        hindiImage.color = offColour;
        ChangeText(SelectedLanguage.None);
    }

    public void OnEnglishToggle(bool value)
    {
        if (value)
        {
            warningText.enabled = false;
            hindiToggle.isOn = false;
            englishImage.color = onColour;
            ChangeText(SelectedLanguage.English);
        }
        else
        {
            englishImage.color = offColour;
            ChangeText(SelectedLanguage.None);
        }
    }

    public void OnHindiToggle(bool value)
    {
        if (value)
        {
            warningText.enabled = false;
            englishToggle.isOn = false;
            hindiImage.color = onColour;
            ChangeText(SelectedLanguage.Hindi);
        }
        else
        {
            hindiImage.color = offColour;
            ChangeText(SelectedLanguage.None);
        }
    }

    private void ChangeText(SelectedLanguage language)
    {
        switch(language)
        {
            case SelectedLanguage.None:
                selectedLanguage = SelectedLanguage.None;
                textBox.SetActive(false);
                break;

            case SelectedLanguage.English:
                selectedLanguage = SelectedLanguage.English;
                textBox.SetActive(true);
                text.text = englishText;
                break;

            case SelectedLanguage.Hindi:
                selectedLanguage = SelectedLanguage.Hindi;
                textBox.SetActive(true);
                text.text = hindiText;
                break;
        }
    }

    public void OnBackButton()
    {
        SwitchToCaptureCanvas();
    }

    public void OnNextButton()
    {
        if(selectedLanguage == SelectedLanguage.None)
        {
            warningText.enabled = true;
            return;
        }

        SwitchToAudioCanvas(selectedLanguage, text.text);
    }
}

public enum SelectedLanguage
{
    None,
    English,
    Hindi
}