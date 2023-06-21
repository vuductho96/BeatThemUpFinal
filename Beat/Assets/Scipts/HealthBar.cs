using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarw : MonoBehaviour
{
    public Slider slider;
    public PlayerControl_Animation player;

    private void Start()
    {
        // Get reference to the PlayerControl_Animation component
        player = FindObjectOfType<PlayerControl_Animation>();

        // Set the maximum value of the slider to match the player's max health
        slider.maxValue = player.maxHealth;
    }

    private void Update()
    {
        // Update the slider value to reflect the player's current health
        slider.value = player.currentHealth;
    }

    public void TakeDamage(float damageAmount)
    {
        // Call the TakeDamage method on the player to inflict damage
        player.TakeDamage(damageAmount);
    }
}
