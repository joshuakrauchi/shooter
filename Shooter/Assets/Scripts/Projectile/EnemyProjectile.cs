public class EnemyProjectile : Projectile
{
    public override void UpdateUpdateable()
    {
        base.UpdateUpdateable();
        
        IsDisabled = IsOffscreen();
    }
}
