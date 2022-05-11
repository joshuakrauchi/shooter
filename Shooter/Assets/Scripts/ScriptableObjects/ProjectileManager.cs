using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Projectile Manager")]
public class ProjectileManager : ScriptableObject
{
    private readonly List<Projectile> _projectiles;

    private ProjectileManager()
    {
        _projectiles = new List<Projectile>();
    }

    public void UpdateProjectiles()
    {
        foreach (Projectile projectile in _projectiles.ToArray())
        {
            projectile.UpdateUpdatable();
        }
    }

    public void AddProjectile(Projectile projectile)
    {
        _projectiles.Add(projectile);
    }

    public void RemoveProjectile(Projectile projectile)
    {
        _projectiles.Remove(projectile);
    }

    public void ClearProjectiles()
    {
        foreach (Projectile projectile in _projectiles.ToArray())
        {
            projectile.DestroyProjectile();
        }
    }
}