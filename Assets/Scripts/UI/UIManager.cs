using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject gui;
    private GameObject[] pauseObjects;
    private GameObject[] levelSelectorObjects;
    private GameObject[] inGameObjects;
    private GameObject[] gameOverObjects;
    private GameObject[] finishObjects;
    private GameObject[] ballsController;

    // Initialization
    void Start()
    {
        Time.timeScale = 1;

        gui.SetActive(true);

        pauseObjects = GameObject.FindGameObjectsWithTag("OnPauseUI");          //gets all objects with tag OnPauseUI
        levelSelectorObjects = GameObject.FindGameObjectsWithTag("LevelSelector"); //gets all objects with tag LevelSelector
        finishObjects = GameObject.FindGameObjectsWithTag("OnFinishUI");        //gets all objects with tag OnFinishUI
        gameOverObjects = GameObject.FindGameObjectsWithTag("OnGameOverUI");      //gets all objects with tag OnGameOverUI
        inGameObjects = GameObject.FindGameObjectsWithTag("InGameUI");          //gets all objects with tag InGameUI

        HidePaused();
        HideLevelSelector();
        HideGameOver();
        HideFinished();
        ShowInGame();

        if (GameObject.FindWithTag("Ball"))
            ballsController = GameObject.FindGameObjectsWithTag("Ball");
    }

    // Update
    void Update()
    {
        //uses the p button to pause and unpause the game
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (Time.timeScale == 1)
            {
                Time.timeScale = 0;
                ShowPaused();
            }
            else if (Time.timeScale == 0)
            {
                Time.timeScale = 1;
                HidePaused();
            }
        }
    }


    //Reloads the Level
    public void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //controls the pausing of the scene
    public void PauseControl()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            ShowPaused();
            HideInGame();
        }
        else if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            HidePaused();
            ShowInGame();
        }
    }

    // show level selector screen from pause menu
    public void LevelSelectorOpenFromPause(){
        print("LevelSelectorOpenFromPause()");
        HidePaused();
        ShowLevelSelector();
    }

    // back to pause menu from level selector screen
    public void LevelSelectorback(){
        HideLevelSelector();
        ShowPaused();
    }

    //shows objects with OnPauseUI tag
    public void ShowPaused()
    {
        foreach (GameObject g in pauseObjects)
        {
            g.SetActive(true);
        }
    }

    //hides objects with OnPauseUI tag
    public void HidePaused()
    {
        print("HidePaused()");
        foreach (GameObject g in pauseObjects)
        {
        print(g.name);
            g.SetActive(false);
        }
    }

    //shows objects with InGameUI tag
    public void ShowInGame()
    {
        foreach (GameObject g in inGameObjects)
        {
            g.SetActive(true);
        }
    }

    //hides objects with InGameUI tag
    public void HideInGame()
    {
        foreach (GameObject g in inGameObjects)
        {
            g.SetActive(false);
        }
    }

    //shows objects with OnFinishUI tag
    public void ShowFinished()
    {
        foreach (GameObject g in finishObjects)
        {
            g.SetActive(true);
        }
    }

    //hides objects with OnFinishUI tag
    public void HideFinished()
    {
        foreach (GameObject g in finishObjects)
        {
            g.SetActive(false);
        }
    }

    //shows objects with OnGameOverUI tag
    public void ShowGameOver()
    {
        foreach (GameObject g in gameOverObjects)
        {
            g.SetActive(true);
        }
    }

    //hides objects with OnGameOverUI tag
    public void HideGameOver()
    {
        foreach (GameObject g in gameOverObjects)
        {
            g.SetActive(false);
        }
    }

    //shows objects with LevelSelector tag
    public void ShowLevelSelector()
    {
        foreach (GameObject g in levelSelectorObjects)
        {
            g.SetActive(true);
        }
    }

    //hides objects with LevelSelector tag
    public void HideLevelSelector()
    {
        foreach (GameObject g in levelSelectorObjects)
        {
            g.SetActive(false);
        }
    }

    //loads inputted level
    public void LoadLevel(int levelIndex)
    {
        LevelManager.Instance.LoadLevel(levelIndex);
    }

    public void NextLevel(){
        LevelManager.Instance.NextLevel();
    }
}