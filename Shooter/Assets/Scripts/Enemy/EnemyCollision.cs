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
        if (hit.GetComponent<PlayerProjectile>() is { } projectile)
        {
            _enemy.OnHit(projectile);
        }
    }
}