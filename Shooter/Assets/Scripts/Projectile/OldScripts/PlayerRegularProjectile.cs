using UnityEngine;

public class PlayerRegularProjectile : Projectile, IDamager
{
    [field: SerializeField] public float DamageMultiplier { get; set; }

    protected override void Awake()
    {
        base.Awake();

        DamageMultiplier = GameData.ProjectileDamage;
    }

    public override void UpdateUpdateable()
    {
        base.UpdateUpdateable();

        if (Utilities.IsOffscreen(transform.position, SpriteRenderer.bounds.extents, GameData.ScreenRect))
        {
            IsDisabled = true;
        }
    }

    public void OnDamage()
    {
        IsDisabled = true;
    }
}