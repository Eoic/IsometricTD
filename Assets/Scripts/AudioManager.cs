using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
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
            s.source.outputAudioMixerGroup = s.outputMixer;
        }
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
}
