using System.Collections.Generic;

public class BossTimeData : EnemyTimeData
{
    public int PhaseIndex { get; }
    public BossMovement BossMovement { get; }
    public Timer PhaseTimer { get; }

    public BossTimeData(float health, bool isDisabled, List<ShootBehaviour> shootBehaviours, int phaseIndex, BossMovement bossMovement, Timer phaseTimer) : base(health, isDisabled, shootBehaviours)
    {
        PhaseIndex = phaseIndex;
        BossMovement = bossMovement;
        PhaseTimer = phaseTimer;
    }
}