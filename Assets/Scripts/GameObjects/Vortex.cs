using UnityEngine;

public class Vortex : MonoBehaviour {
    public float velocity = 1f;

    private void OnTriggerStay(Collider other) {
        if(other.gameObject.GetComponent<Rigidbody>() != null && !other.gameObject.tag.Equals("Board")){
            Vector3 offset = transform.position - other.gameObject.transform.position;
            float magsqr = offset.sqrMagnitude;
            
            if(magsqr > 0.01f)
                other.gameObject.GetComponent<Rigidbody>().AddForce(velocity * offset.normalized / magsqr, ForceMode.Acceleration);
            else{
                if(other.gameObject.tag.Equals("Ball")) {
                    other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                    other.gameObject.GetComponent<Ball>().GameOver();
                }
            }
        }
    }
}