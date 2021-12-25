using System.Collections.Generic;
using UnityEngine;

public class Minion : Enemy
{
    public Animator Animator { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        Animator = GetComponent<Animator>();
    }

    protected override void Record()
    {
        var shootClones = new List<ShootBehaviour>();
        foreach (var shootBehaviour in ShootBehaviours)
        {
            shootClones.Add((ShootBehaviour) shootBehaviour.Clone());
        }

        AddTimeData(new MinionTimeData(Health, IsDisabled, shootClones, Animator.GetCurrentAnimatorStateInfo(0).normalizedTime));

        base.Record();
    }

    protected override void Rewind()
    {
        if (CreationTime > GameManager.LevelTime)
        {
            DestroySelf();
        }

        if (TimeData.Count <= 0) return;

        var timeData = (MinionTimeData) TimeData.Last.Value;
        Animator.Play(0, 0, timeData.AnimationTime);
        Health = timeData.Health;
        IsDisabled = timeData.IsDisabled;
        ShootBehaviours = timeData.ShootBehaviours;
        TimeData.Remove(timeData);
    }

    protected override void DestroySelf()
    {
        EnemyManager.Instance.RemoveEnemy(this);
        Destroy(gameObject);
    }
}