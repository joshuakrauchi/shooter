using System;
using UnityEngine;

public class Timer : ICloneable
{
    public float TotalTime { get; private set; }
    public float ElapsedTime { get; protected set; }

    public Timer(float totalTime)
    {
        TotalTime = totalTime;
    }

    public virtual void UpdateTime()
    {
        ElapsedTime += GameManager.IsRewinding ? -Time.deltaTime : Time.deltaTime;
    }

    public void Reset(bool byTotalTime)
    {
        ElapsedTime = byTotalTime ? ElapsedTime - TotalTime : 0f;
    }

    public bool IsFinished(bool isRewinding)
    {
        return isRewinding ? ElapsedTime <= 0f : ElapsedTime >= TotalTime;
    }

    public object Clone()
    {
        return new Timer(TotalTime)
        {
            ElapsedTime = ElapsedTime
        };
    }
}