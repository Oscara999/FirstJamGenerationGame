using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BetweenDeathAndOblivion;
using UnityEngine.UI;
//using TMPro;

public class TimerController : MonoBehaviour
{

    [SerializeField] public float seconds;
    public bool starTime;
    public bool finished;
    public float Seconds
    {
        get { return seconds; }
    }
    [SerializeField] private Text chrono;
    [SerializeField] private Text Leaves;



    void Start()
    {
        chrono.text = "";
        starTime = false;
        finished = false;
        chrono.text = "0/3";
    }

    void Update()
    {
        if (finished) return;

        if (starTime)
        {

            seconds -= Time.deltaTime * 1;
            if (seconds >= 0)
            {
                DisplayTimer(ToTimer(seconds));
            }
            else
            {
                starTime = false;
                GameManager.Instance.FinishGame(false);
                finished = true;
            }    
        }

      
        
    }

    public string ToTimer(float totalSeconds)
    {
        int minutes = Mathf.FloorToInt(totalSeconds / 60);
        int seconds = (int)totalSeconds % 60;
        string remainingTime = minutes.ToString() + " : " + seconds.ToString();
        return remainingTime;

    }

    public void StopTimer()
    {
        finished = true;
        chrono.color = Color.yellow;
    }

    public void DisplayTimer(string timer)
    {
        chrono.text = timer;
    }
}
