using System.Collections.Generic;
using UnityEngine;

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

    public GameObject CreateProjectile(GameObject projectilePrefab, Vector3 position, Quaternion rotation)
    {
        var projectileID = projectilePrefab.GetInstanceID();

        if (!ProjectilePool.ContainsKey(projectileID))
        {
            ProjectilePool.Add(projectileID, new Stack<GameObject>());
        }
        
        var projectileStack = ProjectilePool[projectileID];

        GameObject projectileObject;
        if (projectileStack.Count <= 0)
        {
            projectileObject = Instantiate(projectilePrefab, position, rotation);
        }
        else
        {
            projectileObject = projectileStack.Pop();
            
            Transform projectileTransform = projectileObject.transform;
            projectileTransform.position = position;
            projectileTransform.rotation = rotation;
        }
        
        Projectile projectile = projectileObject.GetComponent<Projectile>();

        projectile.PoolID = projectileID;
        projectile.ActivatePoolable();
        _projectiles.Add(projectile);

        return projectileObject;
    }

    public GameObject CreateProjectile(GameObject projectilePrefab, Vector3 position, Quaternion rotation, float speed)
    {
        GameObject projectileObject = CreateProjectile(projectilePrefab, position, rotation);
        
        ProjectileMovement projectileMovement = projectileObject.GetComponent<ProjectileMovement>();
        projectileMovement.Speed = speed;

        return projectileObject;
    }
}