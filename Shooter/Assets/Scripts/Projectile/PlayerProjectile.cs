public class PlayerProjectile : Projectile
{
    public override void UpdateUpdatable()
    {
        SpriteRenderer.enabled = !IsDisabled;
        Collider.enabled = !IsDisabled;

        ProjectileMovement.UpdateMovement(gameState.IsRewinding);
        if (IsOffscreen())
        {
            IsDisabled = true;
        }

        UpdateTimeData();
    }
}