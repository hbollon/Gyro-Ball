using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

    private Rigidbody m_Rigidbody;
    private bool atEnd;
    private bool attached;
    private float speedEndSeconds = 0.6f;

    private void Start() {
        // Disable sleep for Rigidbody
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Rigidbody.sleepThreshold = 0.0f;
        atEnd = false;
        attached = false;
    }

    private void Update() {
        
    }

    public void Finish() {
        gameObject.GetComponent<Rigidbody>().useGravity = false;
        gameObject.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.Discrete;
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
            
        Vector3 endPosition = Vector3.zero;
        endPosition.y += 0.3f;
        StartCoroutine(MoveTo(endPosition));
    }

    public IEnumerator MoveTo(Vector3 destPosition)
    {
        float currentTime = 0.0f;
        float normalizedDelta;
        Vector3 srcPosition = gameObject.transform.localPosition;
        while (currentTime < speedEndSeconds)
        {
            currentTime += Time.deltaTime;
            normalizedDelta = currentTime / speedEndSeconds;
            print("NormalizedDelta : "+normalizedDelta);
            gameObject.transform.localPosition = Vector3.Lerp(srcPosition, destPosition, normalizedDelta);
            
            yield return null;
        }
        gameObject.transform.localPosition = destPosition;
        yield return null;
    }

    public void SetAtEnd(bool atEnd){
        this.atEnd = atEnd;
    }

    public bool IsAtEnd(){
        return atEnd;
    }

    public void SetAttached(bool attached){
        this.attached = attached;
    }

    public bool IsAttached(){
        return attached;
    }
}