using System;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager Instance { get; private set; }

    private int quality;
    public int Quality { get; private set;}

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
        if(PlayerPrefs.HasKey("Quality"))
            SetQualitySetting(PlayerPrefs.GetInt("Quality"));
    }

    public void SetQualitySetting(int preset){
        switch(preset)
        {
            case 0:
                QualitySettings.SetQualityLevel(preset, true);
                PlayerPrefs.SetInt("Quality", preset);
                Debug.Log("Quality settings set to 'Very Low'");
                break;
            case 1:
                QualitySettings.SetQualityLevel(preset, true);
                PlayerPrefs.SetInt("Quality", preset);
                Debug.Log("Quality settings set to 'Low'");
                break;
            case 2:
                QualitySettings.SetQualityLevel(preset, true);
                PlayerPrefs.SetInt("Quality", preset);
                Debug.Log("Quality settings set to 'Medium'");
                break;
            case 3:
                QualitySettings.SetQualityLevel(preset, true);
                PlayerPrefs.SetInt("Quality", preset);
                Debug.Log("Quality settings set to 'High'");
                break;
            case 4:
                QualitySettings.SetQualityLevel(preset, true);
                PlayerPrefs.SetInt("Quality", preset);
                Debug.Log("Quality settings set to 'Very High'");
                break;
            case 5:
                QualitySettings.SetQualityLevel(preset, true);
                PlayerPrefs.SetInt("Quality", preset);
                Debug.Log("Quality settings set to 'Ultra'");
                break;
            default:
                throw new ArgumentException("Invalid quality preset");
            }
    }
}