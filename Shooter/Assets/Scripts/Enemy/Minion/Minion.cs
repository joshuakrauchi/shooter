using UnityEngine;

public class Minion : Enemy
{
    [field: SerializeReference] public ShootBehaviour ShootBehaviour { get; set; }
    
    public Animator Animator { get; private set; }
    public float AnimationLength { get; set; } = 1.0f;

    private static readonly int MotionTime = Animator.StringToHash("MotionTime");

    protected override void Awake()
    {
        base.Awake();

        Animator = GetComponent<Animator>();
    }

    public override void UpdateUpdateable()
    {
        base.UpdateUpdateable();

        var newMotionTime = (GameData.LevelTime - CreationTime) / AnimationLength;

        if (!IsDisabled && newMotionTime >= 1.0f)
        {
            IsDisabled = true;
        }
        
        Animator.SetFloat(MotionTime, newMotionTime);

        EnemyCollision.Collider.enabled = !IsDisabled;

        if (IsDisabled || GameState.IsRewinding) return;

        ShootBehaviour?.UpdateShoot(transform.position, GameState.IsRewinding);
    }

    protected override void Record()
    {
        ShootBehaviour shootClone = ShootBehaviour?.Clone();
        
        AddTimeData(new MinionTimeData(IsDisabled, Health, shootClone));
    }

    protected override void Rewind(ITimeData timeData)
    {
        base.Rewind(timeData);

        MinionTimeData minionTimeData = (MinionTimeData) timeData;
        ShootBehaviour = minionTimeData.ShootBehaviour;
    }
}