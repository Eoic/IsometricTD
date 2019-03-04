using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public Sound mouseClick;
    public static AudioManager instance;
    
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

        foreach (var s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }

        mouseClick.source = gameObject.AddComponent<AudioSource>();
        mouseClick.source.clip = mouseClick.clip;
        mouseClick.source.volume = mouseClick.volume;
        mouseClick.source.pitch = mouseClick.pitch;
    }

    void Start()
    {
        Play("Theme1");
    }

    public void Play(string name)
    {
        foreach (var s in sounds)
        {
            if (s.name == name)
            {
                s.source.Play();
                return;
            }
        }

        Debug.LogWarning("Sound: " + name + " not found!");
    }

    public void Stop(string name)
    {
        foreach (var s in sounds)
        {
            if (s.name == name)
            {
                s.source.Stop();
                return;
            }
        }

        Debug.LogWarning("Sound: " + name + " not found!");
    }

    public void PlayMouseClick()
    {
        if (mouseClick == null)
        {
            Debug.LogError("Couldn't play mouse click sound as it's not defined.");
            return;
        }

        mouseClick.source.PlayOneShot(mouseClick.clip);
    }
}
