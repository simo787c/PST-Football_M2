using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float height = 10f;
    public float distance = 10f;

    void Update()
    {
        // Calculate the new position for the camera based on the target's position, height, and distance
        //Debug.Log(transform.forward);
        Vector3 newPosition = target.position - transform.forward * distance;
        newPosition.y = height;

        // Update the camera's position and rotation
        transform.position = newPosition;
        transform.LookAt(target);
    }
}
