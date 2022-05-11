using System;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class ShootHoming : ShootBehaviour
{
    [SerializeReference] private ProjectileDefinition projectileDefinition;
    [SerializeField] private Timer shotTimer;
    [SerializeField] private uint shotsPerCycle;
    [SerializeField] private uint projectilesPerCycle;
    [SerializeField] private float angleBetweenProjectiles;
    [SerializeField] private float angleVariation;
    [SerializeField] private float speedChangeBetweenShots;

    private GameObject _target;
    private uint _currentShots;

    public ShootHoming(GameObject target, uint totalCycles, Timer cycleTimer, ProjectileDefinition projectileDefinition, Timer shotTimer, uint shotsPerCycle, uint projectilesPerCycle, float angleBetweenProjectiles, float angleVariation, float speedChangeBetweenShots) : base(totalCycles, cycleTimer)
    {
        _target = target;
        this.projectileDefinition = projectileDefinition;
        this.shotTimer = shotTimer;
        this.shotsPerCycle = shotsPerCycle;
        this.projectilesPerCycle = projectilesPerCycle;
        this.angleBetweenProjectiles = angleBetweenProjectiles;
        this.angleVariation = angleVariation;
        this.speedChangeBetweenShots = speedChangeBetweenShots;
    }

    public override void UpdateShoot(Vector2 position, bool isRewinding)
    {
        CycleTimer.UpdateTime(isRewinding);

        if (!CycleTimer.IsFinished(false) || TotalCycles != 0 && CurrentCycles == TotalCycles) return;

        shotTimer.UpdateTime(isRewinding);

        if (!shotTimer.IsFinished(false)) return;

        shotTimer.Reset();

        Vector2 direction = (Vector2)_target.transform.position - position;
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        angle -= angleBetweenProjectiles * Mathf.Floor(projectilesPerCycle / 2f) + Random.Range(-angleVariation, angleVariation);

        for (var i = 0; i < projectilesPerCycle; ++i)
        {
            ProjectileMovement projectileMovement = NPCCreator.CreateProjectile(projectileDefinition, position, Quaternion.Euler(0f, 0f, angle));
            projectileMovement.Speed += speedChangeBetweenShots * _currentShots;
            angle += angleBetweenProjectiles;
        }

        ++_currentShots;

        if (_currentShots == shotsPerCycle)
        {
            _currentShots = 0;
            ++CurrentCycles;
            CycleTimer.Reset();
        }
    }

    public override ShootBehaviour Clone()
    {
        return new ShootHoming(_target, TotalCycles, CycleTimer.Clone(), projectileDefinition, shotTimer.Clone(), shotsPerCycle, projectilesPerCycle, angleBetweenProjectiles, angleVariation, speedChangeBetweenShots)
        {
            CurrentCycles = CurrentCycles,
            _currentShots = _currentShots
        };
    }
}