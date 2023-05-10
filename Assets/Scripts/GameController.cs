using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{

   public Button lvl2;
   public Button lvl3;
   public TMP_Text score;


   /* void Awake()
    {

      GameObject[] objs = GameObject.FindGameObjectsWithTag(gameObject.tag);
      if (objs.Length > 1)
      {
         // If it exists, destroy this instance
         Destroy(gameObject);
         return;
      }
      

      // Make sure this object persists between scenes
         if(SceneManager.GetActiveScene().buildIndex != 0)
         {
            DontDestroyOnLoad(gameObject);
         }
         
    } */

void Start()
    {
        // Disable the restart button at the start of the game
        /* lvl2.gameObject.SetActive(false);
        lvl2.interactable = false;
        lvl3.gameObject.SetActive(false);
        lvl3.interactable = false; */
      if(SceneManager.GetActiveScene().buildIndex == 1)
      {
         lvl2.gameObject.SetActive(false);
         lvl2.interactable = false;
      } else if (SceneManager.GetActiveScene().buildIndex == 2) {
         lvl2.gameObject.SetActive(true);
         lvl2.interactable = true;
      }
      if (SceneManager.GetActiveScene().buildIndex == 3) {
         lvl3.gameObject.SetActive(true);
         lvl3.interactable = true;
      } else {
         lvl3.gameObject.SetActive(false);
         lvl3.interactable = false;
      }
    }

public void Update()

   {
      if (Keyboard.current.rKey.wasPressedThisFrame)
      {
         ResetCurrentLevel();
      }

      if(SceneManager.GetActiveScene().buildIndex == 1 && int.Parse(score.text) == 1)
      {
         lvl2.gameObject.SetActive(true);
         lvl2.interactable = true;
         score.text = "0";
      }

      if(SceneManager.GetActiveScene().buildIndex == 2 && int.Parse(score.text) == 1)
      {
         lvl3.gameObject.SetActive(true);
         lvl3.interactable = true;
         score.text = "0";
      }

      if(SceneManager.GetActiveScene().buildIndex == 3 && int.Parse(score.text) == 100)
      {
         Debug.Log("You win nothing!");

         Invoke("QuitGame",10f);
      }
   }

   public void ResetCurrentLevel()
   {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
   }

   public void StartMenu()
   {
      //Destroy(gameObject);
      SceneManager.LoadScene(0);
   }

   public void playLevel1()
   {
        //number is scene index
        SceneManager.LoadScene(1);
   }

   public void playLevel2()
   {
        //number is scene index
        SceneManager.LoadScene(2);
   }

   public void playLevel3()
   {
        //number is scene index
        SceneManager.LoadScene(3);
   }

   public void QuitGame()
   {
    //To quit if using editor, also useful for setting dev only properties
    //preprocesser directive, code only exists when running in editor
    #if UNITY_EDITOR

      UnityEditor.EditorApplication.isPlaying = false;

    #endif

      Application.Quit();
   }
}
