using System;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;

public class AdsManager : MonoBehaviour, IUnityAdsListener { 

    public static AdsManager Instance { get; private set; }

    #if UNITY_IOS
    private string gameId = "3698844";
    #elif UNITY_ANDROID
    private string gameId = "3698845";
    #endif

    private int consecutivesLevelsCount;
    public int ConsecutivesLevels { 
        get {
            return consecutivesLevelsCount;
        }
        set {
            if (value < 0)
                throw new ArgumentException(
                   $"{nameof(value)} must be positive.");

            consecutivesLevelsCount = value;
            PlayerPrefs.SetInt("ConsecutivesLevels", value);
            Debug.Log("ConsecutivesLevels : " + value);
        }
    }

    private string rewardedPlacementId = "rewardedVideo";
    private string videoPlacementId = "video";

    public int adsLevelsFrequency = 10;
    public bool testMode = true;

    private void Awake() {
        if (Instance == null){
            Instance = this;
            DontDestroyOnLoad(gameObject);

            if(PlayerPrefs.HasKey("ConsecutivesLevels"))
                ConsecutivesLevels = PlayerPrefs.GetInt("ConsecutivesLevels");

        } else {
            Destroy(gameObject);
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
        if(ConsecutivesLevels >= adsLevelsFrequency){
            PlayAd(videoPlacementId);
        }
    }

    private void Start() {
        Advertisement.AddListener(this);
        Advertisement.Initialize(gameId, testMode);
    }

    public void PlayAd(string placementId){
        Advertisement.Show(placementId);
    } 

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult) {
        if (showResult == ShowResult.Finished) {
            if(placementId.Equals(rewardedPlacementId))
                LevelManager.Instance.NextLevel();
            else if(placementId.Equals(videoPlacementId))
                ConsecutivesLevels = 0;
            Debug.Log("The ad was watched completelly");
        } else if (showResult == ShowResult.Skipped) {
            if(placementId.Equals(videoPlacementId))
                ConsecutivesLevels = 0;
            Debug.Log("The ad was skipped");
        } else if (showResult == ShowResult.Failed) {
            Debug.LogWarning ("The ad did not finish due to an error");
        }
    }

    public void OnUnityAdsReady (string placementId) {
        
    }

    public void OnUnityAdsDidError (string message) {
        
    }

    public void OnUnityAdsDidStart (string placementId) {
        
    } 

    public void OnDestroy() {
        Advertisement.RemoveListener(this);
    }

    void OnGUI () {
        if(testMode){
            GUIStyle style = new GUIStyle();
            style.fontSize = Mathf.RoundToInt(Mathf.Min(Screen.width, Screen.height) / 20f);
            style.normal.textColor = Color.white;
            GUILayout.BeginVertical("box");
            GUILayout.Label("Consecutives levels: " + ConsecutivesLevels, style);
            GUILayout.EndVertical();
        }
    }
}