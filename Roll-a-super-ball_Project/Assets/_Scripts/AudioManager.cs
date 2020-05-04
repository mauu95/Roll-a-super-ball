using System;
using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public bool playTheme;
    public Sound defaultSound;
    public Sound[] sounds;
    public static AudioManager instance;

    private void Awake()
    {
        //DontDestroyOnLoad(gameObject);
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        defaultSound.source = gameObject.AddComponent<AudioSource>();
        defaultSound.source.clip = defaultSound.clip;
        defaultSound.source.volume = defaultSound.volume;
        defaultSound.source.loop = defaultSound.loop;

        foreach (Sound s in sounds){
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.loop = s.loop;
        }
    }

    private void Start()
    {
        Sound theme = AudioManager.instance.GetSound("Theme");

        if (playTheme && theme != null)
            theme.Play();
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null)
        {
            defaultSound.source.Play();
            Debug.LogError("Sound '" + name + "' not found." + " Played DefaultSound instead. Add a sound named '" + name + "' to the AudioManager to FIX THIS ERROR.");
            return;
        }
        s.source.Play();
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
            return;

        s.source.Stop();
    }

    public Sound GetSound(string name)
    {
        return Array.Find(sounds, sound => sound.name == name);
    }
}
