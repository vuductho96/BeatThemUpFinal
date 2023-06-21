using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class AudioManger : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseUI;
    public Slider slider;
    private float originalTimeScale; // Store the original time scale value

    private void Start()
    {
        float savedVolume = PlayerPrefs.GetFloat("BGMusicVolume", 1f);
        slider.value = savedVolume;
        GetComponent<AudioSource>().volume = savedVolume;

        originalTimeScale = Time.timeScale; // Store the original time scale value
    }

    private void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseUI.SetActive(false);
        Time.timeScale = originalTimeScale; // Reset the time scale to its original value
        GameIsPaused = false;
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
        float volume = slider.value;
        PlayerPrefs.SetFloat("BGMusicVolume", volume);
        PlayerPrefs.Save();
        SetBGMusicVolume(volume);

       
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("StartGAME");
    }
}
