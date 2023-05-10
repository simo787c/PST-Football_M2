using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using UnityEngine.InputSystem;

public class Ball : MonoBehaviour
{
    [Header("Push Force Settings")]
    public float pushForceWhenWalkedInto = 2f;
	public float pushForceWhenRunInto = 5f;
    public float slowdownFactor = 0.95f; // Factor by which the ball's speed will be reduced each FixedUpdate

    private bool isPushed = false;
    private Rigidbody rb;

    private StarterAssetsInputs inputs;

    [Space(10)]
    /* [Header("Ground Check")]
    public float ballHeight;
    public LayerMask whatIsGround;
    bool grounded; */
    [Header("Player Grounded")]
    [Tooltip("If the character is grounded or not. Not part of the CharacterController built in grounded check")]
    public bool Grounded = true;
    [Tooltip("Useful for rough ground")]
    public float GroundedOffset = -0.07f;
    [Tooltip("The radius of the grounded check. Should match the radius of the CharacterController")]
    public float GroundedRadius = 0.5f;
    [Tooltip("What layers the character uses as ground")]
    public LayerMask GroundLayers;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        inputs = GameObject.FindGameObjectWithTag("Player").GetComponent<StarterAssetsInputs>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isPushed = true;
        }
        else if (other.gameObject.tag == "RedGoal" || other.gameObject.tag == "BlueGoal")
        {
            GoalScore goalScore = FindObjectOfType<GoalScore>();
            if (other.gameObject.tag == "RedGoal")
            {
                goalScore.IncrementScore("Red");
            } else if (other.gameObject.tag == "BlueGoal")
            {
                goalScore.IncrementScore("Blue");
            }
            ResetBallPosition(new Vector3(0f, 0.37f, 0f));
        }
        else if (other.gameObject.tag == "Endline")
        {
            ResetBallPosition(new Vector3(12.565f, 0.37f, 19.095f));
        }
        else if (other.gameObject.tag == "Sideline")
        {
            StopBall();
        }
        else if (other.gameObject.tag == "Outerlines")
        {
            StopBall();
        }

        if (other.CompareTag("Wall"))
        {
            // Calculate the new velocity of the ball after hitting the wall
            Vector3 newVelocity = Vector3.Reflect(rb.velocity, other.transform.forward);

            // Reduce the speed of the ball to prevent it from going through the wall
            newVelocity *= 0.5f;

            // Set the new velocity of the ball
            rb.velocity = newVelocity;
        }

        if (other.CompareTag("Stadium"))
        {
            ResetBallPosition(new Vector3(0f, 0.37f, 0f));
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // When ball pushed to the wall with the "Wall" tag it bounce back
        if (collision.gameObject.CompareTag("Wall"))
        {
            // Calculate the new velocity of the ball after hitting the wall
            Vector3 newVelocity = Vector3.Reflect(rb.velocity, collision.contacts[0].normal);

            // Reduce the speed of the ball to prevent it from going through the wall
            newVelocity *= 0.5f;

            // Set the new velocity of the ball
            rb.velocity = newVelocity;
        }
    }

    void Update() 
    {
        // ground check
        GroundedCheck();
        /* if (Grounded) 
        {
            Debug.Log("Grounded?");
            Debug.Log(Grounded);
            rb.useGravity = false;
        } */
        if (Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) < 2.5f)
        {
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

                // Get the direction the player is facing
                Vector3 direction = GameObject.FindGameObjectWithTag("Player").transform.forward;

                // Apply a force to the ball in the direction the player is facing
                rb.AddForce(direction * 24f, ForceMode.Impulse);

                // Add an upward force to the ball to counteract gravity
                rb.AddForce(Vector3.up * Mathf.Abs(-7.5f), ForceMode.Impulse);

                // Apply gravity to the ball
                // rb.useGravity = true;

                // Clear the isPushed flag to prevent it from interfering with the kick
                isPushed = false;
            }
        }

    }

    void FixedUpdate()
    {
        if (Grounded && rb.velocity.y < 0f)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        }
        if (Grounded == false) 
        {
            rb.AddForce(Vector3.down * 9.81f, ForceMode.Acceleration);
        }

        if (isPushed)
        {
            // Get the direction of the player's movement
            Vector3 direction = transform.position - GameObject.FindGameObjectWithTag("Player").transform.position;
            direction.y = 0f;
            direction.Normalize();

            // Get the player's speed and adjust the push force accordingly
            /* float playerSpeed = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>().velocity.magnitude;
            float pushForce = playerSpeed < pushForceWhenWalkedInto ? pushForceWhenRunInto : pushForceWhenWalkedInto; */
            float pushForce = inputs.sprint ? pushForceWhenRunInto : pushForceWhenWalkedInto;
            
            // Apply a force to the ball based on the player's movement direction
            rb.AddForce(direction * pushForce, ForceMode.Impulse);

            isPushed = false;
        }

        if (inputs.sprint)
        {
            slowdownFactor = 0.99f;
        } else {
            slowdownFactor = 0.95f;
        }

        // Reduce the ball's speed gradually over time
        rb.velocity *= slowdownFactor;
    }

    void StopBall()
    {
        // Stop the ball's movement
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    void ResetBallPosition(Vector3 position)
    {
        // Stop the ball's movement
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        transform.position = position;
    }


    void GroundedCheck()
    {
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z);
        Grounded = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers, QueryTriggerInteraction.Ignore);
    }


    /* private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8) {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                Debug.Log("Ouch");
            };
        }
    } */
}
