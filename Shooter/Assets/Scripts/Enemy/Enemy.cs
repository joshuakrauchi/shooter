using UnityEngine;

public abstract class Enemy : TimeObject
{
    [SerializeField] protected EnemyManager enemyManager;
    [SerializeField] protected float health = 1f;
    [SerializeField] private float rewindRecharge = 0.1f;
    [SerializeField] protected GameObject[] droppedObjects;
    
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
        enemyManager.AddEnemy(this);
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
        health -= projectile.Damage;

        projectile.OnHit();

        if (health > 0.0f) return;
        
        Disable();
    }

    protected virtual void Disable()
    {
        gameData.RewindCharge += rewindRecharge;

        rewindRecharge /= 2f;
        
        if (droppedObjects.Length > 0)
        {
            NPCCreator.CreateCollectible(droppedObjects[0], transform.position);
        }

        IsDisabled = true;
    }

    protected void DestroySelf()
    {
        enemyManager.RemoveEnemy(this);
        Destroy(gameObject);
    }
}