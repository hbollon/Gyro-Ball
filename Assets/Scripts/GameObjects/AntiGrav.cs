using UnityEngine;

/**
    Not implemented for first release
*/

public class AntiGrav : MonoBehaviour {
    public float impultion = 1f;
    public bool flipCamera = false;
    public GameObject cameraToFlip;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ball"){
            other.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(
                0f, 
                other.gameObject.GetComponent<Rigidbody>().velocity.y * impultion - other.gameObject.GetComponent<Rigidbody>().velocity.y,
                0f), ForceMode.Acceleration);
            other.gameObject.GetComponent<Gravity>().magnitude *= -1; 
        }
    }
}