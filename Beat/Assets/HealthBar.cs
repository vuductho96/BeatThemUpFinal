using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public PlayerHealth playerHealth;

    private void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();

        // Set the maximum value of the slider to match the player's max health
        slider.maxValue = playerHealth.maxHealth;
    }

    private void Update()
    {
        // Update the slider value to reflect the player's current health
        slider.value = playerHealth.currentHealth;
    }
}
