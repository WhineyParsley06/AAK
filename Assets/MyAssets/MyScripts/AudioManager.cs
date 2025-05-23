using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

// Handles music and sound effects across the game
public class AudioManager : MonoBehaviour
{
    // Singleton instance for global access
    public static AudioManager Instance { get; private set; }

    // AudioSources for music and sound effects
    [SerializeField] private AudioSource musicAudio, sfxAudio;

    // Reference to the main AudioMixer
    [SerializeField] private AudioMixer Master;

    // Audio clips assigned in the Inspector
    [SerializeField] public AudioClip GeneralMusic, SoundEfectButtons, ButtonPressedCLip, JumpSound;

    // Ensures only one AudioManager exists (Singleton pattern)
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject); // Persist across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate
        }

        // Play background music at startup
        PlayMusic(GeneralMusic);
    }

    // Assign AudioSources from child objects
    private void Start()
    {
        musicAudio = transform.GetChild(0).GetComponent<AudioSource>();
        sfxAudio = transform.GetChild(1).GetComponent<AudioSource>();
    }

    // Plays a one-shot sound effect (doesn't interrupt other sounds)
    public void PlaySFX(AudioClip clip)
    {
        sfxAudio.PlayOneShot(clip);
    }

    // Plays background music (only if it's different from the current clip)
    public void PlayMusic(AudioClip clip)
    {
        if (musicAudio.clip == clip) return; // avoid restarting same track

        musicAudio.Stop();
        musicAudio.clip = clip;
        musicAudio.Play();
        musicAudio.loop = true; // ensures music loops
    }

    // Sets the music volume using the exposed parameter from AudioMixer
    public void MusicVolumeControl(float volume)
    {
        Master.SetFloat("MusicVol", volume);
    }
}
