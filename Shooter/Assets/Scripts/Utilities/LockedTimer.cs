using UnityEngine;

public class LockedTimer : Timer
{
    public LockedTimer(float totalTime) : base(totalTime)
    {
    }

    public override void UpdateTime(bool isRewinding)
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
}