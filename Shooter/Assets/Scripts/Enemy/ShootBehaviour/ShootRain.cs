using UnityEngine;

public class ShootRain : ShootBehaviour
{
    [SerializeField] private ProjectileDefinition ProjectileDefinition;
    private float _currentAngle;
    private Timer delay;
    private uint _shotsFired;
    [SerializeField] private uint NumberOfProjectiles;


    public ShootRain(uint totalCycles, Timer cycleTimer, ProjectileDefinition projectileDefinition) : base(totalCycles, cycleTimer)
    {
        ProjectileDefinition = projectileDefinition;
        delay = new LockedTimer(0f);
        NumberOfProjectiles = 20;
    }

    public override void UpdateShoot(Vector2 position)
    {
        var directions = 4f;
        var AngleBetweenProjectiles = 10f;
        var HalfAngleVariation = 1f;

        CycleTimer.UpdateTime();

        if (!CycleTimer.IsFinished(GameManager.IsRewinding) || TotalCycles != 0 && CurrentCycles == TotalCycles) return;

        delay.UpdateTime();
        if (!delay.IsFinished(false)) return;

        var x = 360f / directions;
        for (var i = 0; i < directions; ++i)
        {
            NPCCreator.CreateProjectile(ProjectileDefinition, position, Quaternion.Euler(0f, 0f, Random.Range(-180f, 180f) + Random.Range(-HalfAngleVariation, HalfAngleVariation)));
        }

        _currentAngle += AngleBetweenProjectiles;
        ++_shotsFired;

        delay.Reset(true);

        if (_shotsFired == NumberOfProjectiles)
        {
            ++CurrentCycles;
            CycleTimer.Reset(true);
            _shotsFired = 0;
        }
    }

    public override object Clone()
    {
        return new ShootRain(TotalCycles, CycleTimer, ProjectileDefinition);
    }
}