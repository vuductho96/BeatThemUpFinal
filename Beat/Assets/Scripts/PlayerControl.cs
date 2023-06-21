using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class PlayerControl : MonoBehaviour
{
    private CharacterController character;
    private Vector2 movement;
    public float movementSpeed = 5f;
    public float gravity = -9.81f;
    private Vector3 velocity;
    public float rotationSpeed;
    public float jumpForce;
    private bool isJumping;
 
   // Maximum health value for the player
   

    Animator animator;
    AudioSource audioSource;
    bool isMoving; // Flag to track if the player is moving

    private void Start()
    {
      
        character = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();
    }

    private void Update()
    {
        MovePlayer();
        ApplyGravity();
        if (!isJumping && Keyboard.current.spaceKey.isPressed) // Check if space key is pressed and not already jumping
        {
            Jump();
        }
        if (Mouse.current.leftButton.isPressed)
        {
            Punch();
        }
    }

    private void MovePlayer()
    {
        Vector3 move = new Vector3(movement.x, 0f, movement.y); // Swap movement.x and movement.y
        Vector3 movementVector = move * movementSpeed * Time.deltaTime;

        if (move != Vector3.zero) // Rotate regardless of movement magnitude
        {
            Vector3 desiredForward = Vector3.RotateTowards(transform.forward, move, rotationSpeed * Time.deltaTime, 0f);
            Quaternion targetRotation = Quaternion.LookRotation(desiredForward);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        character.Move(movementVector);


      

        float speed = move.magnitude; // Calculate the speed based on the magnitude of the movement vector

        if (speed < 0.1f)
        {
            Idle();
            isMoving = false;
        }
        else if (speed < 0.5f)
        {
            Walk();
            isMoving = true;
        }
        else
        {
            Run();
            isMoving = true;
        }

        // Play sound when starting to move
        if (isMoving && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
        // Stop sound when not moving
        else if (!isMoving && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

    private void ApplyGravity()
    {
        velocity.y += gravity * Time.deltaTime;
        character.Move(velocity * Time.deltaTime);

        if (character.isGrounded)
        {
            isJumping = false;
            velocity.y = 0f;
        }
    }

    private void Idle()
    {
        animator.SetFloat("Speed", 0f, 0.1f, Time.deltaTime);
    }

    public void Walk()
    {
        animator.SetFloat("Speed", 0.5f, 0.1f, Time.deltaTime);
    }

    public void Run()
    {
        if (Keyboard.current.leftShiftKey.isPressed)
        {
            animator.SetFloat("Speed", 1.0f, 0.1f, Time.deltaTime);
        }
        else
        {
            animator.SetFloat("Speed", 0.5f, 0.1f, Time.deltaTime);
        }
    }

    public void Jump()
    {
        velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
        isJumping = true;
        animator.SetTrigger("Jump");
    }

    public void Punch()
    {
        animator.SetTrigger("Attack");

       
    }

    private void Die()
    {
        // Perform any actions when the player dies
        // For example, play a death animation, restart the level, etc.
    }

    private void MainMenu()
    {
        SceneManager.LoadScene("StartGAME");
    }
}
