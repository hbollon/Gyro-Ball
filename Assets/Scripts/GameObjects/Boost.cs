using UnityEngine;

public class Boost : MonoBehaviour {

    public float velocity = 10f;

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Ball")
            other.gameObject.GetComponent<Rigidbody>().AddForce(
                new Vector3(velocity, 0f, 0f),
                ForceMode.Impulse
            );
    }
}