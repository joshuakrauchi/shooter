using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Physics.Systems;
using UnityEngine;

public abstract class Enemy : TimeObject
{
    [field: SerializeField] private EnemyManager EnemyManager { get; set; }
    [field: SerializeField] protected float Health { get; set; } = 1.0f;
    [field: SerializeField] protected float RewindRecharge { get; private set; } = 0.1f;

    public float CreationTime { get; set; }

    protected SpriteRenderer SpriteRenderer { get; private set; }
    protected bool HasDied { get; set; }
    
    protected override void Awake()
    {
        base.Awake();

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

        if (IsDisabled || GameState.IsRewinding) return;

        UpdateCollision();
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

    public void OnHit(float damageAmount)
    {
        Health -= damageAmount;
        
        if (Health > 0.0f) return;

        OnZeroHealth();
    }

    protected virtual void OnZeroHealth()
    {
        IsDisabled = true;
    }

    private void DestroySelf()
    {
        EnemyManager.RemoveEnemy(this);
        //Destroy(gameObject);
    }
    
    private void UpdateCollision()
    {
        CollisionWorld collisionWorld = World.DefaultGameObjectInjectionWorld.GetExistingSystem<BuildPhysicsWorld>().PhysicsWorld.CollisionWorld;
        
        var result = collisionWorld.SphereCast(transform.position, 1.0f, new float3(1.0f), 0.0f, out ColliderCastHit hit, CollisionFilter.Default);
        
        EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

        if (!result) return;

        Entity hitEntity = hit.Entity;

        if (!entityManager.HasComponent<PlayerProjectileComponent>(hitEntity)) return;
        
        PlayerProjectileComponent playerProjectileComponent = entityManager.GetComponentData<PlayerProjectileComponent>(hit.Entity);

        OnHit(GameData.ProjectileDamage * playerProjectileComponent.damageMultiplier);
        playerProjectileComponent.hasHitEnemyThisFrame = true;
        entityManager.SetComponentData(hitEntity, playerProjectileComponent);
    }
}