using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] private AudioSource musicAudio;
    [SerializeField] private AudioMixer Master;

    
    public AudioClip GeneralMusic;





    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        PlayMusic(GeneralMusic);
    }

    private void Start()
    {
        musicAudio = transform.GetChild(0).GetComponent<AudioSource>();
       

    }



    public void PlayMusic(AudioClip clip)
    {
        if (musicAudio.clip == clip) return; // Evita reiniciar la misma música

        musicAudio.Stop();
        musicAudio.clip = clip;
        musicAudio.Play();
        musicAudio.loop = true;
    }



    public void MusicVolumeControl(float volume)
    {
        Master.SetFloat("Musica", volume);
    }

    
}
