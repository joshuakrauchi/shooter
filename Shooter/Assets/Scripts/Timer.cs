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

    public void Reset(bool byTotalTime)
    {
        ElapsedTime = byTotalTime ? ElapsedTime - TotalTime : 0f;
    }

    public void SetToFinished()
    {
        ElapsedTime = TotalTime;
    }

    public bool IsFinished(bool isRewinding)
    {
        return isRewinding ? ElapsedTime <= 0f : ElapsedTime >= TotalTime;
    }
}