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
    
    protected override void Awake()
    {
        base.Awake();

        EnemyCollision = GetComponent<EnemyCollision>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
        EnemyManager.AddEnemy(this);
    }

    public override void UpdateUpdateable()
    {
        base.UpdateUpdateable();
        
        // This must be put here instead of in Rewind() because
        // it may not be called if this TimeObject has no TimeData.
        // This occurs when bosses activate themselves, as the LevelTime
        // doesn't update when a boss is active, but a boss may rewind and
        // set themselves back to inactive, but due to the ordering of events,
        // the time may not decrement successfully and the boss will run out of
        // TimeData before rewinding to a point where they don't exist.
        // Putting this here is pretty failsafe anyways.
        if (CreationTime > GameData.LevelTime)
        {
            DestroySelf();
        }
        
        SpriteRenderer.enabled = !IsDisabled;

        if (!EnemyCollision.Collider.enabled || GameState.IsRewinding) return;

        EnemyCollision.UpdateCollision();
    }

    protected override void Rewind(ITimeData timeData)
    {
        EnemyTimeData enemyTimeData = (EnemyTimeData) timeData;

        Health = enemyTimeData.Health;
        IsDisabled = enemyTimeData.IsDisabled;
    }

    protected override void OnFullyDisabled()
    {
        DestroySelf();
    }

    public void OnHit(PlayerProjectile projectile)
    {
        Health -= projectile.Damage;

        projectile.OnHit();

        if (Health > 0.0f) return;

        Disable();
    }

    /**
     * Note: "Disable" is used as the name of the callback when a minion's animation clip is finished.
     */
    protected void Disable()
    {
        GameData.RewindCharge += RewindRecharge;

        RewindRecharge /= 2.0f;

        if (DroppedObjects.Length > 0)
        {
            NPCCreator.CreateCollectible(DroppedObjects[0], transform.position);
        }

        IsDisabled = true;
    }

    private void DestroySelf()
    {
        EnemyManager.RemoveEnemy(this);
        Destroy(gameObject);
    }
}