using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    public void SubmitMusicValue(Slider musicEffectsSlider)
    {
        PlayerPrefs.SetFloat("MusicEffects", musicEffectsSlider.value);
    }

    public void SubmitEffectsValue(Slider soundEffectsSlider)
    {
        PlayerPrefs.SetFloat("SoundEffects", soundEffectsSlider.value);
    }
}
