using System.Collections.Generic;
using UnityEngine;

public abstract class TimeObject : MonoBehaviour, IUpdateable
{
    [field: SerializeField] protected GameData GameData { get; private set; }
    [field: SerializeField] protected GameState GameState { get; private set; }

    protected LinkedList<ITimeData> TimeData { get; private set; }

    private const uint MaxData = 600;

    protected virtual void Awake()
    {
        TimeData = new LinkedList<ITimeData>();
    }

    protected void UpdateTimeData()
    {
        if (GameState.IsRewinding)
        {
            if (TimeData.Count <= 0) return;
            
            ITimeData timeData = TimeData.Last.Value;
            Rewind(timeData);
            
            TimeData.RemoveLast();
        }
        else
        {
            Record();
        }
    }

    protected abstract void Rewind(ITimeData timeData);

    protected abstract void Record();

    protected void AddTimeData(ITimeData timeData)
    {
        while (TimeData.Count >= MaxData)
        {
            TimeData.RemoveFirst();
        }

        TimeData.AddLast(timeData);
    }

    public virtual void UpdateUpdateable()
    {
        UpdateTimeData();
    }
}