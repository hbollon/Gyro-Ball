using UnityEngine;
using UnityEngine.UI;

public class MuteSwitchToggle : MonoBehaviour {
    private bool toggle;
    private bool baseValue;

    private void Awake() {
        
    }

    private void Start() {
        UpdateBaseValue();
        if(baseValue){
            GetComponent<UnityEngine.UI.Toggle>().SetIsOnWithoutNotify(true);
            GetComponent<Image>().sprite = Resources.Load<Sprite>("Icons/speaker_off");
            toggle = true;
        } else {
            GetComponent<UnityEngine.UI.Toggle>().SetIsOnWithoutNotify(false);
            GetComponent<Image>().sprite = Resources.Load<Sprite>("Icons/speaker");
            toggle = false;
        }
    }

    private void Update() {
        UpdateBaseValue();
        if(baseValue && !toggle){
            GetComponent<Image>().sprite = Resources.Load<Sprite>("Icons/speaker_off");
            toggle = true;
        } else if (!baseValue && toggle) {
            GetComponent<Image>().sprite = Resources.Load<Sprite>("Icons/speaker");
            toggle = false;
        }
    }

    private void UpdateBaseValue(){
        baseValue = SoundManager.Instance.SoundDisabled;
    }
}