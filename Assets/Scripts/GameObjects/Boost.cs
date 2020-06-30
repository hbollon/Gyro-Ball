using UnityEngine;

public class Boost : MonoBehaviour {

    public float velocity = 10f;
    public bool addJump = false;

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Ball")
            other.gameObject.GetComponent<Rigidbody>().AddForce(
                new Vector3(velocity, velocity * (addJump ? 1 : 0), 0f),
                ForceMode.Impulse
            );
    }
}