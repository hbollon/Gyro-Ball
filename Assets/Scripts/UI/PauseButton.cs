using UnityEngine;
using UnityEngine.EventSystems;

public class PauseButton : MonoBehaviour, IPointerDownHandler {
    public GameObject EventSystem;
    private UIManager UIScript;

    private void Awake() {
        UIScript = EventSystem.GetComponent<UIManager>();
    }
    
    public void OnPointerDown(PointerEventData ped)
    {
        UIScript.PauseControl();
    }  
}