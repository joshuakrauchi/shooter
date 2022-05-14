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

        EnemyCollision.Collider.enabled = !IsDisabled;

        if (IsDisabled || GameState.IsRewinding) return;

        ShootBehaviour?.UpdateShoot(transform.position, GameState.IsRewinding);
    }

    protected override void Record()
    {
        ShootBehaviour shootClone = ShootBehaviour?.Clone();

        AddTimeData(new MinionTimeData(IsDisabled, Health, Animator.GetCurrentAnimatorStateInfo(0).normalizedTime, shootClone));
    }

    protected override void Rewind(ITimeData timeData)
    {
        base.Rewind(timeData);

        MinionTimeData minionTimeData = (MinionTimeData) timeData;
        Animator.Play(0, 0, minionTimeData.AnimationTime);
        ShootBehaviour = minionTimeData.ShootBehaviour;
    }
}