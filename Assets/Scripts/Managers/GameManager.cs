using System;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager Instance { get; private set; }

    private int quality;
    public int Quality {    
        get { return quality; } 
        private set { quality = value; }
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

    private void InitSettings()
    {
        if(PlayerPrefs.HasKey("Quality")){
            quality = PlayerPrefs.GetInt("Quality");
            QualitySettings.SetQualityLevel(quality, true);
        }
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
                Debug.Log("Quality settings set to " + quality);
                break;

            default:
                throw new ArgumentException("Invalid quality preset");
            }
    }
}