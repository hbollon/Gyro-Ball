using UnityEngine;

public class FollowPosition : MonoBehaviour {
    public GameObject objectToFollow;
    public bool followChildrenCenter = false;
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
        if(followChildrenCenter)
            currentPositionObject = CalculateCenter();
        else
            currentPositionObject = objectToFollow.transform.position;
    }

    private void Update() {
        if(followChildrenCenter) {
            Vector3 childrenCenter = CalculateCenter();
            if((currentPositionObject != childrenCenter) && (Time.timeScale == 1)){
                transform.position = new Vector3(
                    transform.position.x + (childrenCenter.x - currentPositionObject.x) * axisOffset.x,
                    transform.position.y + (childrenCenter.y - currentPositionObject.y) * axisOffset.y,
                    transform.position.z + (childrenCenter.z - currentPositionObject.z) * axisOffset.z
                );
                currentPositionObject = childrenCenter;
            }
        }
        else {
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

    private Vector3 CalculateCenter(){
        Transform[] children = objectToFollow.GetComponentsInChildren<Transform>();
        Vector3 center = new Vector3(0f, 0f, 0f);

        foreach(Transform child in children){
            center += child.transform.position;
        }
        center /= children.Length;

        return center;
    }
}