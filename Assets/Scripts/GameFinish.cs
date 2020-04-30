using UnityEngine;

public class GameFinish : MonoBehaviour {

    public int nbBalls;
    //public int[] nbBall;
    private int nbBallsAtEnd;

    private void Start() {
        nbBallsAtEnd = 0;
    }

    private void OnCollisionEnter(Collision other) {
        if((other.gameObject.tag == "Ball") && (!other.gameObject.GetComponent<Ball>().isAtEnd()) && (!other.gameObject.GetComponent<Ball>().isAttached())){
            other.gameObject.GetComponent<Ball>().setAtEnd(true);

            print("Creating joint...");
            FixedJoint joint = gameObject.AddComponent<FixedJoint>(); 
            // sets joint position to point of contact
            joint.anchor = other.contacts[0].point; 
            // conects the joint to the other object
            joint.connectedBody = other.contacts[0].otherCollider.transform.GetComponentInParent<Rigidbody>(); 
            // Stops objects from continuing to collide and creating more joints
            joint.enableCollision = false; 
            print("Creating joint done !");
            
            nbBallsAtEnd++;
            other.gameObject.GetComponent<Ball>().setAttached(true);
            other.gameObject.GetComponent<Rigidbody>().useGravity = false;
        }
    }
}