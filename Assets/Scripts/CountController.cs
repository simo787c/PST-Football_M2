using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CountController : MonoBehaviour
{
    private float timeSinceGameStart = 0f;
    public TMP_Text timerText;

    void Update()
    {
        // timeSinceGameStart += Time.deltaTime;
        // timerText.text = timeSinceGameStart.ToString("F2");
        timeSinceGameStart += Time.deltaTime;
        int minutes = Mathf.FloorToInt(timeSinceGameStart / 60f);
        int seconds = Mathf.FloorToInt(timeSinceGameStart % 60f);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
