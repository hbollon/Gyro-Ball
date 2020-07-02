using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class Boost : MonoBehaviour
{

    public float velocity = 4f;
    public bool addJump = false;

    [HideInInspector]
    public bool differentJumpVelocity = false;

    [HideInInspector]
    public float jumpVelocity = 1f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ball"){
            if(!differentJumpVelocity)
                other.gameObject.GetComponent<Rigidbody>().AddForce(
                    new Vector3(velocity, velocity * (addJump ? 1 : 0), 0f),
                    ForceMode.Impulse
                );
            else
                other.gameObject.GetComponent<Rigidbody>().AddForce(
                    new Vector3(velocity, jumpVelocity, 0f),
                    ForceMode.Impulse
                );

        }
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(Boost))]
public class BoostEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Boost boostScript = (Boost)target;

        if (boostScript.addJump){
            var differentJumpVelocityProperty = serializedObject.FindProperty("differentJumpVelocity");
            EditorGUILayout.PropertyField(differentJumpVelocityProperty);
            serializedObject.ApplyModifiedProperties();

            if(boostScript.differentJumpVelocity){
                var jumpVelocityProperty = serializedObject.FindProperty("jumpVelocity");
                EditorGUILayout.PropertyField(jumpVelocityProperty);
                serializedObject.ApplyModifiedProperties();
            }
        }
    }
}
#endif