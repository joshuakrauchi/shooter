using System.Collections.Generic;

public class BossTimeData : EnemyTimeData
{
    public BossMovement BossMovement { get; }
    public int PhaseIndex { get; }
    public List<ShootBehaviour> ShootBehaviours { get; }
    public Timer PhaseTimer { get; }

    public BossTimeData(float health, bool isDisabled, BossMovement bossMovement, int phaseIndex, List<ShootBehaviour> shootBehaviours, Timer phaseTimer) : base(health, isDisabled)
    {
        BossMovement = bossMovement;
        PhaseIndex = phaseIndex;
        ShootBehaviours = shootBehaviours;
        PhaseTimer = phaseTimer;
    }
}