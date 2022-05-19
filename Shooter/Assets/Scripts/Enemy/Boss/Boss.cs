using UnityEngine;

public abstract class Boss : Enemy
{
    protected delegate bool PhaseBehaviour();

    [field: SerializeReference] protected ShootBehaviour[] ShootBehaviours { get; set; }
    [field: SerializeField] protected UIManager UIManager { get; set; }

    protected float MaxHealth { get; private set; }
    protected BossMovement BossMovement { get; private set; }
    protected PhaseBehaviour[] Phases { get; set; }
    protected Timer PhaseTimer { get; set; }

    private int PhaseIndex { get; set; }

    protected override void Awake()
    {
        base.Awake();

        BossMovement = new BossMovement(transform.position);
        ShootBehaviours = GetComponents<ShootBehaviour>();
        MaxHealth = Health;
        PhaseTimer = new Timer(0.0f);
    }

    protected override void Record()
    {
        var numberOfShootBehaviours = ShootBehaviours.Length;
        var shootTimeData = new ShootBehaviour.ShootTimeData[numberOfShootBehaviours];

        for (var i = 0; i < numberOfShootBehaviours; ++i)
        {
            ShootBehaviour shootBehaviour = ShootBehaviours[i];
            if (!shootBehaviour.enabled) return;
            
            shootTimeData[i] = shootBehaviour.GetRecordData();
        }

        AddTimeData(new BossTimeData(IsDisabled, Health, GameState.IsBossActive, BossMovement, PhaseIndex, PhaseTimer, shootTimeData));
    }

    protected override void Rewind(ITimeData timeData)
    {
        base.Rewind(timeData);

        BossTimeData bossTimeData = (BossTimeData) timeData;

        GameState.IsBossActive = bossTimeData.IsBossActive;
        BossMovement = bossTimeData.BossMovement;
        PhaseIndex = bossTimeData.PhaseIndex;
        PhaseTimer = bossTimeData.PhaseTimer;

        for (var i = 0; i < bossTimeData.ShootTimeData.Length; ++i)
        {
            ShootBehaviour.ShootTimeData shootTimeData = bossTimeData.ShootTimeData[i];
            // If the ShootTimeData is null/default, the corresponding ShootBehaviour was disabled at the time of recording.
            ShootBehaviours[i].enabled = shootTimeData != default;

            if (shootTimeData == default) return;
            
            ShootBehaviours[i].SetRewindData(bossTimeData.ShootTimeData[i]);
        }
    }

    public override void UpdateUpdateable()
    {
        base.UpdateUpdateable();

        EnemyCollision.Collider.enabled = !IsDisabled && GameState.IsBossActive;

        if (IsDisabled) return;

        // The phase timer must be rewound before updating the phases, or else it will appear to have been finished.
        PhaseTimer.UpdateTime(GameState.IsRewinding);

        // Check if the current phase is done (returns true) and that there is another phase.
        if (Phases[PhaseIndex].Invoke())
        {
            ++PhaseIndex;

            if (PhaseIndex >= Phases.Length)
            {
                IsDisabled = true;
            }
        }

        transform.position = BossMovement.GetMovement(GameState.IsRewinding);

        if (!GameState.IsBossActive) return;

        foreach (ShootBehaviour shootBehaviour in ShootBehaviours)
        {
            if (!shootBehaviour.enabled) return;
            
            shootBehaviour.UpdateShoot(GameState.IsRewinding);
        }
    }

    protected void ResetMovement(Vector2 startPosition, Vector2 endPosition, float totalTime, float initialDelay)
    {
        BossMovement = new BossMovement(startPosition, endPosition, totalTime, initialDelay);
    }
}