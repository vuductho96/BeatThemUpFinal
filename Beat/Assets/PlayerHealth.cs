using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
   public int currentHealth;

    public Slider healthBar;

    public void Start()
    {
        currentHealth = maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.value = currentHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        healthBar.value = currentHealth;

        if (currentHealth <= 0)
        {
            // Player is defeated or any other game over logic
        }
    }
}
