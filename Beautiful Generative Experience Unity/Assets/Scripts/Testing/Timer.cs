using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private float timeLeft;
    public enum Mode { COUNTDOWN, COUNTUP}
    private Mode mode;

    public delegate void TimerEnd();
    public static event TimerEnd OnTimerEnd;


    private void Update()
    {
        Countdown();
    }

    private void Countdown()
    {
        if(mode == Mode.COUNTDOWN)
        {
            if (timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
            }
            else
            {
                OnTimerEnd();
            }

        } else if(mode == Mode.COUNTUP)
        {
            timeLeft += Time.deltaTime;
        }
       
    }

    public void SetTimer(float t, Mode m)
    {
        timeLeft = t;
        mode = m;
    }
   

 

}
