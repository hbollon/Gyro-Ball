using UnityEngine;
using System.Collections;

public class KeyboardRotationControl : MonoBehaviour
{

    private Quaternion OriginalRotation;
    private Quaternion CurrentRotation;

    void Start()
    {
        OriginalRotation = gameObject.transform.rotation;
    }

    IEnumerator RotateMe(Vector3 byAngles, float inTime) 
     {    
         var fromAngle = transform.rotation;
         var toAngle = Quaternion.Euler(transform.eulerAngles + byAngles);
         for(var t = 0f; t < 1; t += Time.deltaTime/inTime) {
             transform.rotation = Quaternion.Slerp(fromAngle, toAngle, t);
             yield return null;
         }
     }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            print("up arrow key is held down");
            StartCoroutine(RotateMe(Vector3.up * 1, 0.8f));
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            print("down arrow key is held down");
            StartCoroutine(RotateMe(Vector3.down * 1, 0.8f));
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            print("right arrow key is held down");
            StartCoroutine(RotateMe(Vector3.right * 1, 0.8f));
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            print("left arrow key is held down");
            StartCoroutine(RotateMe(Vector3.left * 1, 0.8f));
        }
    }
}