using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioScreen : MonoBehaviour
{
    private AudioManager audioManager;
    public event Action SwitchToTextCanvas;

    [SerializeField]
    private Text text;

    private SelectedLanguage language;

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    public void Initialise(SelectedLanguage language, string text)
    {
        this.language = language;
        this.text.text = text;
    }

    public void OnBackButton()
    {
        SwitchToTextCanvas();
    }

    public void OnPlayButton()
    {
        print("Playing " + language);
        switch(language)
        {
            case SelectedLanguage.English:
                audioManager.Play("English Audio");
                break;

            case SelectedLanguage.Hindi:
                audioManager.Play("Hindi Audio");
                break;
        }
    }
}
