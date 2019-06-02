using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    public AudioMixer audioMixer;

    public void SubmitMusicValue(Slider musicEffectsSlider)
    {
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(musicEffectsSlider.value) * 20);
        PlayerPrefs.SetFloat("MusicEffects", musicEffectsSlider.value);
    }

    public void SubmitEffectsValue(Slider soundEffectsSlider)
    {
        audioMixer.SetFloat("SoundEffectsVolume", Mathf.Log10(soundEffectsSlider.value) * 20);
        PlayerPrefs.SetFloat("SoundEffects", soundEffectsSlider.value);
    }
}