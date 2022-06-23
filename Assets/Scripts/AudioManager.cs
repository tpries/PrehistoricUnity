using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    // Start is called before the first frame update
    void Awake()
    {
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
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s != null && !s.source.isPlaying)
        {

            if(s.fadeTime == 0)
            {
                s.source.Play();
            }
            else
            {
                StartCoroutine(AudioHelper.FadeIn(s.source, s.fadeTime, s.volume));
            }
        }
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s != null && s.source.isPlaying)
        {
            if(s.fadeTime == 0)
            {
                s.source.Stop();
            }
            else
            {
                StartCoroutine(AudioHelper.FadeOut(s.source, s.fadeTime));
            }
        }

    }

    public void IncreaseVolume(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s != null && s.source.isPlaying)
        {

            StartCoroutine(AudioHelper.FadeInVolume(s.source, s.fadeTimeVolume, s.maxVolume));
        }
    }

    public void DecreaseVolume(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s != null && s.source.isPlaying)
        {
            StartCoroutine(AudioHelper.FadeOutVolume(s.source, s.fadeTimeVolume, s.volume));
        }
    }
}

