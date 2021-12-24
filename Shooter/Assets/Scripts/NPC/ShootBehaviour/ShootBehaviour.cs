using System.Collections.Generic;
using UnityEngine;

public abstract class ShootBehaviour
{
    public abstract void UpdateShoot(IReadOnlyList<ProjectileDefinition> projectileDefinitions, Vector2 position);
}