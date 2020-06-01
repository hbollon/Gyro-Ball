using UnityEngine;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour {
    public static SoundManager Instance { get; private set; }

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

        MusicSource.volume = 0.3f;
        AmbientSource.volume = 0.3f;
        EffectsSource.volume = 1f;
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

    public void StopAll(){
        MusicSource.Stop();
        AmbientSource.Stop();
        EffectsSource.Stop();
    }
}