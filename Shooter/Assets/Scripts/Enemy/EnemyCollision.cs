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
        if (hit.GetComponent<IDamager>() is { } damager)
        {
            _enemy.OnHit(damager.Damage);
            
            damager.OnDamage();
        }
    }
}