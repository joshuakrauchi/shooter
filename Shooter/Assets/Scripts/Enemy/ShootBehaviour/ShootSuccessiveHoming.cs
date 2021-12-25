using UnityEngine;

public class ShootSuccessiveHoming : ShootBehaviour
{
    public ProjectileDefinition ProjectileDefinition { get; private set; }
    public uint NumberOfProjectiles { get; private set; }
    public float AngleBetweenProjectiles { get; private set; }
    public float HalfAngleVariation { get; private set; }
    public float TimeBetweenShots { get; private set; }

    private uint _shotsFired;
    private float _currentDelay;
    private float _currentAngle;

    public ShootSuccessiveHoming(uint totalCycles, Timer cycleTimer, ProjectileDefinition projectileDefinition, uint numberOfProjectiles, float angleBetweenProjectiles, float angleVariation, float timeBetweenShots) : base(totalCycles, cycleTimer)
    {
        ProjectileDefinition = projectileDefinition;
        NumberOfProjectiles = numberOfProjectiles;
        AngleBetweenProjectiles = angleBetweenProjectiles;
        HalfAngleVariation = angleVariation / 2f;
        TimeBetweenShots = timeBetweenShots;
    }

    public override void UpdateShoot(Vector2 position)
    {
        CycleTimer.UpdateTime();

        if (!CycleTimer.IsFinished(GameManager.IsRewinding) || TotalCycles != 0 && CurrentCycles == TotalCycles) return;

        _currentDelay += Time.deltaTime;

        if (_currentDelay < TimeBetweenShots) return;

        _currentDelay -= TimeBetweenShots;

        if (_shotsFired == 0)
        {
            var direction = (Vector2)GameManager.Player.transform.position - position;
            _currentAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - AngleBetweenProjectiles * Mathf.Floor(NumberOfProjectiles / 2f);
        }

        NPCCreator.CreateProjectile(ProjectileDefinition, position, Quaternion.Euler(0f, 0f, _currentAngle + Random.Range(-HalfAngleVariation, HalfAngleVariation)));
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
        return new ShootSuccessiveHoming(TotalCycles, CycleTimer, ProjectileDefinition, NumberOfProjectiles, AngleBetweenProjectiles, 0f, TimeBetweenShots)
        {
            CurrentCycles = CurrentCycles,
            HalfAngleVariation = HalfAngleVariation,
            _shotsFired = _shotsFired,
            _currentAngle = _currentAngle,
            _currentDelay = _currentDelay
        };
    }
}