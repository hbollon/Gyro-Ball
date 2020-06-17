using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.PostProcessing;
public class GameManager : MonoBehaviour {

    public static GameManager Instance { get; private set; }

    private Camera staticCamera;
    private Camera dynamicCamera;
    private GameObject landscapeCameraObj;
    private GameObject postProcessObj;

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

    private void Start() {
        Application.targetFrameRate = 60;
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
        landscapeCameraObj = GameObject.Find("Landscape Camera");
        postProcessObj = GameObject.Find("PostProcessing");

        if(PlayerPrefs.HasKey("PostProcess")){
            if(PlayerPrefs.GetInt("PostProcess") == 0)
                postProcessObj.SetActive(false);
        }
        if(PlayerPrefs.HasKey("Landscape")){
            if(PlayerPrefs.GetInt("Landscape") == 0){
                landscapeCameraObj.SetActive(false);
                ChangeCameraClearFlag(0);
            }
        }

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
                EnablePostProcessing(false);
                EnableLandscape(false);
                break;

            case 1:
                EnablePostProcessing(false);
                EnableLandscape(true);
                break;
            case 2:
            case 3:
            case 4:
            case 5:
                EnablePostProcessing(true);
                EnableLandscape(true);
                break;

            default:
                throw new ArgumentException("Invalid quality preset");
            }

            quality = preset;
            QualitySettings.SetQualityLevel(preset, true);
            PlayerPrefs.SetInt("Quality", preset);
            Debug.Log("Quality settings set to " + preset);
    }

    private void EnablePostProcessing(bool b){
        if(b){
            postProcessObj.SetActive(true);
            PlayerPrefs.SetInt("PostProcess", 1);
            Debug.Log("Post-Processing enabled !");
        } else {
            postProcessObj.SetActive(false);
            PlayerPrefs.SetInt("PostProcess", 0);
            Debug.Log("Post-Processing disabled !");
        }

    }

    private void EnableLandscape(bool b){
        if(b){
            landscapeCameraObj.SetActive(true);
            ChangeCameraClearFlag(1);
            PlayerPrefs.SetInt("Landscape", 1);
            Debug.Log("Landscape enabled !");
        } else {
            landscapeCameraObj.SetActive(false);
            ChangeCameraClearFlag(0);
            PlayerPrefs.SetInt("Landscape", 0);
            Debug.Log("Landscape disabled !");
        }
    }

    private void ChangeCameraClearFlag(int mode){
        if(mode == 0){
            staticCamera.clearFlags = CameraClearFlags.Skybox;
            dynamicCamera.clearFlags = CameraClearFlags.Skybox;
        } else {
            staticCamera.clearFlags = CameraClearFlags.Depth;
            dynamicCamera.clearFlags = CameraClearFlags.Depth;
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