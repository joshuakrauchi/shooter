using System.Collections.Generic;

public class BossTimeData : EnemyTimeData
{
    public bool IsBossActive { get; }
    public BossMovement BossMovement { get; }
    public int PhaseIndex { get; }
    public Timer PhaseTimer { get; }
    public ShootBehaviour.ShootTimeData[] ShootTimeData { get; }

    public BossTimeData(bool isDisabled, float health, bool isBossActive, BossMovement bossMovement, int phaseIndex, Timer phaseTimer, ShootBehaviour.ShootTimeData[] shootTimeData) : base(isDisabled, health)
    {
        IsBossActive = isBossActive;
        BossMovement = bossMovement;
        PhaseIndex = phaseIndex;
        PhaseTimer = phaseTimer;
        ShootTimeData = shootTimeData;
    }
}