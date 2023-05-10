using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Cinemachine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public GameObject menu;

    CinemachineBrain cinemachineBrain;

    

    void Start()
    {
        cinemachineBrain = Camera.main.GetComponent<CinemachineBrain>();
        Time.timeScale = 1; //start game unpaused
    }

    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)

      {
         if(menu.activeSelf)
         {
            CloseMenu();
         }
         else
         {
            OpenMenu();
         }

      }
    }

    public void OpenMenu()
    {
        menu.SetActive(true);
        Time.timeScale = 0; // pause game
        Cursor.lockState = CursorLockMode.None; // unlock cursor
        Cursor.visible = true; // unhide cursor
        // freeze camera movement by setting brains update method to FixedUpdate
        cinemachineBrain.m_UpdateMethod = CinemachineBrain.UpdateMethod.FixedUpdate;
    }

    public void CloseMenu() // inverse of OpenMenu
    {
        menu.SetActive(false);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        cinemachineBrain.m_UpdateMethod = CinemachineBrain.UpdateMethod.LateUpdate;
    }

    /* // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    } */
    /* void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision detected!");
        if (collision.gameObject.tag == "Ball")
        {
            Debug.Log("Ball!");
            Rigidbody ballRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            ballRigidbody.isKinematic = true;

            // Get the direction of the player's movement
            Vector3 direction = transform.position - collision.gameObject.transform.position;
            direction.y = 0f;
            direction.Normalize();

            // Apply a force to the ball based on the player's movement direction
            float pushForce = 5f;
            ballRigidbody.AddForce(direction * pushForce, ForceMode.Impulse);

            ballRigidbody.isKinematic = false;
        }
    } */
}
