using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour
{
    public AudioClip sound;
    public Vector3 soundPos;

    // is called when the GameObject the script is attached to is initialized
    private void OnEnable() 
    {
        // Subscribe to the sceneLoaded event when the script is enabled
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    // on disable happens when gameobject or script is deactivated
    // we disable the OnSceneLoaded when script is disabled to avoid memory leak etc
    private void OnDisable()
    {
        // Unsubscribe from the sceneLoaded event when the script is disabled
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // This method is called when a new scene is loaded
        AudioSource.PlayClipAtPoint(sound, soundPos);
        // Play the sound at the position (0, 0, 0) using Vector3.zero
    }
}
