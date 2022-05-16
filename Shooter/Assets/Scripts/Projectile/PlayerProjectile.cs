using UnityEngine;

public class PlayerProjectile : Projectile
{
    [field: SerializeField] public float Damage { get; private set; } = 1.0f;

    protected override void Awake()
    {
        base.Awake();

        Damage = GameData.ProjectileDamage;
    }

    public override void UpdateUpdateable()
    {
        base.UpdateUpdateable();

        if (IsOffscreen())
        {
            IsDisabled = true;
        }
    }
}