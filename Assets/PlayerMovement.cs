using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Movement
    public CharacterController controller;
    float x;
    float z;
    public float playerSpeed;
    public float walkingSpeed = 12f;
    public float runningMultiplier = 1.2f;
    public float jumpHeight = 3f;

    // Gravity
    Vector3 velocity;
    public float gravity = -9.81f;
    public float groundDistance = 0.4f;
    public bool isGrounded;
    public float yVerlocity;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        setup();
        Movement();
        Jump();
    }


    public void setup()
    {
        // Movement
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");

        // Player speed logic
        if (Input.GetKey(KeyCode.LeftShift))
        {
            playerSpeed = walkingSpeed * runningMultiplier;
            Debug.Log("Sprinting");
        }
        else
        {
            playerSpeed = walkingSpeed;
            Debug.Log("Walking");
        }

        // Jump
        isGrounded = controller.isGrounded;
        yVerlocity = velocity.y;
    }

    public void Movement()
    {
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * playerSpeed * Time.deltaTime);
    }

    public void Jump()
    {
        // Jump check
        bool isJumping = Input.GetButtonDown("Jump") && isGrounded;
        if (isJumping)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            Debug.Log("Is Jumping");
        }

        // Velocity reset
        bool shouldResetPlayerVelocity = isGrounded && velocity.y < 0;
        if (shouldResetPlayerVelocity)
        {
            velocity.y = -1f;
        }

        // Gravity simulation.
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
