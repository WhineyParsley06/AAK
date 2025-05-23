using UnityEngine;

// Manages game state (pause/resume) and UI visibility for pause menu buttons
public class GameManager : MonoBehaviour
{
    // Singleton instance to ensure global access to the GameManager
    public static GameManager Instance { get; private set; }

    public bool isPaused = false; // Tracks whether the game is currently paused

    // References to the Pause and Resume UI buttons
    public GameObject pauseButton, ResumeButton;

    // Awake runs before Start; ensures only one GameManager exists
    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject); // Keep this GameObject across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicates
        }
    }

    // Set initial button visibility when the game starts
    void Start()
    {
        if (pauseButton != null) pauseButton.SetActive(true);     // Show pause button
        if (ResumeButton != null) ResumeButton.SetActive(false);  // Hide resume button
    }

    // Currently unused, but available for future logic
    void Update()
    {

    }

    // Pauses the game and toggles the button visibility
    public void pauseGame()
    {
        isPaused = true;
        Time.timeScale = 0; // Freezes game time

        if (pauseButton != null) pauseButton.SetActive(false);    // Hide pause button
        if (ResumeButton != null) ResumeButton.SetActive(true);   // Show resume button
    }

    // Resumes the game and restores original button visibility
    public void resumeGame()
    {
        isPaused = false;
        Time.timeScale = 1; // Restores normal game speed

        if (pauseButton != null) pauseButton.SetActive(true);     // Show pause button
        if (ResumeButton != null) ResumeButton.SetActive(false);  // Hide resume button
    }
}
