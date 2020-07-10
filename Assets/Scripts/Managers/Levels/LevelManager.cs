using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {
    public static LevelManager Instance { get; private set; }
    public int NbBallsAtEnd { get; set; }
    private bool levelFinished;
    private bool gameOver;
    private List<Ball> balls;
    private UIManager uiInstance;

    [SerializeField] private bool unlockAll = false;

    [System.Serializable] public class Level {
        public int levelNumber;
        public int levelChapter;
        public SceneReference levelScene;
        public int nbBalls;
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

    private void Start() {
        LoadLevel(0);
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.R))
            ResetProgression();

        if(Input.GetKeyDown(KeyCode.F))
            NbBallsAtEnd = currentLevel.nbBalls;

        if(NbBallsAtEnd == currentLevel.nbBalls && !levelFinished){
            levelFinished = true;
            UnlockNextLevel();
            uiInstance.HideInGame();
            uiInstance.ShowFinished();
        }

        foreach (Ball ball in balls){
            if(ball != null && (ball.GetComponent<Ball>().GetFalled() || Input.GetKeyDown(KeyCode.G)) && !gameOver){
                gameOver = true;
                uiInstance.HideInGame();
                uiInstance.ShowGameOver();
                ball.gameObject.SetActive(false);
                break;
            }
        }
        
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    private void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        NbBallsAtEnd = 0;
        levelFinished = false;
        gameOver = false;
        if(GameObject.Find("UIManager").GetComponent<UIManager>() != null)
            uiInstance = GameObject.Find("UIManager").GetComponent<UIManager>();
        if (GameObject.FindWithTag("Ball")){
            GameObject[] ballsObjects = GameObject.FindGameObjectsWithTag("Ball");
            balls = new List<Ball>();
            foreach (GameObject ball in ballsObjects){
                balls.Add(ball.GetComponent<Ball>());
            }
        }
    }

    private void Awake() {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            InitLevels();
        } else {
            Destroy(gameObject);
        }
    }

    private void InitLevels() {
        int progression = LoadProgression();
        print("Levels unlocked : " + progression);
        for(int i = 0; i<levels.Count; i++) {
            if(progression >= i || unlockAll)
                levels[i].unlocked = true;
            levels[i].LevelIndex = i;
        }
    }

    public void LoadLevel(int index) {
        if (index < levels.Count && index >= 0 && levels[index].unlocked) {
            AdsManager.Instance.ConsecutivesLevels++;
            currentLevel = levels[index];
            SceneManager.LoadScene(currentLevel.LevelIndex);
            SoundManager.Instance.StartMusicWithTheme(currentLevel.levelChapter);
        }

    }

    public void NextLevel() {
        AdsManager.Instance.ConsecutivesLevels++;
        currentLevel = levels[currentLevel.LevelIndex + 1];
        SceneManager.LoadScene(currentLevel.levelScene.ScenePath);
        SoundManager.Instance.StartMusicWithTheme(currentLevel.levelChapter);
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