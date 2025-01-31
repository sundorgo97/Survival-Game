using UnityEngine.Audio;
using System;
using UnityEngine;

public class SFXController : MonoBehaviour
{
    public Sound[] sounds;

    public static SFXController instance;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    private void Start()
    {
        Play("Theme");
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.Log("Sound: " + name + " not found!");
            return;
        }
        s.source.Play();
    }

    public void MuteHandler(bool mute)
    {
        if (mute)
        {
            AudioListener.volume = 0;
            PlayerPrefs.SetInt("muted", 0);
        }
        else
        {
            AudioListener.volume = PlayerPrefs.GetFloat("Volume", 0.5f);
            PlayerPrefs.SetInt("muted", 1);
        }
    }

    public void VolumeHandler(float volume)
    {
        AudioListener.volume = volume;
        PlayerPrefs.SetFloat("Volume", volume);
    }
}
