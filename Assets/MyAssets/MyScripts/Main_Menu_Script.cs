using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Main_Menu_Script : MonoBehaviour
{
    

    [SerializeField] public Slider SliderMusic;
    void Start()
    {
        if (SliderMusic != null)
        {
            SliderMusic.onValueChanged.AddListener(UpdateMusicVolume);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void UpdateMusicVolume(float value)
    {

        if (SliderMusic != null)
        {
            AudioManager.Instance.MusicVolumeControl(value); // Actualizamos el volumen de la música
        }
    }
}
