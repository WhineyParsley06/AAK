using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Handles main menu functionality, including music volume, button sound, and scene loading
public class Main_Menu_Script : MonoBehaviour
{
    // Reference to the UI Slider for adjusting music volume
    [SerializeField] public Slider SliderMusic;

    // Called at the start of the scene
    void Start()
    {
        // If the slider is assigned, attach a listener to update the music volume when its value changes
        if (SliderMusic != null)
        {
            SliderMusic.onValueChanged.AddListener(UpdateMusicVolume);
        }
    }

    // Update is not used here but kept for possible future use
    void Update()
    {

    }

    // Plays a sound effect when any button is clicked
    public void PlayButtonsound()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.SoundEfectButtons);
    }

    // Loads the main game scene when the Play button is pressed
    public void PlayGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    // Updates the music volume based on the slider's value
    public void UpdateMusicVolume(float value)
    {
        if (SliderMusic != null)
        {
            // Calls the method in AudioManager to set the volume
            AudioManager.Instance.MusicVolumeControl(value);
        }
    }
}
