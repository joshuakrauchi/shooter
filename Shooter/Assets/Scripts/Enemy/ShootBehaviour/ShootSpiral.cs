using UnityEngine;

public class ShootSpiral : ShootBehaviour
{
    private ProjectileDefinition ProjectileDefinition;
    private uint _shotsFired;
    private Timer delay;
    private float _currentAngle;

    public ShootSpiral(uint totalCycles, Timer cycleTimer, ProjectileDefinition projectileDefinition) : base(totalCycles, cycleTimer)
    {
        ProjectileDefinition = projectileDefinition;
        delay = new LockedTimer(0.01f);
    }

    public override void UpdateShoot(Vector2 position)
    {
        var directions = 8;
        var NumberOfProjectiles = 5000;
        var AngleBetweenProjectiles = 10f;
        var HalfAngleVariation = 2f;

        CycleTimer.UpdateTime();

        if (!CycleTimer.IsFinished(GameManager.IsRewinding) || TotalCycles != 0 && CurrentCycles == TotalCycles) return;

        delay.UpdateTime();

        if (!delay.IsFinished(false)) return;

        delay.Reset(true);

        var x = 360f / directions;
        for (var i = 0; i < directions; ++i)
        {
            NPCCreator.CreateProjectile(ProjectileDefinition, position, Quaternion.Euler(0f, 0f, _currentAngle + x * i + Random.Range(-HalfAngleVariation, HalfAngleVariation)));
        }

        _currentAngle += AngleBetweenProjectiles;
        ++_shotsFired;

        if (_shotsFired == NumberOfProjectiles)
        {
            ++CurrentCycles;
            CycleTimer.Reset(true);
            _shotsFired = 0;
        }
    }

    public override object Clone()
    {
        return new ShootSpiral(TotalCycles, CycleTimer, ProjectileDefinition);
    }
}