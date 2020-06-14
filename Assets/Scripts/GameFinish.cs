using UnityEngine;
using System.Collections.Generic;

public class GameFinish : MonoBehaviour {

    public GameObject ui;
    public int nbBalls;
    private int nbBallsAtEnd;
    private List<Ball> balls;
    private bool levelFinished;
    private bool gameOver;
    private UIManager uiManager;

    private void Start() {
        nbBallsAtEnd = 0;
        levelFinished = false;
        gameOver = false;
        uiManager = ui.GetComponent<UIManager>();
        if (GameObject.FindWithTag("Ball")){
            GameObject[] ballsObjects = GameObject.FindGameObjectsWithTag("Ball");
            balls = new List<Ball>();
            foreach (GameObject ball in ballsObjects){
                balls.Add(ball.GetComponent<Ball>());
            }
        }
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.F))
            nbBallsAtEnd = nbBalls;

        if(nbBallsAtEnd == nbBalls && !levelFinished){
            levelFinished = true;
            LevelManager.Instance.UnlockNextLevel();
            uiManager.HideInGame();
            uiManager.ShowFinished();
        }

        foreach (Ball ball in balls){
            if((ball.GetComponent<Ball>().GetFalled() || Input.GetKeyDown(KeyCode.G)) && !gameOver){
                gameOver = true;
                uiManager.HideInGame();
                uiManager.ShowGameOver();
                ball.gameObject.SetActive(false);
                break;
            }
        }
    }

    private void OnCollisionEnter(Collision other) {
        Ball ball = other.gameObject.GetComponent<Ball>();
        if((other.gameObject.tag == "Ball") && (!ball.IsAtEnd()) && (!ball.IsAttached())){
            ball.SetAtEnd(true);
            other.gameObject.transform.parent = gameObject.transform;
            ball.SetAttached(true);
            ball.Finish();
            nbBallsAtEnd++;
        }
    }
}