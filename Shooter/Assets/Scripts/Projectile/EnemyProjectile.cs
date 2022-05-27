public class EnemyProjectile : Projectile
{
    public override void UpdateUpdateable()
    {
        base.UpdateUpdateable();
        
        if (Utilities.IsOffscreen(transform.position, SpriteRenderer.bounds.extents, GameData.ScreenRect))
        {
            IsDisabled = true;
        }
    }

    public void OnHit()
    {
        IsDisabled = true;
    }
}