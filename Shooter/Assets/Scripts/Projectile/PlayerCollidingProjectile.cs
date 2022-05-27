public class PlayerCollidingProjectile : PlayerProjectile
{
    private PlayerCollidingProjectileCollision PlayerCollidingProjectileCollision { get; set; }
    
    protected override void Awake()
    {
        base.Awake();

        PlayerCollidingProjectileCollision = GetComponent<PlayerCollidingProjectileCollision>();
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
            PlayerCollidingProjectileCollision.UpdateCollision();
        }
    }
}