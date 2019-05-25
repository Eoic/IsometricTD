using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public static GameAudioManager instance;

    private List<string> availableTracks;
    private List<float> tracksLength;
    private int trackPointer = 1;

    void Awake()
    {
        if (instance == null)
            instance = this;

        //DontDestroyOnLoad(gameObject);

        foreach (var sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
            sound.source.outputAudioMixerGroup = sound.outputMixer;
        }
    }

    private void Start()
    {
        availableTracks = new List<string>();
        tracksLength = new List<float>();

        // Get available tracks
        foreach (var sound in sounds)
        {
            if (sound.name.StartsWith("Theme"))
            {
                availableTracks.Add(sound.name);
                tracksLength.Add(sound.clip.length);
            }
        }

        // Start playing backgrouund music
        StartCoroutine(PlayBackgroundMusic());
    }

    public void Play(string name)
    {
        foreach (var sound in sounds)
        {
            if (sound.name == name)
            {
                sound.source.Play();
                return;
            }
        }
    }

    public void Stop(string name)
    {
        foreach (var sound in sounds)
        {
            if (sound.name == name)
            {
                sound.source.Stop();
                return;
            }
        }
    }

    IEnumerator PlayBackgroundMusic()
    {
        // Played all available tracks. Start again.
        if (trackPointer == availableTracks.Count)
            trackPointer = 0;

        Play(availableTracks[trackPointer]);
        yield return new WaitForSeconds(tracksLength[trackPointer]);
        trackPointer++;
    }
}
