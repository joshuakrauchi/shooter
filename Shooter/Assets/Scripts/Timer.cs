using System;
using UnityEngine;

public class Timer
{
    public float TotalTime { get; private set; }

    public float ElapsedTime { get; private set; }

    public Timer(float totalTime)
    {
        TotalTime = totalTime;
    }

    public void UpdateTime()
    {
        if (GameManager.IsRewinding)
        {
            if (ElapsedTime > 0f)
            {
                ElapsedTime -= Time.deltaTime;
            }
        }
        else if (ElapsedTime < TotalTime)
        {
            ElapsedTime += Time.deltaTime;
        }
    }

    public void Reset()
    {
        ElapsedTime = 0f;
    }

    public void SubtractTotalTime()
    {
        ElapsedTime -= TotalTime;
    }

    public void SetToTotal()
    {
        ElapsedTime = TotalTime - Time.deltaTime;
    }

    public bool IsFinished()
    {
        return GameManager.IsRewinding ? ElapsedTime <= 0f : ElapsedTime >= TotalTime;
    }
}