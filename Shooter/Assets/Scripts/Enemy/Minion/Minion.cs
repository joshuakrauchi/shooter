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

    public override void UpdateUpdateable()
    {
        base.UpdateUpdateable();
        
        

        if (!IsDisabled && !GameState.IsRewinding)
        {
            EnemyCollision.UpdateCollision();
            ShootBehaviour?.UpdateShoot(transform.position, GameState.IsRewinding);
        }

    }

    protected override void Record()
    {
        base.Record();

        ShootBehaviour shootClone = ShootBehaviour?.Clone();

        AddTimeData(new MinionTimeData(Health, IsDisabled, Animator.GetCurrentAnimatorStateInfo(0).normalizedTime, shootClone));
    }

    protected override void Rewind(ITimeData timeData)
    {
        base.Rewind(timeData);
        
        if (CreationTime > GameData.LevelTime)
        {
            DestroySelf();
        }

        MinionTimeData minionTimeData = (MinionTimeData) TimeData.Last.Value;
        Animator.Play(0, 0, minionTimeData.AnimationTime);
        ShootBehaviour = minionTimeData.ShootBehaviour;
    }

    protected override void Disable()
    {
        base.Disable();
        
        IsDisabled = true;
    }
}