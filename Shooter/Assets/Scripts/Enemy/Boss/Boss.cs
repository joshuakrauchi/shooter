using System;
using System.Collections.Generic;

public abstract class Boss : Enemy
{
    public delegate void PhaseBehaviour();

    public BossMovement BossMovement { get; protected set; }
    public List<Tuple<float, PhaseBehaviour>> Phases { get; private set; }
    public int PhaseIndex { get; private set; }
    public bool IsActive { get; protected set; }
    public bool IsNewPhase { get; protected set; }

    protected override void Awake()
    {
        base.Awake();

        BossMovement = GetComponent<BossMovement>();
        Phases = new List<Tuple<float, PhaseBehaviour>>();
        IsNewPhase = true;
    }

    protected override void Record()
    {
        AddTimeData(new BossTimeData(Health, IsDisabled, (ShootBehaviour) ShootBehaviour?.Clone(), PhaseIndex, IsNewPhase, BossMovement.StartPosition, BossMovement.EndPosition, BossMovement.MovementTimer.TotalTime, BossMovement.InitialDelayTimer.TotalTime));

        base.Record();
    }

    protected override void Rewind()
    {
        if (TimeData.Count <= 0) return;

        var timeData = (BossTimeData) TimeData.Last.Value;

        Health = timeData.Health;
        IsDisabled = timeData.IsDisabled;
        ShootBehaviour = timeData.ShootBehaviour;
        PhaseIndex = timeData.PhaseIndex;
        IsNewPhase = timeData.IsNewPhase;
        BossMovement.SetRewindData(timeData.StartPosition, timeData.EndPosition, timeData.TotalTime, timeData.InitialDelay);

        if (TimeData.Count > 1)
        {
            TimeData.Remove(timeData);
        }
    }

    public override void UpdateEnemy()
    {
        if (IsActive && PhaseIndex + 1 < Phases.Count && Phases[PhaseIndex + 1].Item1 >= Health)
        {
            ++PhaseIndex;
            IsNewPhase = true;
        }

        Phases[PhaseIndex].Item2.Invoke();

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

    protected override void DestroySelf()
    {

    }
}