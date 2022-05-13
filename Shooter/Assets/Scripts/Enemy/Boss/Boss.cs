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

        ShootBehaviours = new List<ShootBehaviour>();
        MaxHealth = Health;
        Disable();
    }

    protected override void Record()
    {
        var shootClones = new List<ShootBehaviour>();
        foreach (ShootBehaviour shootBehaviour in ShootBehaviours)
        {
            shootClones.Add(shootBehaviour.Clone());
        }

        AddTimeData(new BossTimeData(Health, IsDisabled, shootClones, PhaseIndex, BossMovement, PhaseTimer));
    }

    protected override void Rewind()
    {
        if (TimeData.Count <= 0) return;

        BossTimeData timeData = (BossTimeData) TimeData.Last.Value;

        Health = timeData.Health;
        IsDisabled = timeData.IsDisabled;
        ShootBehaviours = timeData.ShootBehaviours;
        PhaseIndex = timeData.PhaseIndex;
        BossMovement = timeData.BossMovement;
        PhaseTimer = timeData.PhaseTimer;
        
        TimeData.Remove(timeData);
    }

    public override void UpdateUpdateable()
    {
        base.UpdateUpdateable();

        // The phase timer must be rewound before updating the phases, or else it will appear to have been finished.
        PhaseTimer?.UpdateTime(GameState.IsRewinding);

        // Check if the current phase is done (returns true) and that there is another phase.
        if (Phases[PhaseIndex].Invoke() && PhaseIndex + 1 < Phases.Length)
        {
            ++PhaseIndex;
        }
        
        transform.position = BossMovement.GetMovement(GameState.IsRewinding);
        EnemyCollision.Collider.enabled = !IsDisabled;

        if (!IsDisabled && !GameState.IsRewinding)
        {
            EnemyCollision.UpdateCollision();
            foreach (ShootBehaviour shootBehaviour in ShootBehaviours)
            {
                shootBehaviour?.UpdateShoot(transform.position, GameState.IsRewinding);
            }
        }
    }

    protected void ActivateBoss()
    {
        IsDisabled = false;
        GameState.IsBossActive = true;
    }

    protected override void Disable()
    {
        IsDisabled = true;
        GameState.IsBossActive = false;
    }

    protected void ResetMovement(Vector2 startPosition, Vector2 endPosition, float totalTime, float initialDelay)
    {
        BossMovement = new BossMovement(startPosition, endPosition, totalTime, initialDelay);
    }
}