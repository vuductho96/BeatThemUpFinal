    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.AI;

    public class EnemyControler : MonoBehaviour
    {
        public Transform playerPosition; // Reference to the player's position
        public float detectionRange = 10f; // Range within which the enemy detects the player
        public float attackRange = 2f; // Range within which the enemy can attack the player
        public int attackDamage = 1; // Damage amount of the enemy's attack
        public float attackCooldown = 2f; // Cooldown period between attacks

        private NavMeshAgent enemy; // Reference to the NavMeshAgent component
        private Animator anim;
        private enum State { Idle, Chase, Attack }
        private State currentState = State.Idle;
        private bool canAttack = true; // Flag to track if the enemy can attack

        private void Awake()
        {
            enemy = GetComponent<NavMeshAgent>(); // Assign the NavMeshAgent component
            anim = GetComponent<Animator>();
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
                        currentState = State.Chase;
                    }
                    break;
            }
        }

        private void IdleState()
        {
            // Logic for the Idle state
            anim.SetFloat("Speed", 0f,0.1f,Time.deltaTime);
        }

        private void ChaseState()
        {
            // Logic for the Chase state
            enemy.SetDestination(playerPosition.position);
            anim.SetFloat("Speed", 0.5f, 0.1f, Time.deltaTime);
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

            // Set the canAttack flag to true, allowing the enemy to attack again
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
            }
        }

        private bool PlayerInRange()
        {
            // Check if the player is within attack range
            return Vector3.Distance(transform.position, playerPosition.position) <= attackRange;
        }
    }
