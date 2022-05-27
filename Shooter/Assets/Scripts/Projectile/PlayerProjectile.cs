using UnityEngine;

public abstract class PlayerProjectile : Projectile
{
    [field: SerializeField] public float Damage { get; protected set; } = 1.0f;
}