using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

    private Rigidbody m_Rigidbody;
    private bool atEnd;
    private bool attached;
    private float speedEndSeconds = 1.0f;

    public float minimalHeight = 0f;

    private void Start() {
        // Disable sleep for Rigidbody
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Rigidbody.sleepThreshold = 0.0f;
        atEnd = false;
        attached = false;
    }

    private void Update() {
        if(transform.position.y < minimalHeight)
            GameOver();
    }

    public void Finish() {
        GetComponent<Rigidbody>().useGravity = false;
        GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.Discrete;
        GetComponent<Rigidbody>().isKinematic = true;
            
        Vector3 endPosition = Vector3.zero;
        endPosition.y += 0.3f;
        StartCoroutine(MoveTo(endPosition));
    }

    public IEnumerator MoveTo(Vector3 destPosition)
    {
        float currentTime = 0.0f;
        float normalizedDelta;
        Vector3 srcPosition = transform.localPosition;
        Quaternion srcRotation = transform.rotation;
        while (currentTime < speedEndSeconds)
        {
            currentTime += Time.deltaTime;
            normalizedDelta = currentTime / speedEndSeconds;
            print("NormalizedDelta : " + normalizedDelta);
            transform.localPosition = Vector3.Lerp(srcPosition, destPosition, normalizedDelta);
            transform.rotation = Quaternion.Lerp(srcRotation, Quaternion.identity, normalizedDelta);
            
            yield return null;
        }
        transform.localPosition = destPosition;
        StartCoroutine(StartEndAnim());
        yield return null;
    }

    private IEnumerator StartEndAnim() {
        Vector3 initialPosition = transform.localPosition;
        float animHeight = 0.05f;
        float speed = 0.002f;
        int upDown = 1;
        for(;;){
            if(upDown == 1){
                transform.localPosition = new Vector3(transform.localPosition.x,
                                                transform.localPosition.y + speed, 
                                                transform.localPosition.z);
                transform.Rotate(0,60 * Time.deltaTime,0);
                if(transform.localPosition.y >= (initialPosition.y + animHeight))
                    upDown = 0;
            }
            else{
                transform.localPosition = new Vector3(transform.localPosition.x,
                                                transform.localPosition.y - speed, 
                                                transform.localPosition.z);
                transform.Rotate(0,60 * Time.deltaTime, 0);
                if(transform.localPosition.y <= (initialPosition.y - animHeight))
                    upDown = 1;
            }

            yield return null;
        }
    }

    private void GameOver(){
        Time.timeScale = 0;
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