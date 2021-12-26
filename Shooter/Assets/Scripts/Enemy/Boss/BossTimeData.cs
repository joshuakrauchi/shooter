using System.Collections.Generic;
using UnityEngine;

public class BossTimeData : EnemyTimeData
{
    public int PhaseIndex { get; private set; }
    public Vector2 StartPosition { get; private set; }
    public Vector2 EndPosition { get; private set; }
    public Timer TotalTime { get; private set; }
    public Timer InitialDelay { get; private set; }

    public BossTimeData(float health, bool isDisabled, List<ShootBehaviour> shootBehaviours, int phaseIndex, Vector2 startPosition, Vector2 endPosition, Timer totalTime, Timer initialDelay) : base(health, isDisabled, shootBehaviours)
    {
        PhaseIndex = phaseIndex;
        StartPosition = startPosition;
        EndPosition = endPosition;
        TotalTime = totalTime;
        InitialDelay = initialDelay;
    }
}