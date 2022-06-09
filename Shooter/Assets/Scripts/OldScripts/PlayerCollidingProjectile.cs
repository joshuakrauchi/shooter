using UnityEngine;

public class PlayerCollidingProjectile : Projectile, IDamager
{
    [field: SerializeField] public float DamageMultiplier { get; set; }

    private PlayerProjectileCollision PlayerProjectileCollision { get; set; }
    
    protected override void Awake()
    {
        base.Awake();

        PlayerProjectileCollision = GetComponent<PlayerProjectileCollision>();
    }

    public override void UpdateUpdateable()
    {
        base.UpdateUpdateable();

        if (Utilities.IsOffscreen(transform.position, SpriteRenderer.bounds.extents, GameData.ScreenRect))
        {
            IsDisabled = true;
        }

        if (!GameState.IsRewinding)
        {
            PlayerProjectileCollision.UpdateCollision();
        }
    }

    public void OnDamage()
    {
        // This projectile is piercing.
    }
}