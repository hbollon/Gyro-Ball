using UnityEngine;

public class GameFinish : MonoBehaviour {

    public int nbBalls;
    //public int[] nbBall;
    private int nbBallsAtEnd;

    private void Start() {
        nbBallsAtEnd = 0;
    }

    private void OnCollisionEnter(Collision other) {
        if((other.gameObject.tag == "Ball") && (!other.gameObject.GetComponent<Ball>().IsAtEnd()) && (!other.gameObject.GetComponent<Ball>().IsAttached())){
            other.gameObject.GetComponent<Ball>().SetAtEnd(true);
            other.gameObject.transform.parent = gameObject.transform;
            other.gameObject.GetComponent<Ball>().SetAttached(true);
            nbBallsAtEnd++;

            other.gameObject.GetComponent<Ball>().Finish();
        }
    }
}