using UnityEngine;

public class SoundManager : MonoBehaviour {
    public static SoundManager Instance { get; private set; }

    // Audio players components.
	public AudioSource EffectsSource;
	public AudioSource AmbientSource;
	public AudioSource MusicSource;

    private bool musicEnabled;
    private bool ambientEnabled;
    private bool soundEnabled;
    private int currentTheme;


    private void Awake() {
        if (Instance == null){
            Instance = this;
            DontDestroyOnLoad(gameObject);

            musicEnabled = true;
            ambientEnabled = true;
            soundEnabled = true;
        } else {
            Destroy(gameObject);
        }
    }

    public void StartMusicWithTheme(int theme){
        if(theme != currentTheme){
            switch (theme)
            {
                case 0:
                case 1:
                    PlayMusic(Resources.Load<AudioClip>("Music/Siddhartha_Lightstream"));
                    PlayAmbient(Resources.Load<AudioClip>("Sound/Forest/ForestAmbient"));
                    break;

                default:
                    break;
            }
            currentTheme = theme;
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