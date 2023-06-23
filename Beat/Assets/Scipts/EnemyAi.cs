using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAi : MonoBehaviour
{
    public int damageAmount;
    public float attackRange = 2f;
    public float movementSpeed = 3f;

    private CharacterController controller;
    private Transform target;
    private bool isAttacking;
    private Animator animator;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        // Find the player using the playerTag
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            target = player.transform;
        }
    }

    private void Update()
    {
        if (target != null)
        {
            float distance = Vector3.Distance(transform.position, target.position);

            if (distance <= attackRange && !isAttacking)
            {
                // Attack the target
                Attack();
            }
            else
            {
                // Move towards the target
                Move();
            }
        }
    }

    private void Move()
    {
        Vector3 moveDirection = (target.position - transform.position).normalized;
        Vector3 movement = moveDirection * movementSpeed * Time.deltaTime;

        // Face the target
        Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10f * Time.deltaTime);

        controller.Move(movement);

        isAttacking = false;
        animator.SetBool("isWalking", true);
    }

    private void Attack()
    {
        // Face the target
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10f * Time.deltaTime);

        // Perform attack animation
        animator.SetBool("Attack", true);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Player"))
        {
            // Deal damage to the player
            PlayerHealth playerHealth = hit.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount);
            }

            isAttacking = true;
        }
    }
}
