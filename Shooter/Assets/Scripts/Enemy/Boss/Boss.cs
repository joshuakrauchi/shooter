using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Boss : Enemy
{
    public delegate bool PhaseBehaviour();

    public BossMovement BossMovement { get; protected set; }
    public PhaseBehaviour[] Phases { get; protected set; }
    public int PhaseIndex { get; private set; }
    public float MaxHealth { get; private set; }
    public Timer PhaseTimer { get; protected set; }

    protected override void Awake()
    {
        base.Awake();

        BossMovement = GetComponent<BossMovement>();
        MaxHealth = Health;
        Disable();
    }

    protected override void Record()
    {
        var shootClones = new List<ShootBehaviour>();
        foreach (var shootBehaviour in ShootBehaviours)
        {
            shootClones.Add(shootBehaviour.Clone());
        }

        AddTimeData(new BossTimeData(Health, IsDisabled, shootClones, PhaseIndex, BossMovement.StartPosition, BossMovement.EndPosition, BossMovement.MovementTimer.Clone(),
            BossMovement.InitialDelayTimer.Clone()));
    }

    protected override void Rewind()
    {
        if (TimeData.Count <= 0) return;

        var timeData = (BossTimeData) TimeData.Last.Value;

        Health = timeData.Health;
        IsDisabled = timeData.IsDisabled;
        ShootBehaviours = timeData.ShootBehaviours;
        PhaseIndex = timeData.PhaseIndex;
        BossMovement.SetRewindData(timeData.StartPosition, timeData.EndPosition, timeData.TotalTime, timeData.InitialDelay);

        if (TimeData.Count > 1)
        {
            TimeData.Remove(timeData);
        }
    }

    public override void UpdateUpdatable()
    {
        if (Phases[PhaseIndex].Invoke() && PhaseIndex + 1 < Phases.Length)
        {
            ++PhaseIndex;
        }

        PhaseTimer?.UpdateTime(GameState.IsRewinding);

        transform.position = BossMovement.GetMovement(GameState.IsRewinding);
        EnemyCollision.Collider.enabled = !IsDisabled;

        if (!IsDisabled && !GameState.IsRewinding)
        {
            EnemyCollision.UpdateCollision();
            foreach (var shootBehaviour in ShootBehaviours)
            {
                shootBehaviour?.UpdateShoot(transform.position, GameState.IsRewinding);
            }
        }

        UpdateTimeData();
    }

    protected void ActivateBoss()
    {
        IsDisabled = false;
        GameState.BossIsActive = true;
    }

    protected override void Disable()
    {
        IsDisabled = true;
        GameState.BossIsActive = false;
    }
}