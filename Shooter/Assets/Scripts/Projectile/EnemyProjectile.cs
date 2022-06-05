public class EnemyProjectile : Projectile
{
    public override void UpdateUpdateable()
    {
        base.UpdateUpdateable();

        IsDisabled = Utilities.IsOffscreen(transform.position, SpriteRenderer.bounds.extents, GameData.ScreenRect);
    }

    public void OnHit()
    {
        IsDisabled = true;
    }
}