using System.Collections.Generic;

public class BossTimeData : EnemyTimeData
{
    public bool IsBossActive { get; }
    public BossMovement BossMovement { get; }
    public int PhaseIndex { get; }
    public List<ShootBehaviour> ShootBehaviours { get; }
    public Timer PhaseTimer { get; }

    public BossTimeData(bool isDisabled, float health, bool isBossActive, BossMovement bossMovement, int phaseIndex, List<ShootBehaviour> shootBehaviours, Timer phaseTimer) : base(isDisabled, health)
    {
        IsBossActive = isBossActive;
        BossMovement = bossMovement;
        PhaseIndex = phaseIndex;
        ShootBehaviours = shootBehaviours;
        PhaseTimer = phaseTimer;
    }
}