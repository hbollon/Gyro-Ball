using UnityEngine;

/**
    Implement gravity for rigidbody
    Ignore mass of the gameobject
*/

public class Gravity : MonoBehaviour {
    private float magnitude = -9.81f;

    public Vector3 GravityForce
    {
        get{
            if (gameObject == null){
                return Vector3.up;
            }
            return gameObject.transform.up * magnitude;
        }
    }

    void FixedUpdate () {
        GetComponent<Rigidbody>().AddForce(new Vector3(0.0f, magnitude, 0.0f), ForceMode.Acceleration);
    }
}