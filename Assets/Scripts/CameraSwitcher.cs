using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraSwitcher : MonoBehaviour
{
    public Camera camera1;
    public Camera camera2;

    void Start()
    {
        // Disable all cameras except the main camera
        Camera[] cameras = FindObjectsOfType<Camera>();
        foreach (Camera camera in cameras)
        {
            if (camera != Camera.main)
            {
                camera.enabled = false;
            }
        }
    }

    void Update()
    {
        if (Keyboard.current.tabKey.wasPressedThisFrame)
        {
            if (Camera.main == camera1)
            {
                camera1.enabled = false;
                camera2.enabled = true;
            }
            else
            {
                camera2.enabled = false;
                camera1.enabled = true;
            }
        }
    }
}