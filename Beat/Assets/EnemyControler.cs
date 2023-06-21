using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyControler : MonoBehaviour
{
    public Transform playerPosition; // Reference to the player's position
    public NavMeshAgent enemy; // Reference to the NavMeshAgent component
    public Animator anim;

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foundPlayer = true;
            isIdle = false;
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
