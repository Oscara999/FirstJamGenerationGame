using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BetweenDeathAndOblivion;
using UnityEngine.UI;
//using TMPro;

public class TimerController : MonoBehaviour
{

    [SerializeField] private float seconds;
    public bool starTime;
    public float Seconds
    {
        get { return seconds; }
    }
    [SerializeField] private Text chrono;

    void Start()
    {
        chrono.text = "";
        starTime = false;
    }

    void Update()
    {
        if (starTime)
        {
            seconds -= Time.deltaTime * 1;
            if (seconds >= 0)
                DisplayTimer(ToTimer(seconds));
        }
    }

    public string ToTimer(float totalSeconds)
    {
        int minutes = Mathf.FloorToInt(totalSeconds / 60);
        int seconds = (int)totalSeconds % 60;
        string remainingTime = minutes.ToString() + " : " + seconds.ToString();
        return remainingTime;

    }
    public void DisplayTimer(string timer)
    {
        chrono.text = timer;
    }
}
