using UnityEngine;
using UnityEngine.UI;

public class ToggleSwitch : MonoBehaviour {
    public SoundManager.AudioSourceComponant sourceAudio;

    private bool toggle;
    private bool baseValue;

    private void Start() {
        UpdateBaseValue();
        if(baseValue){
            GetComponent<UnityEngine.UI.Toggle>().SetIsOnWithoutNotify(true);
        } else {
            GetComponent<UnityEngine.UI.Toggle>().SetIsOnWithoutNotify(false);
        }
    }

    private void UpdateBaseValue(){
        switch (sourceAudio)
        {
            case SoundManager.AudioSourceComponant.Music:
                baseValue = SoundManager.Instance.MusicSource.mute;
                break;

            case SoundManager.AudioSourceComponant.Ambient:
                baseValue = SoundManager.Instance.AmbientSource.mute;
                break;

            case SoundManager.AudioSourceComponant.Effects:
                baseValue = SoundManager.Instance.EffectsSource.mute;
                break;

            default:
                throw new System.Exception("Invalid audio source");
        }
    }
}