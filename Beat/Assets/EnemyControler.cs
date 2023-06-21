using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyControler : MonoBehaviour
{
    public Transform playerPosition; // Reference to the player's position
    public NavMeshAgent enemy; // Reference to the NavMeshAgent component
   Animator anim;
    public float attackRange = 2f; // The range within which the enemy can attack the player
    public int attackDamage = 1;
    private bool isIdle = true;
    private bool foundPlayer = false;
    private void Awake()
    {
        enemy = GetComponent<NavMeshAgent>(); // Assign the NavMeshAgent component
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        if (playerPosition != null)
        {
            enemy.SetDestination(playerPosition.position); // Set the destination to the player's position
        }
        else
        {
            Debug.LogError("Player position is not set!");
        }

        if (foundPlayer)
        {
            Attack();
        }
        else if (isIdle)
        {
            Idle();
        }
        else
        {
            Run();
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.CompareTag("Player"))
        {
            PlayerHealth playerHealth = hit.collider.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(attackDamage);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foundPlayer = false;
            isIdle = true;
        }
    }

    private void Attack()
    {
        if (Vector3.Distance(transform.position, playerPosition.position) <= attackRange)
        {
            // Deal damage to the player
            PlayerHealth playerHealth = playerPosition.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(attackDamage);
            }
        }

        // Trigger the attack animation
        anim.SetTrigger("Attack");
    }

    private void Run()
    {
        anim.SetFloat("Speed", enemy.velocity.magnitude);
    }

    private void Idle()
    {
        anim.SetFloat("Speed", 0f);
    }
}
