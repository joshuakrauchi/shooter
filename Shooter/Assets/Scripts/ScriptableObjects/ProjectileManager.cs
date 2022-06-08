using System;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;

[CreateAssetMenu(fileName = "ProjectileManager", menuName = "ScriptableObjects/ProjectileManager")]
public class ProjectileManager : ScriptableObject
{
    private Dictionary<int, Stack<GameObject>> ProjectilePool { get; set; }

    private readonly List<Projectile> _projectiles;

    private ProjectileManager()
    {
        ProjectilePool = new Dictionary<int, Stack<GameObject>>();

        _projectiles = new List<Projectile>();
    }
    
    public void UpdateProjectiles()
    {
        foreach (Projectile projectile in _projectiles.ToArray())
        {
            projectile.UpdateUpdateable();
        }
    }

    public void AddProjectilePool(GameObject projectilePrefab, uint initialAmount)
    {
        var projectileID = projectilePrefab.GetInstanceID();

        if (!ProjectilePool.ContainsKey(projectileID))
        {
            ProjectilePool.Add(projectileID, new Stack<GameObject>());
        }

        var projectileStack = ProjectilePool[projectileID];

        while (projectileStack.Count < initialAmount)
        {
            projectileStack.Push(Instantiate(projectilePrefab, Utilities.OffscreenPosition, Quaternion.identity));
        }
    }

    public void RemoveProjectile(Projectile projectile)
    {
        _projectiles.Remove(projectile);

        projectile.transform.position = Utilities.OffscreenPosition;
        ProjectilePool[projectile.PoolID].Push(projectile.gameObject);
    }

    public void ClearProjectiles()
    {
        foreach (Projectile projectile in _projectiles.ToArray())
        {
            projectile.DestroyProjectile();
        }
    }


    public void CreateProjectile(Entity entity, Vector3 position, Quaternion rotation)
    {
        EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

        var newEntity = entityManager.Instantiate(entity);
        entityManager.SetComponentData(newEntity, new Translation
        {
            Value = position
        });
        
        entityManager.SetComponentData(newEntity, new Rotation
        {
            Value = rotation
        });
    }
}