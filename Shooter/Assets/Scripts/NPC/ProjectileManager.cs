using System.Collections.Generic;

public class ProjectileManager
{
    private static ProjectileManager _instance;
    public static ProjectileManager Instance => _instance ??= new ProjectileManager();
    private List<Projectile> _projectiles;

    private ProjectileManager()
    {
        _projectiles = new List<Projectile>();
    }

    public void UpdateProjectiles()
    {
        foreach (var projectile in _projectiles.ToArray())
        {
            projectile.UpdateProjectile();
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
}