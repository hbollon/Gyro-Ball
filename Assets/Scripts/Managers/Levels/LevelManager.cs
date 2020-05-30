using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {
    public static LevelManager Instance { get; private set; }

    [System.Serializable] public class Level {
        public int levelNumber;
        public int levelChapter;
        public SceneReference levelScene;
        private int levelIndex;
        public bool unlocked;

        public int LevelIndex {
            get { return levelIndex; }
            set { levelIndex = value; }
        }
    }

    public List<Level> levels;
    public Level currentLevel;
    private int CurrentLevelIndex {
        get { return SceneManager.GetActiveScene().buildIndex; }
    }
    private Level CurrentLevel {
        get { return currentLevel; }
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.R))
            ResetProgression();
    }

    private void Awake() {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            InitLevels();
            LoadLevel(0);
        } else {
            Destroy(gameObject);
        }
    }

    private void InitLevels() {
        int progression = LoadProgression();
        print("Levels unlocked : " + progression);
        for(int i = 0; i<levels.Count; i++) {
            if(progression >= i)
                levels[i].unlocked = true;
            levels[i].LevelIndex = i;
        }
    }

    public void LoadLevel(int index) {
        if (index < levels.Count && index >= 0 && levels[index].unlocked) {
            currentLevel = levels[index];
            SceneManager.LoadScene(currentLevel.LevelIndex);
        }

    }

    public void NextLevel() {
        currentLevel = levels[currentLevel.LevelIndex + 1];
        SceneManager.LoadScene(currentLevel.levelScene.ScenePath);
        SaveProgression();
    }

    public void UnlockNextLevel(){
        if ((CurrentLevelIndex != SceneManager.sceneCountInBuildSettings) &&
            (currentLevel.levelNumber <= levels.Count)) {
            
            levels[currentLevel.LevelIndex + 1].unlocked = true;
            SaveProgression();
        }
    }

    public bool LevelIsUnlocked(int index){
        return levels[index].unlocked;
    }

    public Level GetLevel(int index){
        return levels[index];
    }

    public int GetProgression(){
        for(int i = 0; i<levels.Count; i++) {
            if(!LevelIsUnlocked(i))
                return i-1;
        }
        return levels.Count;
    }

    private void SaveProgression(){
        PlayerPrefs.SetInt("LevelsUnlocked", GetProgression());
    }

    private int LoadProgression(){
        if(PlayerPrefs.HasKey("LevelsUnlocked"))
            return PlayerPrefs.GetInt("LevelsUnlocked");
        else
            return 1;
    }

    private void ResetProgression(){
        if(PlayerPrefs.HasKey("LevelsUnlocked"))
            PlayerPrefs.DeleteKey("LevelsUnlocked");
    }
}