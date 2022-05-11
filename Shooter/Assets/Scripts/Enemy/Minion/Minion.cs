using System.Collections.Generic;
using UnityEngine;

public class Minion : Enemy
{
    [field: SerializeReference] public ShootBehaviour ShootBehaviour { get; set; }
    public Animator Animator { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        Animator = GetComponent<Animator>();
    }

    public override void UpdateUpdatable()
    {
        base.UpdateUpdatable();
        
        SpriteRenderer.enabled = !IsDisabled;
        EnemyCollision.Collider.enabled = !IsDisabled;

        if (!IsDisabled && !gameState.IsRewinding)
        {
            EnemyCollision.UpdateCollision();
            ShootBehaviour?.UpdateShoot(transform.position, gameState.IsRewinding);
        }

    }

    protected override void Record()
    {
        var shootClones = new List<ShootBehaviour> {ShootBehaviour?.Clone()};

        AddTimeData(new MinionTimeData(health, IsDisabled, shootClones, Animator.GetCurrentAnimatorStateInfo(0).normalizedTime));

        base.Record();
    }

    protected override void Rewind()
    {
        if (CreationTime > GameManager.LevelTime)
        {
            DestroySelf();
        }

        if (TimeData.Count <= 0) return;

        MinionTimeData timeData = (MinionTimeData) TimeData.Last.Value;
        Animator.Play(0, 0, timeData.AnimationTime);

        health = timeData.Health;
        IsDisabled = timeData.IsDisabled;
        ShootBehaviour = timeData.ShootBehaviours[0];
        TimeData.Remove(timeData);
    }
}