using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Boss : Enemy
{
    public delegate bool PhaseBehaviour();

    public BossMovement BossMovement { get; protected set; }
    public List<PhaseBehaviour> Phases { get; private set; }
    public int PhaseIndex { get; private set; }
    public bool IsActive { get; protected set; }
    public float MaxHealth { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        BossMovement = GetComponent<BossMovement>();
        Phases = new List<PhaseBehaviour>();
        MaxHealth = Health;
    }

    protected override void Record()
    {
        var shootClones = new List<ShootBehaviour>();
        foreach (var shootBehaviour in ShootBehaviours)
        {
            shootClones.Add((ShootBehaviour) shootBehaviour.Clone());
        }

        AddTimeData(new BossTimeData(Health, IsDisabled, shootClones, PhaseIndex, BossMovement.StartPosition, BossMovement.EndPosition, (Timer) BossMovement.MovementTimer.Clone(), (Timer) BossMovement.InitialDelayTimer.Clone()));

        base.Record();
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

    public override void UpdateEnemy()
    {
        if (Phases[PhaseIndex].Invoke() && PhaseIndex + 1 < Phases.Count)
        {
            ++PhaseIndex;
        }

        transform.position = BossMovement.GetMovement();

        if (IsActive)
        {
            base.UpdateEnemy();
        }
        else
        {
            UpdateTimeData();
        }
    }

    protected override void Disable()
    {
        IsActive = false;
        IsDisabled = true;
    }
}