using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager Instance { get; private set; }

    private Camera staticCamera;
    private Camera dynamicCamera;

    private int quality;
    public int Quality {    
        get { return quality; } 
        private set { quality = value; }
    }

    private int cameraMode;
    public int CameraMode {    
        get { return cameraMode; } 
        private set { cameraMode = value; }
    }

    private void Awake() {
        if (Instance == null){
            Instance = this;
            DontDestroyOnLoad(gameObject);

            InitSettings();
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
        staticCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        dynamicCamera = GameObject.Find("DynamicCamera").GetComponent<Camera>();
        ApplyCameraMode();
    }

    private void InitSettings()
    {
        if(PlayerPrefs.HasKey("Quality")){
            quality = PlayerPrefs.GetInt("Quality");
            QualitySettings.SetQualityLevel(quality, true);
        }
        if(PlayerPrefs.HasKey("CameraMode")){
            cameraMode = PlayerPrefs.GetInt("CameraMode");
        } else cameraMode = 0;
    }

    public void SetQualitySetting(int preset){
        switch(preset)
        {
            case 0:
            case 1:
            case 2:
            case 3:
            case 4:
            case 5:
                quality = preset;
                QualitySettings.SetQualityLevel(preset, true);
                PlayerPrefs.SetInt("Quality", preset);
                Debug.Log("Quality settings set to " + preset);
                break;

            default:
                throw new ArgumentException("Invalid quality preset");
            }
    }

    public void SetCameraMode(int mode){
        if(mode == 0 || mode == 1){
            cameraMode = mode;
            PlayerPrefs.SetInt("CameraMode", cameraMode);
            ApplyCameraMode();
        } else throw new ArgumentException("Invalid camera mode");
    }

    private void ApplyCameraMode(){
        if(cameraMode == 0){
            staticCamera.enabled = true;
            dynamicCamera.enabled = false;
        } else if (cameraMode == 1) {
            staticCamera.enabled = false;
            dynamicCamera.enabled = true;
        } else throw new Exception("Bad camera configuration");
    }
}