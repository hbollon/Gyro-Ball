using UnityEngine;
using System;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour {
    public static SoundManager Instance { get; private set; }

    [System.Serializable] public enum AudioSourceComponant { Music, Ambient, Effects }

    // Audio players components.
	public AudioSource EffectsSource;
	public AudioSource AmbientSource;
	public AudioSource MusicSource;

    private AudioClip WoodAmbient;
    private AudioClip WoodMusic;
    private List<AudioClip> WoodTocSounds;

    private bool musicEnabled;
    private bool ambientEnabled;
    private bool soundEnabled;
    private int currentTheme = -1;

    private float musicVolumeReduce;
    private float ambientVolumeReduce;
    private float effectsVolumeReduce;


    private void Awake() {
        if (Instance == null){
            Instance = this;
            DontDestroyOnLoad(gameObject);

            musicEnabled = true;
            ambientEnabled = true;
            soundEnabled = true;
            InitSounds();
        } else {
            Destroy(gameObject);
        }
    }

    private void InitSounds(){
        WoodTocSounds = new List<AudioClip>();
        WoodTocSounds.Add(Resources.Load<AudioClip>("Sound/CollideSounds/LightToc1"));
        WoodTocSounds.Add(Resources.Load<AudioClip>("Sound/CollideSounds/LightToc2"));
        WoodTocSounds.Add(Resources.Load<AudioClip>("Sound/CollideSounds/LoudToc1"));
        WoodTocSounds.Add(Resources.Load<AudioClip>("Sound/CollideSounds/LoudToc2"));
        WoodTocSounds.Add(Resources.Load<AudioClip>("Sound/CollideSounds/MediumToc1"));
        WoodTocSounds.Add(Resources.Load<AudioClip>("Sound/CollideSounds/MediumToc2"));

        WoodMusic = Resources.Load<AudioClip>("Music/Siddhartha_Lightstream");
        WoodAmbient = Resources.Load<AudioClip>("Sound/Forest/ForestAmbient");

        musicVolumeReduce = 0.3f;
        ambientVolumeReduce = 0.3f;
        effectsVolumeReduce = 1f;

        if(PlayerPrefs.HasKey("MusicVolume")){
            MusicSource.volume = PlayerPrefs.GetFloat("MusicVolume") * musicVolumeReduce;
        }
        if(PlayerPrefs.HasKey("MusicMute")){
            MusicSource.mute = Convert.ToBoolean(PlayerPrefs.GetInt("MusicMute"));
        }

        if(PlayerPrefs.HasKey("AmbientVolume")){
            AmbientSource.volume = PlayerPrefs.GetFloat("AmbientVolume") * ambientVolumeReduce;
        }
        if(PlayerPrefs.HasKey("AmbientMute")){
            AmbientSource.mute = Convert.ToBoolean(PlayerPrefs.GetInt("AmbientMute"));
        }

        if(PlayerPrefs.HasKey("EffectsVolume")){
            EffectsSource.volume = PlayerPrefs.GetFloat("EffectsVolume") * effectsVolumeReduce;
        }
        if(PlayerPrefs.HasKey("EffectsMute")){
            EffectsSource.mute = Convert.ToBoolean(PlayerPrefs.GetInt("EffectsMute"));
        }
    }

    public void StartMusicWithTheme(int theme){
        if(theme != currentTheme){
            switch (theme)
            {
                case 0:
                case 1:
                    PlayMusic(WoodMusic);
                    PlayAmbient(WoodAmbient);
                    break;

                default:
                    break;
            }
            currentTheme = theme;
        }
    }

    public void PlayCollideSound(){
        System.Random rnd = new System.Random();
        switch (currentTheme)
        {
            case 0:
            case 1:
                Play(WoodTocSounds[rnd.Next(WoodTocSounds.Count)]);
                break;

            default:
                break;
        }
    }

    // Play a single clip through the sound effects source.
    public void Play(AudioClip clip)
    {
        if(soundEnabled){
            EffectsSource.clip = clip;
            EffectsSource.Play();
        }
    }

    // Play a single clip through the music source.
    public void PlayMusic(AudioClip clip)
    {
        if(musicEnabled){
            MusicSource.clip = clip;
            MusicSource.loop = true;
            MusicSource.Play();
        }
    }

    // Play a single clip through the ambient source.
    public void PlayAmbient(AudioClip clip)
    {
        if(ambientEnabled){
            AmbientSource.clip = clip;
            AmbientSource.loop = true;
            AmbientSource.Play();
        }
    }

    public void SetVolume(AudioSourceComponant src, float value){
        switch (src)
        {
            case AudioSourceComponant.Music:
                MusicSource.volume = value * musicVolumeReduce;
                PlayerPrefs.SetFloat("MusicVolume", value);
                break;
            case AudioSourceComponant.Ambient:
                AmbientSource.volume = value * ambientVolumeReduce;
                PlayerPrefs.SetFloat("AmbientVolume", value);
                break;
            case AudioSourceComponant.Effects:
                EffectsSource.volume = value * effectsVolumeReduce;
                PlayerPrefs.SetFloat("EffectsVolume", value);
                break;
            default:
                throw new ArgumentException("Invalid audio source");
        }
    }

    public void Mute(AudioSourceComponant src, bool value){
        switch (src)
        {
            case AudioSourceComponant.Music:
                MusicSource.mute = value;
                PlayerPrefs.SetInt("MusicMute", value ? 1 : 0);
                break;
            case AudioSourceComponant.Ambient:
                AmbientSource.mute = value;
                PlayerPrefs.SetInt("AmbientMute", value ? 1 : 0);
                break;
            case AudioSourceComponant.Effects:
                EffectsSource.mute = value;
                PlayerPrefs.SetInt("EffectsMute", value ? 1 : 0);
                break;
            default:
                throw new ArgumentException("Invalid audio source");
        }
    }

    public void MuteAll(bool value){
            MusicSource.mute = value;
            AmbientSource.mute = value;
            EffectsSource.mute = value;
    }
}