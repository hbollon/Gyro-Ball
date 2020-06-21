using UnityEngine;
using System.Collections;

public class SlidingWall : MonoBehaviour {
    public float distancePerSeconds = 1f;
    public float distance = 1f;
    public bool invertSliding = false;
    public bool debug = false;

    private void Start() {
        StartCoroutine(Slide());
    }

    private IEnumerator Slide() {
        Vector3 initialPosition = transform.localPosition;
        for(;;){
            if(!invertSliding){
                transform.localPosition = new Vector3(transform.localPosition.x,
                                                transform.localPosition.y, 
                                                transform.localPosition.z + distancePerSeconds * Time.deltaTime);

                if(transform.localPosition.z >= (initialPosition.z + distance)){
                    invertSliding = !invertSliding;
                    transform.localPosition = new Vector3(transform.localPosition.x,
                                                transform.localPosition.y, 
                                                initialPosition.z + distance);
                }
            }
            else{
                transform.localPosition = new Vector3(transform.localPosition.x,
                                                transform.localPosition.y, 
                                                transform.localPosition.z - distancePerSeconds * Time.deltaTime);
                                                
                if(transform.localPosition.z <= (initialPosition.z - distance)){
                    invertSliding = !invertSliding;
                    transform.localPosition = new Vector3(transform.localPosition.x,
                                                transform.localPosition.y, 
                                                initialPosition.z - distance);
                }
            }

            yield return new WaitForFixedUpdate();
        }
    }

    private void OnGUI () {
        if(debug){
            GUIStyle style = new GUIStyle();
            style.fontSize = Mathf.RoundToInt(Mathf.Min(Screen.width, Screen.height) / 20f);
            style.normal.textColor = Color.white;
            GUILayout.BeginVertical("box");
            GUILayout.Label("Local position: " + transform.localPosition.ToString(), style);
            GUILayout.EndVertical();
        }
    }

}