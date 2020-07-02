using UnityEngine;

public class GameFinish : MonoBehaviour {

    public bool multipleBallsSupport = false;
    private int nbBallsIn = 0;

    private void OnCollisionEnter(Collision other) {
        if(multipleBallsSupport || nbBallsIn < 1) {
            Ball ball = other.gameObject.GetComponent<Ball>();
            if((other.gameObject.tag == "Ball") && (!ball.IsAtEnd()) && (!ball.IsAttached())){
                nbBallsIn++;
                ball.SetAtEnd(true);
                other.gameObject.transform.parent = gameObject.transform;
                ball.SetAttached(true);
                ball.Finish();
                LevelManager.Instance.NbBallsAtEnd++;
            }
        }
    }
}