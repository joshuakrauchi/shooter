using UnityEngine;

public abstract class Enemy : TimeObject
{
    [field: SerializeField] private EnemyManager EnemyManager { get; set; }
    [field: SerializeField] protected float Health { get; set; } = 1.0f;
    [field: SerializeField] private float RewindRecharge { get; set; } = 0.1f;
    [field: SerializeField] private GameObject[] DroppedObjects { get; set; }

    public float CreationTime { get; set; }

    protected EnemyCollision EnemyCollision { get; private set; }
    protected SpriteRenderer SpriteRenderer { get; private set; }

    // If disabled, collision and updating are disabled for this object.
    protected bool IsDisabled;

    protected override void Awake()
    {
        base.Awake();

        EnemyCollision = GetComponent<EnemyCollision>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
        EnemyManager.AddEnemy(this);
    }

    protected override void Record()
    {
        // If never rewound to a non-dead state, it has been dead for a full cycle and can never be revived.
        if (((EnemyTimeData) TimeData.First.Value).IsDisabled)
        {
            DestroySelf();
        }
    }

    public void OnHit(PlayerProjectile projectile)
    {
        Health -= projectile.Damage;

        projectile.OnHit();

        if (Health > 0.0f) return;

        Disable();
    }

    protected virtual void Disable()
    {
        GameData.RewindCharge += RewindRecharge;

        RewindRecharge /= 2.0f;

        if (DroppedObjects.Length > 0)
        {
            NPCCreator.CreateCollectible(DroppedObjects[0], transform.position);
        }

        IsDisabled = true;
    }

    protected void DestroySelf()
    {
        EnemyManager.RemoveEnemy(this);
        Destroy(gameObject);
    }
}