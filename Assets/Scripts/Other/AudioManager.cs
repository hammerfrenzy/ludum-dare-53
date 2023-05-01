using UnityEngine;
using System;


//call AudioManager by using FindObjectOfType<AudioManager>().Play("Sound_Name");

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public static AudioManager instance;
    public bool muteSfx = false;
    public bool muteBgm = false;
    void Awake()
    {
        if (FindObjectsOfType<AudioManager>().Length > 1)
        {
            // there's already one in the scene.
            // this is probably cause main menu.
            // leave it in game scene so we don't 
            // have to start from main menu each time.
            Destroy(gameObject);
            return;
        }
        if (instance == null)
        {
            instance = this;
        }
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
        Play("Background Music");
        Play("Background Wind");
        Play("Ship Creaking");
    }

    void FixedUpdate()
    {
        Sound s = Array.Find(sounds, sound => sound.name == "Background Music");

        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found in AudioManager.");
            return;
        }

        if (muteBgm)
        {
            s.source.volume = 0.0f;
        }
        else
        {
            s.source.volume = s.source.volume;
        }
    }

    public void Play(string name)
    {
        if (muteSfx) return;

        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found in AudioManager.");
            return;
        }

        s.source.Play();
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found in AudioManager.");
            return;
        }

        s.source.Stop();
    }

    public void ToggleBGM()
    {
        muteBgm = !muteBgm;
    }

    public void ToggleSFX()
    {
        muteSfx = !muteSfx;
    }
}
