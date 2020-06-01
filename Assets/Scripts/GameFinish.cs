using UnityEngine;

public class GameFinish : MonoBehaviour {

    public GameObject ui;
    public int nbBalls;
    private int nbBallsAtEnd;
    private GameObject[] balls;
    private bool levelFinished;
    private bool gameOver;

    private void Start() {
        nbBallsAtEnd = 0;
        levelFinished = false;
        gameOver = false;
        if (GameObject.FindWithTag("Ball"))
            balls = GameObject.FindGameObjectsWithTag("Ball");
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.F))
            nbBallsAtEnd = nbBalls;

        if(nbBallsAtEnd == nbBalls && !levelFinished){
            levelFinished = true;
            LevelManager.Instance.UnlockNextLevel();
            ui.GetComponent<UIManager>().HideInGame();
            ui.GetComponent<UIManager>().ShowFinished();
        }

        foreach (GameObject ball in balls){
            if((ball.GetComponent<Ball>().GetFalled() || Input.GetKeyDown(KeyCode.G)) && !gameOver){
                gameOver = true;
                ui.GetComponent<UIManager>().HideInGame();
                ui.GetComponent<UIManager>().ShowGameOver();
                ball.SetActive(false);
                break;
            }
        }
    }

    private void OnCollisionEnter(Collision other) {
        if((other.gameObject.tag == "Ball") && (!other.gameObject.GetComponent<Ball>().IsAtEnd()) && (!other.gameObject.GetComponent<Ball>().IsAttached())){
            other.gameObject.GetComponent<Ball>().SetAtEnd(true);
            other.gameObject.transform.parent = gameObject.transform;
            other.gameObject.GetComponent<Ball>().SetAttached(true);
            other.gameObject.GetComponent<Ball>().Finish();
            nbBallsAtEnd++;
        }
    }
}