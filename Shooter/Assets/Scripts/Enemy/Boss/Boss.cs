using System.Collections.Generic;
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

        BossMovement = null;
        ShootBehaviours = null;
        MaxHealth = Health;
    }

    protected override void Record()
    {
        base.Record();
        
        List<ShootBehaviour> shootClones = null;
        
        if (ShootBehaviours != null)
        {
            shootClones = new List<ShootBehaviour>();
            foreach (ShootBehaviour shootBehaviour in ShootBehaviours)
            {
                shootClones.Add(shootBehaviour.Clone());
            }
        }

        AddTimeData(new BossTimeData(Health, IsDisabled, BossMovement, PhaseIndex, shootClones, PhaseTimer));
    }

    protected override void Rewind(ITimeData timeData)
    {
        base.Rewind(timeData);
        
        BossTimeData bossTimeData = (BossTimeData)timeData;

        ShootBehaviours = bossTimeData.ShootBehaviours;
        PhaseIndex = bossTimeData.PhaseIndex;
        BossMovement = bossTimeData.BossMovement;
        PhaseTimer = bossTimeData.PhaseTimer;
    }

    public override void UpdateUpdateable()
    {
        base.UpdateUpdateable();

        EnemyCollision.enabled = GameState.IsBossActive;
        
        // The phase timer must be rewound before updating the phases, or else it will appear to have been finished.
        PhaseTimer?.UpdateTime(GameState.IsRewinding);

        // Check if the current phase is done (returns true) and that there is another phase.
        if (Phases[PhaseIndex].Invoke())
        {
            if (PhaseIndex + 1 < Phases.Length)
            {
                ++PhaseIndex;
            }
            else
            {
                IsDisabled = true;
            }
        }

        transform.position = BossMovement.GetMovement(GameState.IsRewinding);

        if (!IsDisabled && !GameState.IsRewinding && GameState.IsBossActive)
        {
            foreach (ShootBehaviour shootBehaviour in ShootBehaviours)
            {
                shootBehaviour?.UpdateShoot(transform.position, GameState.IsRewinding);
            }
        }
    }

    protected override void Disable()
    {
        base.Disable();
        
        GameState.IsBossActive = false;
    }

    protected void ResetMovement(Vector2 startPosition, Vector2 endPosition, float totalTime, float initialDelay)
    {
        BossMovement = new BossMovement(startPosition, endPosition, totalTime, initialDelay);
    }
}