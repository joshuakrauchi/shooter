using System.Collections.Generic;
using UnityEngine;

public abstract class TimeObject : MonoBehaviour
{
    protected LinkedList<TimeData> TimeData;

    protected virtual void Awake()
    {
        TimeData = new LinkedList<TimeData>();
    }

    protected void UpdateTimeData()
    {
        if (!GameManager.IsRewinding)
        {
            Record();
        }
        else
        {
            Rewind();
        }
    }

    protected abstract void Record();

    protected abstract void Rewind();
}