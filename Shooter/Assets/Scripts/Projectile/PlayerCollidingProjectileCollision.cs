using UnityEngine;

public class PlayerCollidingProjectileCollision : Collision
{
    protected override void HandleOverlapCollision(Collider2D hit)
    {
        if (hit.GetComponent<EnemyProjectile>() is { } enemyProjectile)
        {
            enemyProjectile.OnHit();
        }
    }
}