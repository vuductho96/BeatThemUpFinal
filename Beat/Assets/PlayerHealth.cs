using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100; // Maximum health of the player
    public int currentHealth; // Current health of the player

    public Slider healthSlider; // Reference to the health slider UI element

    private void Start()
    {
        currentHealth = maxHealth; // Initialize current health to max health
        UpdateHealthBar(); // Update the health bar UI
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount; // Reduce current health by the damage amount

        // Check if the player's health has reached zero or below
        if (currentHealth <= 0)
        {
            Die();
        }

        UpdateHealthBar(); // Update the health bar UI
    }

    private void Die()
    {
        // Perform actions when the player dies, such as game over or respawn logic
        Debug.Log("Player has died!");
    }

    private void UpdateHealthBar()
    {
        if (healthSlider != null)
        {
            healthSlider.value = (float)currentHealth / maxHealth; // Update the health bar value
        }
    }
}
