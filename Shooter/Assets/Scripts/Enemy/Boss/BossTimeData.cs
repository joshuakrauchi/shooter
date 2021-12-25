using UnityEngine;

public class BossTimeData : EnemyTimeData
{
    public int PhaseIndex { get; private set; }
    public bool IsNewPhase { get; private set; }
    public Vector2 StartPosition { get; private set; }
    public Vector2 EndPosition { get; private set; }
    public Timer MovementTimer { get; private set; }

    public BossTimeData(float time, float health, bool isDisabled, ShootBehaviour shootBehaviour, int phaseIndex, bool isNewPhase, Vector2 startPosition, Vector2 endPosition, Timer movementTimer) : base(time, health, isDisabled, shootBehaviour)
    {
        PhaseIndex = phaseIndex;
        IsNewPhase = isNewPhase;
        StartPosition = startPosition;
        EndPosition = endPosition;
        MovementTimer = movementTimer;
    }
}