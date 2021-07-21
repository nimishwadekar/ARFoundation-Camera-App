using System;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public int defaultSounds;

    [SerializeField]
    private List<Sound> sounds;
    
    public static AudioManager instance;

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }
    }

    public void Play(string name)
    {
        //Sound s = Array.Find(sounds, sound => sound.name == name);
        Sound s = sounds.Find(sound => sound.name == name);
        if (s == null) return;

        s.source.Play();
    }
}