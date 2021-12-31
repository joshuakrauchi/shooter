using System;
using UnityEngine;

[Serializable]
public class Timer
{
    [field: SerializeField] public float TotalTime { get; private set; }

    public float ElapsedTime { get; protected set; }

    public Timer(float totalTime)
    {
        TotalTime = totalTime;
    }

    public virtual void UpdateTime()
    {
        ElapsedTime += GameManager.IsRewinding ? -Time.deltaTime : Time.deltaTime;
    }

    public void Reset()
    {
        ElapsedTime -= TotalTime;
    }

    public void SetToEnd()
    {
        ElapsedTime += TotalTime;
    }

    public bool IsFinished(bool isRewinding)
    {
        return isRewinding ? ElapsedTime <= 0f : ElapsedTime >= TotalTime;
    }

    public Timer Clone()
    {
        return (Timer) MemberwiseClone();
    }
}