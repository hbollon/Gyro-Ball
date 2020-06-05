using UnityEngine;
using UnityEngine.UI;

public class ToggleSlider : MonoBehaviour {
    public SoundManager.AudioSourceComponant sourceAudio;

    private float baseValue;

    private void Start() {
        UpdateBaseValue();
        GetComponent<Slider>().value = baseValue;
    }

    private void UpdateBaseValue(){
        switch (sourceAudio)
        {
            case SoundManager.AudioSourceComponant.Music:
                baseValue = SoundManager.Instance.MusicSource.volume / 0.3f;
                break;

            case SoundManager.AudioSourceComponant.Ambient:
                baseValue = SoundManager.Instance.AmbientSource.volume / 0.3f;
                break;

            case SoundManager.AudioSourceComponant.Effects:
                baseValue = SoundManager.Instance.EffectsSource.volume;
                break;

            default:
                throw new System.Exception("Invalid audio source");
        }
    }
}