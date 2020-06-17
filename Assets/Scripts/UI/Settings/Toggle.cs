using UnityEngine;
using UnityEngine.UI;

public class Toggle : MonoBehaviour {
    [System.Serializable] public enum ButtonType { GraphicBtn, CameraBtn }

    public ButtonType type;
    public int preset;
    private bool toggle;
    private Color toggledColor;
    private int baseValue;
    private Image image;

    private void Awake() {
        toggledColor = new Color(0.75f, 0.75f, 0.75f, 1f);
    }

    private void Start() {
        image = GetComponent<Image>();

        UpdateBaseValue();
        if(baseValue == preset){
            image.color = toggledColor;
            toggle = true;
        } else toggle = false;
    }

    private void Update() {
        UpdateBaseValue();
        if(baseValue == preset && !toggle){
            image.color = toggledColor;
            toggle = true;
        }
        else if(baseValue != preset && toggle){
            image.color = new Color(1f, 1f, 1f, 1f);
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