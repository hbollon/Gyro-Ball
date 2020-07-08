using UnityEngine;

public class AntiGrav : MonoBehaviour {
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ball"){
            other.gameObject.GetComponent<Gravity>().magnitude *= -1; 
        }
    }
}