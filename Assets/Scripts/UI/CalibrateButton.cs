using UnityEngine;
using UnityEngine.EventSystems;

public class CalibrateButton : MonoBehaviour, IPointerDownHandler {
    public GameObject Board;
    private GyroscopeControl gcScript;

    private void Awake() {
        gcScript = Board.GetComponent<GyroscopeControl>();
    }
    
    public void OnPointerDown(PointerEventData ped)
    {
        print("Recalibrating...");
        gcScript.recalibrate();
    }  
}