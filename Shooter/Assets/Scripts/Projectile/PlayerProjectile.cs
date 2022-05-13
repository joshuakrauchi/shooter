using UnityEngine;

public class PlayerProjectile : Projectile
{
    [field: SerializeField] public float Damage { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        Damage = GameData.ProjectileDamage;
    }

    public override void UpdateUpdateable()
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