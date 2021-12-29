using UnityEngine;

public class ShootRain : ShootBehaviour
{
    private ProjectileDefinition ProjectileDefinition;
    private float _currentAngle;
    private Timer delay;

    public ShootRain(uint totalCycles, Timer cycleTimer, ProjectileDefinition projectileDefinition) : base(totalCycles, cycleTimer)
    {
        ProjectileDefinition = projectileDefinition;
        delay = new LockedTimer(0.01f);
    }

    public override void UpdateShoot(Vector2 position)
    {
        var directions = 4f;
        var AngleBetweenProjectiles = 10f;
        var HalfAngleVariation = 1f;

        delay.UpdateTime();
        if (!delay.IsFinished(false)) return;

        var x = 360f / directions;
        for (var i = 0; i < directions; ++i)
        {
            var proj1 = NPCCreator.CreateProjectile(ProjectileDefinition, position, Quaternion.Euler(0f, 0f, Random.Range(-180f, 180f) + Random.Range(-HalfAngleVariation, HalfAngleVariation)));
            proj1.XSpeed = 0.2f;
        }

        _currentAngle += AngleBetweenProjectiles;

        delay.Reset(true);
    }

    public override object Clone()
    {
        return new ShootRain(TotalCycles, CycleTimer, ProjectileDefinition);
    }
}