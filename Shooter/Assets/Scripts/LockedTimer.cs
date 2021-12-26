using UnityEngine;

public class LockedTimer : Timer
{
    public LockedTimer(float totalTime) : base(totalTime)
    {
    }

    public override void UpdateTime()
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
}