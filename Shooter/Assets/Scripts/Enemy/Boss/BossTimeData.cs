using System.Collections.Generic;
using UnityEngine;

public class BossTimeData : EnemyTimeData
{
    public int PhaseIndex { get; private set; }
    public bool IsNewPhase { get; private set; }
    public Vector2 StartPosition { get; private set; }
    public Vector2 EndPosition { get; private set; }
    public float TotalTime { get; private set; }
    public float InitialDelay { get; private set; }

    public BossTimeData(float health, bool isDisabled, List<ShootBehaviour> shootBehaviours, int phaseIndex, bool isNewPhase, Vector2 startPosition, Vector2 endPosition, float totalTime, float initialDelay) : base(health, isDisabled, shootBehaviours)
    {
        PhaseIndex = phaseIndex;
        IsNewPhase = isNewPhase;
        StartPosition = startPosition;
        EndPosition = endPosition;
        TotalTime = totalTime;
        InitialDelay = initialDelay;
    }
}