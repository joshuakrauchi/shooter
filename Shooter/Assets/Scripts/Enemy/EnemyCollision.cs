using UnityEngine;

public class EnemyCollision : Collision
{
    [field: SerializeField] private GameData GameData { get; set; }
    private Enemy Enemy { get; set; }

    protected override void Awake()
    {
        base.Awake();

        Enemy = GetComponent<Enemy>();
    }

    protected override void HandleOverlapCollision(Collider2D hit)
    {
        if (hit.GetComponent<IDamager>() is { } damager)
        {
            Enemy.OnHit(GameData.ProjectileDamage * damager.DamageMultiplier);
            
            damager.OnDamage();
        }
    }
}