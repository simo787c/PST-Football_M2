using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalkeeperController : MonoBehaviour
{
    public float speed = 4.0f;
    public float leftLimit = -3.0f;
    public float rightLimit = 3.0f;
    private bool moveRight = true;

    void Update()
    {
        if (transform.position.x < leftLimit)
        {
            moveRight = true;
        }
        else if (transform.position.x > rightLimit)
        {
            moveRight = false;
        }

        if (moveRight)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        else
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
    }
}
