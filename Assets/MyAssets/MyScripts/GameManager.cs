using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public bool isPaused = false;
    public GameObject pauseButton, ResumeButton;

    public void Awake()
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
    }
    void Start()
    {
        if (pauseButton != null) pauseButton.SetActive(true);
        if (ResumeButton != null) ResumeButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void pauseGame()
    {   

        isPaused = true;
        Time.timeScale = 0;
        if (pauseButton != null) pauseButton.SetActive(false);
        if (ResumeButton != null) ResumeButton.SetActive(true);
    }
    public void resumeGame()
    {
        isPaused = false;
        Time.timeScale = 1;
        if (pauseButton != null) pauseButton.SetActive(true);
        if (ResumeButton != null) ResumeButton.SetActive(false);
    }
}
