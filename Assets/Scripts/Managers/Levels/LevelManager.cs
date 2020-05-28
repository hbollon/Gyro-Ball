using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelManager {
    private int CurrentLevelIndex { 
        get { return SceneManager.GetActiveScene().buildIndex; } 
    }
    private static int unlockedLevelIndex;
    public int UnlockedLevelIndex{
        get { return unlockedLevelIndex; }
        set { UnlockedLevelIndex = value; }
    }

    public LevelManager() {
        unlockedLevelIndex = 0;
    }

    public void LoadLevel(int index){
        if(index <= unlockedLevelIndex && index >= 0)
            SceneManager.LoadScene(index);
    }

    public void NextLevel(){
        if(CurrentLevelIndex != SceneManager.sceneCountInBuildSettings){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            unlockedLevelIndex++;
        }
    }
}