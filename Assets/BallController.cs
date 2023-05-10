using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using StarterAssets;

public class BallController : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    private StarterAssetsInputs inputs;

    private Rigidbody rb;

    [Header("Push Force Settings")]
    public float pushForceWhenWalkedInto = 3f;
	public float pushForceWhenRunInto = 5f;
    [SerializeField] private float kickForce = 14f;
    [SerializeField] private float kickUpForce = -6f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        inputs = GameObject.FindGameObjectWithTag("Player").GetComponent<StarterAssetsInputs>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            float pushForce = inputs.sprint ? pushForceWhenRunInto : pushForceWhenWalkedInto;
            WalkOrRunKick(playerTransform, pushForce);
        }

        if (collision.gameObject.tag == "RedGoal" || collision.gameObject.tag == "BlueGoal")
        {
            GoalScore goalScore = FindObjectOfType<GoalScore>();
            if (collision.gameObject.tag == "RedGoal")
            {
                goalScore.IncrementScore("Red");
            } else if (collision.gameObject.tag == "BlueGoal")
            {
                goalScore.IncrementScore("Blue");
            }
            ResetBallPosition(new Vector3(0f, 0.37f, 0f));
        }
        else if (collision.gameObject.tag == "Endline")
        {
            // ResetBallPosition(new Vector3(12.565f, 0.37f, 19.095f));
            // If it touch the backline/Endline then depending on which side then it will goes to that corner
            if (transform.position.z > 1f)
            {
                if (transform.position.x > 3.18499994f)
                {
                    ResetBallPosition(new Vector3(12.565f, 0.37f, 19.095f));
                }
                else if (transform.position.x < -3.18499994f)
                {
                    ResetBallPosition(new Vector3(-12.565f, 0.37f, 19.095f));
                }
            }
            else if (transform.position.z < -1f)
            {
                if (transform.position.x > 3.18499994f)
                {
                    ResetBallPosition(new Vector3(12.565f, 0.37f, -19.095f));
                }
                else if (transform.position.x < -3.18499994f)
                {
                    ResetBallPosition(new Vector3(-12.565f, 0.37f, -19.095f));
                }
            }
            
        }
        else if (collision.gameObject.tag == "Sideline")
        {
            StopBall();
        }
        else if (collision.gameObject.tag == "Outerlines")
        {
            StopBall();
        }
        /* else if (collision.CompareTag("Stadium"))
        {
            ResetBallPosition(new Vector3(0f, 0.37f, 0f));
        } */
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) < 2.5f)
        {
            // Detect mouse click to kick the ball
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                KickBall(playerTransform);
            }
        }
    }

    private void WalkOrRunKick(Transform playerTransform, float pushForce)
    {
        float maxDistance = 5f;
        Collider[] colliders = Physics.OverlapSphere(playerTransform.position, maxDistance, LayerMask.GetMask("Ball"));
        Collider closestCollider = null;
        float closestDistance = Mathf.Infinity;
        foreach (Collider collider in colliders)
        {
            float distance = Vector3.Distance(playerTransform.position, collider.transform.position);
            if (distance < closestDistance)
            {
                closestCollider = collider;
                closestDistance = distance;
            }
        }

        // Kick the ball if it is close enough
        if (closestCollider != null && closestDistance < 1.5f)
        {
            Vector3 kickDirection = (closestCollider.transform.position - playerTransform.position).normalized;
            closestCollider.GetComponent<Rigidbody>().AddForce(kickDirection * pushForce, ForceMode.Impulse);
        }
    }



    private void KickBall(Transform playerTransform)
    {
        // Find the closest ball in front of the player
        float maxDistance = 5f;
        Collider[] colliders = Physics.OverlapSphere(playerTransform.position, maxDistance, LayerMask.GetMask("Ball"));
        Collider closestCollider = null;
        float closestDistance = Mathf.Infinity;
        foreach (Collider collider in colliders)
        {
            float distance = Vector3.Distance(playerTransform.position, collider.transform.position);
            if (distance < closestDistance)
            {
                closestCollider = collider;
                closestDistance = distance;
            }
        }

        // Kick the ball if it is close enough
        if (closestCollider != null && closestDistance < 2.5f)
        {
            Vector3 kickDirection = (closestCollider.transform.position - playerTransform.position).normalized;

            closestCollider.GetComponent<Rigidbody>().AddForce(kickDirection * kickForce, ForceMode.Impulse);
            rb.AddForce(Vector3.up * Mathf.Abs(kickUpForce), ForceMode.Impulse);
        }
    }


    void ResetBallPosition(Vector3 position)
    {
        StopBall();

        transform.position = position;
    }

    void StopBall()
    {
        // Stop the ball's movement
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    private void RespawnBallIfNeeded(Transform ballTransform)
    {
        float respawnXThreshold = 3.18499994f;
        Vector3 respawnPositionRight = new Vector3(12.565f, 0.37f, 19.095f);
        Vector3 respawnPositionLeft = new Vector3(-12.565f, 0.37f, 19.095f);

        if (ballTransform.position.x > respawnXThreshold)
        {
            if (ballTransform.position != respawnPositionRight)
            {
                ballTransform.position = respawnPositionRight;
            }
        }
        else if (ballTransform.position.x < -respawnXThreshold)
        {
            if (ballTransform.position != respawnPositionLeft)
            {
                ballTransform.position = respawnPositionLeft;
            }
        }
}
}

