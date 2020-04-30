using UnityEngine;

public class Ball : MonoBehaviour {

    private Rigidbody m_Rigidbody;
    private bool atEnd;
    private bool attached;

    private void Start() {
        // Disable sleep for Rigidbody
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Rigidbody.sleepThreshold = 0.0f;
        atEnd = false;
        attached = false;
    }

    private void Update() {
        
    }

    public void setAtEnd(bool atEnd){
        this.atEnd = atEnd;
    }

    public bool isAtEnd(){
        return atEnd;
    }

    public void setAttached(bool attached){
        this.attached = attached;
    }

    public bool isAttached(){
        return attached;
    }
}