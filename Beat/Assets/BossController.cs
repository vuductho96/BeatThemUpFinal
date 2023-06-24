using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public Transform playerPosition; // Reference to the player's position
    public float detectionRange; // Range within which the boss detects the player
    public float attackRange; // Range within which the boss can attack the player
    public int attackDamage; // Damage amount of the boss's attack
    public float attackCooldown; // Cooldown period between attacks

    private UnityEngine.AI.NavMeshAgent boss; // Reference to the NavMeshAgent component
    private Animator anim;
    private enum State { Idle, Chase, Attack }
    private State currentState = State.Idle;
    private bool canAttack = true; // Flag to track if the boss can attack
    private Vector3 initialPosition; // Boss's initial position

    private void Awake()
    {
        boss = GetComponent<UnityEngine.AI.NavMeshAgent>(); // Assign the NavMeshAgent component
        anim = GetComponent<Animator>();
        initialPosition = transform.position; // Store the boss's initial position
    }

    private void Update()
    {
        switch (currentState)
        {
            case State.Idle:
                IdleState();
                DetectPlayer();
                break;
            case State.Chase:
                ChaseState();
                DetectPlayer();
                if (PlayerInRange())
                {
                    currentState = State.Attack;
                }
                break;
            case State.Attack:
                AttackState();
                if (!PlayerInRange())
                {
                    currentState = State.Idle;
                }
                break;
        }
    }

    private void IdleState()
    {
        // Logic for the Idle state
        anim.SetFloat("BossMove", 0f, 0.1f, Time.deltaTime);
    }

    private void ChaseState()
    {
        // Logic for the Chase state
        boss.SetDestination(playerPosition.position);
        anim.SetFloat("BossMove", 0.5f, 0.1f, Time.deltaTime);
    }

    private void AttackState()
    {
        // Logic for the Attack state
        if (canAttack)
        {
            anim.SetTrigger("Attack");

            // Deal damage to the player
            PlayerHealth playerHealth = playerPosition.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(attackDamage);
            }

            // Start the attack cooldown
            StartCoroutine(StartAttackCooldown());
        }
    }

    private IEnumerator StartAttackCooldown()
    {
        // Set the canAttack flag to false
        canAttack = false;

        // Wait for the attack cooldown duration
        yield return new WaitForSeconds(attackCooldown);

        // Set the canAttack flag to true, allowing the boss to attack again
        canAttack = true;
    }

    private void DetectPlayer()
    {
        // Check if the player is within detection range
        if (Vector3.Distance(transform.position, playerPosition.position) <= detectionRange)
        {
            currentState = State.Chase;
        }
        else
        {
            currentState = State.Idle;
            boss.SetDestination(initialPosition); // Return to the initial position
        }
    }

    private bool PlayerInRange()
    {
        // Check if the player is within attack range
        return Vector3.Distance(transform.position, playerPosition.position) <= attackRange;
    }
}
