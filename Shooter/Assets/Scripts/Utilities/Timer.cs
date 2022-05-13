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

    public virtual void UpdateTime(bool isRewinding)
    {
        if (isRewinding)
        {
            if (ElapsedTime > 0.0f)
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
        if (IsFinished(false))
        {
            ElapsedTime = 0.0f;
        }
        else
        {
            ElapsedTime -= TotalTime;
        }
    }

    public bool IsFinished(bool isRewinding)
    {
        return isRewinding ? ElapsedTime <= 0f : ElapsedTime >= TotalTime;
    }

    public Timer DeepCopy()
    {
        return (Timer) MemberwiseClone();
    }
}