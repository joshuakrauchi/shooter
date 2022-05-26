using System.Collections.Generic;
using UnityEngine;

public abstract class TimeObject : MonoBehaviour, IUpdateable
{
    [field: SerializeField] protected GameData GameData { get; private set; }
    [field: SerializeField] protected GameState GameState { get; private set; }

    private LinkedList<ITimeData> TimeData { get; set; }

    protected bool IsDisabled { get; set; }

    // If the whole list is full of TimeData is disabled, it's safe to remove from the world.
    private uint DisabledTimeDataRunCount { get; set; }

    private const uint MaxData = 500;

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

            if (IsDisabled)
            {
                ++DisabledTimeDataRunCount;

                if (DisabledTimeDataRunCount >= MaxData)
                {
                    OnFullyDisabled();
                }
            }
            else
            {
                DisabledTimeDataRunCount = 0;
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