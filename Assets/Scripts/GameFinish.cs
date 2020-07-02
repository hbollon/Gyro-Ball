using UnityEngine;

public class GameFinish : MonoBehaviour {

    private void OnCollisionEnter(Collision other) {
        Ball ball = other.gameObject.GetComponent<Ball>();
        if((other.gameObject.tag == "Ball") && (!ball.IsAtEnd()) && (!ball.IsAttached())){
            ball.SetAtEnd(true);
            other.gameObject.transform.parent = gameObject.transform;
            ball.SetAttached(true);
            ball.Finish();
            LevelManager.Instance.NbBallsAtEnd++;
        }
    }
}