using System;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class ShootSuccessiveHoming : ShootBehaviour
{
    [SerializeField] private ProjectileDefinition projectileDefinition;
    [SerializeField] private Timer betweenShotsTimer;
    [SerializeField] private uint numberOfProjectiles;
    [SerializeField] private float angleBetweenProjectiles;
    [SerializeField] private float angleVariation;

    private GameObject _target;
    private uint _shotsFired;
    private float _currentAngle;

    public ShootSuccessiveHoming(GameObject target, uint totalCycles, Timer cycleTimer, ProjectileDefinition projectileDefinition, Timer betweenShotsTimer, uint numberOfProjectiles, float angleBetweenProjectiles, float angleVariation) : base(totalCycles, cycleTimer)
    {
        _target = target;
        this.projectileDefinition = projectileDefinition;
        this.betweenShotsTimer = betweenShotsTimer;
        this.numberOfProjectiles = numberOfProjectiles;
        this.angleBetweenProjectiles = angleBetweenProjectiles;
        this.angleVariation = angleVariation;
    }

    public override void UpdateShoot(Vector2 position, bool isRewinding)
    {
        CycleTimer.UpdateTime(isRewinding);

        if (!CycleTimer.IsFinished(false) || TotalCycles != 0 && CurrentCycles == TotalCycles) return;

        betweenShotsTimer.UpdateTime(isRewinding);

        if (!betweenShotsTimer.IsFinished(false)) return;

        betweenShotsTimer.Reset();

        if (_shotsFired == 0)
        {
            Vector2 direction = (Vector2)_target.transform.position - position;
            _currentAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - angleBetweenProjectiles * Mathf.Floor(numberOfProjectiles / 2f);
        }

        NPCCreator.CreateProjectile(projectileDefinition, position, Quaternion.Euler(0f, 0f, _currentAngle + Random.Range(-angleVariation, angleVariation)));
        _currentAngle += angleBetweenProjectiles;
        ++_shotsFired;

        if (_shotsFired == numberOfProjectiles)
        {
            ++CurrentCycles;
            CycleTimer.Reset();
            _shotsFired = 0;
        }
    }

    public override ShootBehaviour Clone()
    {
        return new ShootSuccessiveHoming(_target, TotalCycles, CycleTimer.Clone(), projectileDefinition, betweenShotsTimer.Clone(), numberOfProjectiles, angleBetweenProjectiles, angleVariation)
        {
            CurrentCycles = CurrentCycles,
            _shotsFired = _shotsFired,
            _currentAngle = _currentAngle
        };
    }
}