using UnityEngine;

public class Ball : MonoBehaviour {

    private Rigidbody m_Rigidbody;

    private void Start() {
        // Disable sleep for Rigidbody
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Rigidbody.sleepThreshold = 0.0f;
    }

    private void Update() {
        
    }
}