using UnityEngine;

public class Minion : Enemy
{
    [field: SerializeField] private GameObject[] DroppedObjects { get; set; }
    [field: SerializeField] private float DropRate { get; set; } = 0.5f;
    
    public Animator Animator { get; private set; }
    public float AnimationLength { get; set; } = 1.0f;

    private ShootBehaviour ShootBehaviour { get; set; }

    private static readonly int MotionTime = Animator.StringToHash("MotionTime");

    protected override void Awake()
    {
        base.Awake();

        Animator = GetComponent<Animator>();
        ShootBehaviour = GetComponent<ShootBehaviour>();
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

        if (IsDisabled) return;

        ShootBehaviour.UpdateShoot(GameState.IsRewinding);
    }

    protected override void Record()
    {
        AddTimeData(new MinionTimeData(IsDisabled, Health, ShootBehaviour.GetRecordData()));
    }

    protected override void Rewind(ITimeData timeData)
    {
        base.Rewind(timeData);

        MinionTimeData minionTimeData = (MinionTimeData) timeData;
        ShootBehaviour.SetRewindData(minionTimeData.ShootTimeData);
    }

    protected override void OnZeroHealth()
    {
        base.OnZeroHealth();
        
        if (HasDied) return;
        
        GameData.RewindCharge += RewindRecharge;
        
        if (DroppedObjects.Length > 0 && Random.value >= DropRate)
        {
            NPCCreator.CreateCollectible(DroppedObjects[Random.Range(0, DroppedObjects.Length)], transform.position);
        }
        
        HasDied = true;
    }
}