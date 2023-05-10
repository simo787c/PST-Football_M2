using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class Goal : MonoBehaviour
{
    /* void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ball")
        {
            // Stop the ball's movement
            Rigidbody ballRigidbody = other.gameObject.GetComponent<Rigidbody>();
            ballRigidbody.velocity = Vector3.zero;
            ballRigidbody.angularVelocity = Vector3.zero;

            // Reset the ball's position to the center of the field
            other.gameObject.transform.position = new Vector3(0f, 0.5f, 0f);

            // Increment the score of the player who scored the goal
            if (this.tag == "RedGoal")
            {
                GameManager.instance.RedScore++;
            }
            else if (this.tag == "BlueGoal")
            {
                GameManager.instance.BlueScore++;
            }

            // Update the score UI
            UIManager.instance.UpdateScoreText();
        }
    } */

    /* void OnTriggerEnter(Collider other)
    {
        GoalScore goalScore = FindObjectOfType<GoalScore>();
        if (other.gameObject.tag == "RedGoal")
        {
            Debug.Log("RedGoal");
            goalScore.IncrementScore("Red");
        } else if (other.gameObject.tag == "BlueGoal")
        {
            Debug.Log("BlueGoal");
            goalScore.IncrementScore("Blue");
        }
    } */
}
