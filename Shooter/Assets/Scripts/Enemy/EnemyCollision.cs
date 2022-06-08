using System;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using UnityEngine;
using Collider = UnityEngine.Collider;
using RaycastHit = Unity.Physics.RaycastHit;

public class EnemyCollision : MonoBehaviour
{
    private void CastMe()
    {
        CollisionWorld collisionWorld = World.DefaultGameObjectInjectionWorld.GetExistingSystem<Unity.Physics.Systems.BuildPhysicsWorld>().PhysicsWorld.CollisionWorld;
        ColliderCastHit hit;
        var result = collisionWorld.SphereCast(transform.position, 1.0f, new float3(1.0f), 0.0f, out hit, CollisionFilter.Default);
        
        EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

        if (!result) return;

        if (!entityManager.HasComponent<ProjectileComponent>(hit.Entity)) return;
        
        ProjectileComponent projectileComponent = entityManager.GetComponentData<ProjectileComponent>(hit.Entity);

        Enemy.OnHit(GameData.ProjectileDamage);
    }
    
    [field: SerializeField] private GameData GameData { get; set; }
    private Enemy Enemy { get; set; }

    protected void Awake()
    {
        Enemy = GetComponent<Enemy>();
    }

    public void FixedUpdate()
    {
        CastMe();
    }

    protected void HandleOverlapCollision(Collider hit)
    {
        Debug.Log(hit.name);
        if (hit.GetComponent<IDamager>() is { } damager)
        {
            Enemy.OnHit(GameData.ProjectileDamage * damager.DamageMultiplier);
            
            damager.OnDamage();
        }
    }
}