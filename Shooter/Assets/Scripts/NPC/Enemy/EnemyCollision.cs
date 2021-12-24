using UnityEngine;

public class EnemyCollision : Collision
{
    private Enemy _enemy;

    protected override void Awake()
    {
        base.Awake();
        
        _enemy = GetComponent<Enemy>();
    }

    protected override void HandleOverlapCollision(Collider2D hit)
    {
        var projectile = hit.gameObject.GetComponent<Projectile>();
        if (projectile && !projectile.IsDisabled && hit.CompareTag("PlayerProjectile"))
        {
            _enemy.OnHit(projectile);
        }
    }
}