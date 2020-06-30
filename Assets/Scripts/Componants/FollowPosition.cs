using UnityEngine;

public class FollowPosition : MonoBehaviour {
    public GameObject objectToFollow;
    public bool followX = false;
    public bool followY = false;
    public bool followZ = false;

    private Vector3 currentPositionObject;
    private Vector3 axisOffset;

    private void Start() {
        axisOffset = new Vector3(
            followX ? 1f : 0f,
            followY ? 1f : 0f,
            followZ ? 1f : 0f
        );
        currentPositionObject = objectToFollow.transform.position;
    }

    private void Update() {
        if((currentPositionObject != objectToFollow.transform.position) && (Time.timeScale == 1)){
            transform.position = new Vector3(
                transform.position.x + (objectToFollow.transform.position.x - currentPositionObject.x) * axisOffset.x,
                transform.position.y + (objectToFollow.transform.position.y - currentPositionObject.y) * axisOffset.y,
                transform.position.z + (objectToFollow.transform.position.z - currentPositionObject.z) * axisOffset.z
            );
            currentPositionObject = objectToFollow.transform.position;
        }
    }
}