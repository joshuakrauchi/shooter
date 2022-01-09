using UnityEngine;

public class PlayerProjectile : Projectile
{
    public override void UpdateUpdatable()
    {
        SpriteRenderer.enabled = !IsDisabled;
        Collider.enabled = !IsDisabled;

        ProjectileMovement.UpdateMovement(GameState.IsRewinding);
        if (IsOffscreen())
        {
            IsDisabled = true;
        }

        UpdateTimeData();
    }
}