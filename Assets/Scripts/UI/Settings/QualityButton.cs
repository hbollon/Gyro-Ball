using UnityEngine;
using UnityEngine.UI;

public class QualityButton : MonoBehaviour {
    public int preset;
    private bool toggle;
    private Color toggledColor;

    private void Awake() {
        toggledColor = new Color(0.75f, 0.75f, 0.75f, 1f);
    }

    private void Start() {
        if(GameManager.Instance.Quality == preset){
            GetComponent<Image>().color = toggledColor;
            toggle = true;
        } else toggle = false;
    }

    private void Update() {
        if(GameManager.Instance.Quality == preset && !toggle){
            GetComponent<Image>().color = toggledColor;
            toggle = true;
        }
        else if(GameManager.Instance.Quality != preset && toggle){
                GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
                toggle = false;
            }
        }
}