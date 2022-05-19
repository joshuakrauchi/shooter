using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Boss : Enemy
{
    protected delegate bool PhaseBehaviour();

    [field: SerializeReference] protected List<ShootBehaviour> ShootBehaviours { get; set; }
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
        ShootBehaviours = new List<ShootBehaviour>();
        MaxHealth = Health;
        PhaseTimer = new Timer(0.0f);
    }

    protected override void Record()
    {
        return;
        List<ShootBehaviour> shootClones = null;

        if (ShootBehaviours != null)
        {
            //shootClones = ShootBehaviours.Select(shootBehaviour => shootBehaviour.Clone()).ToList();
        }

        AddTimeData(new BossTimeData(IsDisabled, Health, GameState.IsBossActive, BossMovement, PhaseIndex, shootClones, PhaseTimer));
    }

    protected override void Rewind(ITimeData timeData)
    {
        return;
        
        base.Rewind(timeData);

        BossTimeData bossTimeData = (BossTimeData) timeData;

        GameState.IsBossActive = bossTimeData.IsBossActive;
        ShootBehaviours = bossTimeData.ShootBehaviours;
        PhaseIndex = bossTimeData.PhaseIndex;
        BossMovement = bossTimeData.BossMovement;
        PhaseTimer = bossTimeData.PhaseTimer;
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

        if (GameState.IsRewinding || !GameState.IsBossActive) return;

        foreach (ShootBehaviour shootBehaviour in ShootBehaviours)
        {
            shootBehaviour.UpdateShoot(GameState.IsRewinding);
        }
    }

    protected void ResetMovement(Vector2 startPosition, Vector2 endPosition, float totalTime, float initialDelay)
    {
        BossMovement = new BossMovement(startPosition, endPosition, totalTime, initialDelay);
    }
}