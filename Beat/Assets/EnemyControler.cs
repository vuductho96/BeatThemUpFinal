using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControler : MonoBehaviour
{
    public float damageAmount = 10f;
    // Amount of damage inflicted on the player

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerControl_Animation player = other.gameObject.GetComponent<PlayerControl_Animation>();

            if (player != null)
            {
                // Call the TakeDamage method on the player to inflict damage
                player.TakeDamage(damageAmount);
                Debug.Log("Player damaged by enemy! Damage Amount: " + damageAmount);
            }
        }
    }
}
