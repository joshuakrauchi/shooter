using System.Collections.Generic;
using UnityEngine;

public abstract class TimeObject : MonoBehaviour, IUpdateable
{
    [field: SerializeField] protected GameData GameData { get; private set; }
    [field: SerializeField] protected GameState GameState { get; private set; }
    
    protected LinkedList<ITimeData> TimeData { get; private set; }

    // If a TimeObject IsDisabled for the full length of a TimeData list, it calls a handler.
    protected bool IsDisabled { get; set; }

    private const uint MaxData = 1000;

    protected virtual void Awake()
    {
        TimeData = new LinkedList<ITimeData>();
    }

    private void UpdateTimeData()
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

            if (TimeData.First.Value.IsDisabled)
            {
                OnFullyDisabled();
            }
        }
    }

    protected abstract void Rewind(ITimeData timeData);

    protected abstract void Record();

    protected abstract void OnFullyDisabled();

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