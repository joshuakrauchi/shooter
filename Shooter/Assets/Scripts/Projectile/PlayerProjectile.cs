using UnityEngine;

public class PlayerProjectile : Projectile
{
    [field: SerializeField] public float Damage { get; private set; }

    [SerializeField] private GameData gameData;

    protected override void Awake()
    {
        base.Awake();

        Damage = gameData.ProjectileDamage;
    }

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