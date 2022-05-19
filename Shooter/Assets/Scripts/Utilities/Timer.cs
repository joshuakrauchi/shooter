using System;
using UnityEngine;

[Serializable]
public class Timer
{
    [field: SerializeField] public float TimeToFinish { get; private set; }

    public float ElapsedTime { get; private set; }

    public Timer(float timeToFinish) => TimeToFinish = timeToFinish;

    public void UpdateTime(bool isRewinding)
    {
        if (isRewinding)
        {
            if (ElapsedTime > 0.0f)
            {
                ElapsedTime -= Time.deltaTime;
            }
        }
        else
        {
            ElapsedTime += Time.deltaTime;
        }
    }

    public void Reset() => ElapsedTime = 0.0f;

    public bool IsFinished(bool isRewinding) => isRewinding ? ElapsedTime <= 0f : ElapsedTime >= TimeToFinish;
}