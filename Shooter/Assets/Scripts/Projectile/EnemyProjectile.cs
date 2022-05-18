public class EnemyProjectile : Projectile
{
    public override void UpdateUpdateable()
    {
        base.UpdateUpdateable();
        
        // Unlike player projectiles, enemy projectiles are only disabled when they go offscreen, as they don't disappear when they hit the player.
        IsDisabled = Utilities.IsOffscreen(transform.position, SpriteRenderer.bounds.extents, GameData.ScreenRect);
    }
}