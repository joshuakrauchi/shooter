using UnityEngine;

public class PlayerProjectile : Projectile
{
    public override void UpdateUpdatable()
    {
        SpriteRenderer.enabled = !IsDisabled;
        Collider.enabled = !IsDisabled;

        ProjectileMovement.UpdateMovement();
        if (IsOffscreen())
        {
            IsDisabled = true;
        }

        UpdateTimeData();
    }
}