using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    public GameObject gui;
    private GameObject[] pauseObjects;
    private GameObject[] levelSelectorObjects;
    private List<GameObject> levelSelectorPanels;
    private GameObject[] skipObjects;
    private GameObject[] helpObjects;
    private GameObject[] endObjects;
    private GameObject[] settingsObjects;
    private GameObject[] inGameObjects;
    private GameObject[] gameOverObjects;
    private GameObject[] finishObjects;
    private GameObject[] ballsController;

    private bool pauseMenuOpen;
    private bool gameOverMenuOpen;
    private bool finishMenuOpen;
    private int previousView;

    // Initialization
    void Start()
    {
        Time.timeScale = 1;

        pauseObjects = GameObject.FindGameObjectsWithTag("OnPauseUI");          //gets all objects with tag OnPauseUI
        levelSelectorObjects = GameObject.FindGameObjectsWithTag("LevelSelector"); //gets all objects with tag LevelSelector
        skipObjects = GameObject.FindGameObjectsWithTag("SkipConfirmationUI"); //gets all objects with tag SkipConfirmationUI
        helpObjects = GameObject.FindGameObjectsWithTag("HelpMessageUI"); //gets all objects with tag SkipConfirmationUI
        endObjects = GameObject.FindGameObjectsWithTag("EndMessageUI"); //gets all objects with tag SkipConfirmationUI
        settingsObjects = GameObject.FindGameObjectsWithTag("SettingsUI"); //gets all objects with tag SettingsUI
        finishObjects = GameObject.FindGameObjectsWithTag("OnFinishUI");        //gets all objects with tag OnFinishUI
        gameOverObjects = GameObject.FindGameObjectsWithTag("OnGameOverUI");      //gets all objects with tag OnGameOverUI
        inGameObjects = GameObject.FindGameObjectsWithTag("InGameUI");          //gets all objects with tag InGameUI

        levelSelectorPanels = new List<GameObject>();
        levelSelectorPanels.Add(GameObject.Find("PanelContent1"));
        levelSelectorPanels.Add(GameObject.Find("PanelContent2"));
        levelSelectorPanels.Add(GameObject.Find("PanelContent3"));

        HidePaused();
        HideLevelSelector();
        HideSettings();
        HideGameOver();
        HideFinished();
        HideSkipLevel();
        HideEndMessage();
        ShowInGame();

        if(!PlayerPrefs.HasKey("FirstLaunch")){
            HelpUI();
        } else HideHelpMessage();

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
        else if(Input.GetKeyDown(KeyCode.N))
            NextLevel();
    }


    //Reloads the Level
    public void Reload()
    {
        AdsManager.Instance.ConsecutivesLevels++;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void SkipLevel(){
        AdsManager.Instance.PlayRewardedAd();
        HideSkipLevel();
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

    public void SkipUI()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            ShowSkipLevel();
        }
        else if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            HideSkipLevel();
        }
    }

    public void HelpUI()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            ShowHelpMessage();
        }
        else if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            HideHelpMessage();

            if(!PlayerPrefs.HasKey("FirstLaunch"))
                PlayerPrefs.SetInt("FirstLaunch", 1);
        }
    }

    public void EndUI()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            ShowEndMessage();
        }
        else if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            HideEndMessage();
            Reload();
        }
    }

    // show level selector screen from pause menu
    public void LevelSelectorOpen(){
        print("LevelSelectorOpen()");
        if(pauseMenuOpen){
            print("LevelSelectorOpen() : pauseMenuOpen");
            HidePaused();
            previousView = 1;
        }
        else if(gameOverMenuOpen){
            print("LevelSelectorOpen() : gameOverMenuOpen");
            HideGameOver();
            previousView = 2;
        }
        else if(finishMenuOpen){
            print("LevelSelectorOpen() : finishMenuOpen");
            HideFinished();
            previousView = 3;
        }

        ShowLevelSelector();
    }

    // back from level selector screen
    public void LevelSelectorBack(){
        HideLevelSelector();
        switch (previousView)
        {
            case 1:
                ShowPaused();
                break;
            case 2:
                ShowGameOver();
                break;
            case 3:
                ShowFinished();
                break;
            default:
                break;
        }
    }

    //shows objects with OnPauseUI tag
    public void ShowPaused()
    {
        foreach (GameObject g in pauseObjects)
        {
            g.SetActive(true);
        }
        pauseMenuOpen = true;
    }

    //hides objects with OnPauseUI tag
    public void HidePaused()
    {
        foreach (GameObject g in pauseObjects)
        {
            g.SetActive(false);
        }
        pauseMenuOpen = false;
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

    //shows objects with HelpMessageUI tag
    public void ShowHelpMessage()
    {
        foreach (GameObject g in helpObjects)
        {
            g.SetActive(true);
        }
    }

    //hides objects with HelpMessageUI tag
    public void HideHelpMessage()
    {
        foreach (GameObject g in helpObjects)
        {
            g.SetActive(false);
        }
    }

    //shows objects with SkipConfirmationUI tag
    public void ShowSkipLevel()
    {
        foreach (GameObject g in skipObjects)
        {
            g.SetActive(true);
        }
    }

    //hides objects with SkipConfirmationUI tag
    public void HideSkipLevel()
    {
        foreach (GameObject g in skipObjects)
        {
            g.SetActive(false);
        }
    }

    //shows objects with EndMessageUI tag
    public void ShowEndMessage()
    {
        foreach (GameObject g in endObjects)
        {
            g.SetActive(true);
        }
    }

    //hides objects with EndMessageUI tag
    public void HideEndMessage()
    {
        foreach (GameObject g in endObjects)
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
        finishMenuOpen = true;
    }

    //hides objects with OnFinishUI tag
    public void HideFinished()
    {
        foreach (GameObject g in finishObjects)
        {
            g.SetActive(false);
        }
        finishMenuOpen = false;
    }

    //shows objects with OnGameOverUI tag
    public void ShowGameOver()
    {
        print("ShowGameOver()");
        foreach (GameObject g in gameOverObjects)
        {
            g.SetActive(true);
        }
        gameOverMenuOpen = true;
    }

    //hides objects with OnGameOverUI tag
    public void HideGameOver()
    {
        print("HideGameOver()");
        foreach (GameObject g in gameOverObjects)
        {
            g.SetActive(false);
        }
        gameOverMenuOpen = false;
    }

    //shows objects with LevelSelector tag
    public void ShowLevelSelector()
    {
        print("ShowLevelSelector()");
        foreach (GameObject g in levelSelectorObjects)
        {
            g.SetActive(true);
        }
        ChangeLevelPage(1);
    }

    public void ChangeLevelPage(int page){
        foreach(GameObject panel in levelSelectorPanels){
            panel.SetActive(false);
        }
        if(levelSelectorObjects[0].activeSelf){
            switch(page){
                case 1: 
                case 2:
                case 3:
                    levelSelectorPanels[page-1].SetActive(true);
                    break;

                default: 
                    break;
            }
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

    //shows objects with SettingsUI tag
    public void ShowSettings()
    {
        foreach (GameObject g in settingsObjects)
        {
            g.SetActive(true);
        }
    }

    //hides objects with SettingsUI tag
    public void HideSettings()
    {
        foreach (GameObject g in settingsObjects)
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

    /************************/
    /* Settings screen part */
    /************************/

    public void SetQuality(int preset) {
        GameManager.Instance.SetQualitySetting(preset);
    }

    public void SetCameraMode(int mode) {
        GameManager.Instance.SetCameraMode(mode);
    }

    public void MuteMusic(bool value){
        SoundManager.Instance.Mute(SoundManager.AudioSourceComponant.Music, value);
    }

    public void MuteAmbient(bool value){
        SoundManager.Instance.Mute(SoundManager.AudioSourceComponant.Ambient, value);
    }

    public void MuteEffects(bool value){
        SoundManager.Instance.Mute(SoundManager.AudioSourceComponant.Effects, value);
    }

    public void MuteAll(bool value){
        SoundManager.Instance.MuteAll(value);
    }

    public void SetMusicVolume(float value){
        SoundManager.Instance.SetVolume(SoundManager.AudioSourceComponant.Music, value);
    }

    public void SetAmbientVolume(float value){
        SoundManager.Instance.SetVolume(SoundManager.AudioSourceComponant.Ambient, value);
    }

    public void SetEffectsVolume(float value){
        SoundManager.Instance.SetVolume(SoundManager.AudioSourceComponant.Effects, value);
    }
}