using UnityEngine;

public class ShootRain : ShootBehaviour
{
    private ProjectileDefinition _projectile;

    public ShootRain(uint totalCycles, Timer cycleTimer, ProjectileDefinition projectileDefinition) : base(totalCycles, cycleTimer)
    {
        _projectile = projectileDefinition;
    }

    public override void UpdateShoot(Vector2 position)
    {

    }

    public override object Clone()
    {
        return new ShootRain(TotalCycles, CycleTimer, _projectile);
    }
}