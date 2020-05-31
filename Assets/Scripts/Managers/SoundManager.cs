using UnityEngine;

public class SoundManager : MonoBehaviour {
    public static SoundManager Instance { get; private set; }

    // Audio players components.
	public AudioSource EffectsSource;
	public AudioSource MusicSource;

	// Random pitch adjustment range.
	public float LowPitchRange = .95f;
	public float HighPitchRange = 1.05f;

    private void Awake() {
        if (Instance == null){
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    public void StartMusicWithTheme(int theme){
        switch (theme)
        {
            case 0:
            case 1:
                

            default:
                break;
        }
    }

    // Play a single clip through the sound effects source.
    public void Play(AudioClip clip)
    {
        EffectsSource.clip = clip;
        EffectsSource.Play();
    }

    // Play a single clip through the music source.
    public void PlayMusic(AudioClip clip)
    {
        MusicSource.clip = clip;
        MusicSource.loop = true;
        MusicSource.Play();
    }
}