using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GoalScore : MonoBehaviour
{
    public AudioClip soundClip;


    [Header("Score Text")]
    public TMP_Text RedScoreText;
    public TMP_Text BlueScoreText;
    private int redScore = 0;
    private int blueScore = 0;



    // Start is called before the first frame update
    public void IncrementScore(string team)
    {
        if (team == "Red")
        {
            redScore++;
            RedScoreText.text = redScore.ToString();
        }
        else if (team == "Blue")
        {
            blueScore++;
            BlueScoreText.text = blueScore.ToString();
        }
        //play crowd cheer sound
        //audioSource.PlayOneShot(soundClip);
        AudioSource.PlayClipAtPoint(soundClip, new Vector3(0,10f,0));
        //example diff if depending on current active level
    }
}
