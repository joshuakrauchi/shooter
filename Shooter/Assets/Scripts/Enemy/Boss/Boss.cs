using System.Collections.Generic;
using UnityEngine;

public abstract class Boss : Enemy
{
    public delegate bool PhaseBehaviour();

    [field: SerializeReference] public List<ShootBehaviour> ShootBehaviours { get; set; }

    public BossMovement BossMovement { get; protected set; }
    public PhaseBehaviour[] Phases { get; protected set; }
    public Timer PhaseTimer { get; protected set; }
    public int PhaseIndex { get; private set; }
    public float MaxHealth { get; private set; }

    [SerializeField] protected UIManager uiManager;

    protected override void Awake()
    {
        base.Awake();

        ShootBehaviours = new List<ShootBehaviour>();
        BossMovement = GetComponent<BossMovement>();
        MaxHealth = health;
        Disable();
    }

    protected override void Record()
    {
        var shootClones = new List<ShootBehaviour>();
        foreach (ShootBehaviour shootBehaviour in ShootBehaviours)
        {
            shootClones.Add(shootBehaviour.Clone());
        }

        AddTimeData(new BossTimeData(health, IsDisabled, shootClones, PhaseIndex, BossMovement.StartPosition, BossMovement.EndPosition, BossMovement.MovementTimer.Clone(),
            BossMovement.InitialDelayTimer.Clone()));
    }

    protected override void Rewind()
    {
        if (TimeData.Count <= 0) return;

        BossTimeData timeData = (BossTimeData) TimeData.Last.Value;

        health = timeData.Health;
        IsDisabled = timeData.IsDisabled;
        ShootBehaviours = timeData.ShootBehaviours;
        PhaseIndex = timeData.PhaseIndex;
        BossMovement.SetRewindData(timeData.StartPosition, timeData.EndPosition, timeData.TotalTime, timeData.InitialDelay);

        if (TimeData.Count > 1)
        {
            TimeData.Remove(timeData);
        }
    }

    public override void UpdateUpdateable()
    {
        base.UpdateUpdateable();
        
        if (Phases[PhaseIndex].Invoke() && PhaseIndex + 1 < Phases.Length)
        {
            ++PhaseIndex;
        }

        PhaseTimer?.UpdateTime(gameState.IsRewinding);

        transform.position = BossMovement.GetMovement(gameState.IsRewinding);
        EnemyCollision.Collider.enabled = !IsDisabled;

        if (!IsDisabled && !gameState.IsRewinding)
        {
            EnemyCollision.UpdateCollision();
            foreach (ShootBehaviour shootBehaviour in ShootBehaviours)
            {
                shootBehaviour?.UpdateShoot(transform.position, gameState.IsRewinding);
            }
        }

    }

    protected void ActivateBoss()
    {
        IsDisabled = false;
        gameState.IsBossActive = true;
    }

    protected override void Disable()
    {
        IsDisabled = true;
        gameState.IsBossActive = false;
    }
}