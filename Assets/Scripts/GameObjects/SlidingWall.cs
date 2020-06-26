using UnityEngine;
using System.Collections;

public class SlidingWall : MonoBehaviour {
    public float distancePerSeconds = 1f;
    public float distance = 1f;
    public bool invertSliding = false;
    public bool applyForceOnX = false;
    public bool applyForceOnY = false;
    public bool applyForceOnZ = false;
    public bool debug = false;

    private void Start() {
        Vector3 axis = new Vector3(
            applyForceOnX ? 1f : 0f,
            applyForceOnY ? 1f : 0f,
            applyForceOnZ ? 1f : 0f
        );
        StartCoroutine(Slide(axis));
    }

    private IEnumerator Slide(Vector3 axis) {
        Vector3 initialPosition = transform.localPosition;
        for(;;){
            if(!invertSliding){
                transform.localPosition = new Vector3(transform.localPosition.x + distancePerSeconds * Time.deltaTime * axis.x,
                                                transform.localPosition.y + distancePerSeconds * Time.deltaTime * axis.y, 
                                                transform.localPosition.z + distancePerSeconds * Time.deltaTime * axis.z);

                if(transform.localPosition.x >= (initialPosition.x + distance) || 
                    transform.localPosition.y >= (initialPosition.y + distance) || 
                    transform.localPosition.z >= (initialPosition.z + distance)){

                    invertSliding = !invertSliding;
                    transform.localPosition = new Vector3(initialPosition.x + distance * axis.x,
                                                initialPosition.y + distance * axis.y, 
                                                initialPosition.z + distance * axis.z);
                }
            }
            else{
                transform.localPosition = new Vector3(transform.localPosition.x - distancePerSeconds * Time.deltaTime * axis.x,
                                                transform.localPosition.y - distancePerSeconds * Time.deltaTime * axis.y, 
                                                transform.localPosition.z - distancePerSeconds * Time.deltaTime * axis.z);
                                                
                if(transform.localPosition.x <= (initialPosition.x - distance) ||
                    transform.localPosition.y <= (initialPosition.y - distance) ||
                    transform.localPosition.z <= (initialPosition.z - distance)){

                    invertSliding = !invertSliding;
                    transform.localPosition = new Vector3(initialPosition.x - distance * axis.x,
                                                initialPosition.y - distance * axis.y, 
                                                initialPosition.z - distance * axis.z);
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