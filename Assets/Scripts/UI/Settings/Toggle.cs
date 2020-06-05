using UnityEngine;
using UnityEngine.UI;

public class Toggle : MonoBehaviour {
    [System.Serializable] public enum ButtonType { GraphicBtn, CameraBtn }

    public ButtonType type;
    public int preset;
    private bool toggle;
    private Color toggledColor;
    private int baseValue;

    private void Awake() {
        toggledColor = new Color(0.75f, 0.75f, 0.75f, 1f);
    }

    private void Start() {
        UpdateBaseValue();
        if(baseValue == preset){
            GetComponent<Image>().color = toggledColor;
            toggle = true;
        } else toggle = false;
    }

    private void Update() {
        UpdateBaseValue();
        if(baseValue == preset && !toggle){
            GetComponent<Image>().color = toggledColor;
            toggle = true;
        }
        else if(baseValue != preset && toggle){
            GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
            toggle = false;
        }
    }

    private void UpdateBaseValue(){
        switch (type)
        {
            case ButtonType.GraphicBtn:
                baseValue = GameManager.Instance.Quality;
                break;

            case ButtonType.CameraBtn:
                baseValue = GameManager.Instance.CameraMode;
                break;

            default:
                throw new System.Exception("Invalid button type");
        }
    }
}