using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class AudioManager : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseUI;
    public Slider slider;

    private void Start()
    {
        float savedVolume = PlayerPrefs.GetFloat("BGMusicVolume", 1f);
        slider.value = savedVolume;
        GetComponent<AudioSource>().volume = savedVolume;
    }

    private void Update()
    {
        // Remove the escape key condition

        if (GameIsPaused)
        {
            // Add the Save() method here
            Save();
        }
    }

    public void Resume()
    {
        pauseUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;

        // Save the BGMusicVolume to PlayerPrefs
        PlayerPrefs.SetFloat("BGMusicVolume", slider.value);
        PlayerPrefs.Save();
    }

    public void Pause()
    {
        pauseUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void SetBGMusicVolume(float volume)
    {
        // Assuming you have an AudioSource component attached to the AudioManager game object
        // You can change the volume using the value of the slider
        GetComponent<AudioSource>().volume = volume;
    }

    public void Save()
    {
        PlayerPrefs.SetFloat("BGMusicVolume", slider.value);
        PlayerPrefs.Save();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("StartGAME");
    }

}
