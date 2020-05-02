using System;
using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound defaultSound;
    public Sound[] sounds;
    public static AudioManager instance;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
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
        defaultSound.source.pitch = defaultSound.pitch;
        defaultSound.source.loop = defaultSound.loop;

        foreach (Sound s in sounds){
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null)
        {
            defaultSound.source.Play();
            Debug.LogError("Sound '" + name + "' not found." + " Played DefaultSound instead");
            return;
        }
        s.source.Play();
    }
}
