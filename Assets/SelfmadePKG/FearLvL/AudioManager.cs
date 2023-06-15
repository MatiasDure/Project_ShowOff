using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public Sound[] sounds;

    public static AudioManager instance;

    // Start is called before the first frame update
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.priority = s.priority;
        }
    }
    /**
    void Start()
    {
        Play("Theme");
    }/**/

    public void Play (string name)
    {
        Sound s =  Array.Find(sounds, sound => sound.name == name); // To play sound in specific point:
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + "not found!");
            return;
        }
        else
        {
            Debug.Log("Playing: " + name);
        }
        s.source.Play();                                           // FindObjectOfType<AudioManager>().Play("Sound_Name");
    }

    //Nico
    public void PlayWithPitch(string name, float pitch)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        else
        {
            //Debug.Log("Playing: " + name + " with pitch: " + pitch);
        }
        s.source.pitch = pitch;
        s.source.Play();
    }

    public float GetClipLength(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s != null)
        {
            return s.Length;
        }
        else
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return 0f;
        }
    }
}
