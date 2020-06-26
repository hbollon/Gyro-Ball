using UnityEngine;
using System.Collections;

public class RotatingWall : MonoBehaviour {
    public float rotatingSpeed = 50f;
    public bool invertRotating = false;
    public bool debug = false;

    private void Start() {
        StartCoroutine(Rotate());
    }

    private IEnumerator Rotate() {
        Quaternion initialRotation = transform.localRotation;
        for(;;){
            if(!invertRotating)
                transform.Rotate(0, rotatingSpeed * Time.deltaTime, 0);
            else
                transform.Rotate(0, -rotatingSpeed * Time.deltaTime, 0);
            yield return new WaitForFixedUpdate();
        }
    }

    private void OnGUI () {
        if(debug){
            GUIStyle style = new GUIStyle();
            style.fontSize = Mathf.RoundToInt(Mathf.Min(Screen.width, Screen.height) / 20f);
            style.normal.textColor = Color.white;
            GUILayout.BeginVertical("box");
            GUILayout.Label("Rotation: " + transform.rotation.ToString(), style);
            GUILayout.EndVertical();
        }
    }

}