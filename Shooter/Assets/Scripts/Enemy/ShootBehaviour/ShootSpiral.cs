using System;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class ShootSpiral : ShootBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private uint directions;
    [SerializeField] private float angleIncreasePerCycle;
    [SerializeField] private float angleVariation;

    private float _currentAngle;

    public ShootSpiral(uint totalCycles, Timer cycleTimer, GameObject projectilePrefab, uint directions, float angleIncreasePerCycle, float angleVariation) : base(totalCycles, cycleTimer)
    {
        this.projectilePrefab = projectilePrefab;
        this.directions = directions;
        this.angleIncreasePerCycle = angleIncreasePerCycle;
        this.angleVariation = angleVariation;
    }

    public override void UpdateShoot(Vector2 position, bool isRewinding)
    {
        CycleTimer.UpdateTime(isRewinding);

        if (!CycleTimer.IsFinished(false) || TotalCycles != 0 && CurrentCycles == TotalCycles) return;

        var angleBetweenProjectiles = 360.0f / directions;
        for (var i = 0; i < directions; ++i)
        {
            NPCCreator.CreateProjectile(projectilePrefab, position, Quaternion.Euler(0.0f, 0.0f, _currentAngle + angleBetweenProjectiles * i + Random.Range(-angleVariation, angleVariation)));
        }

        _currentAngle += angleIncreasePerCycle;
        CycleTimer.Reset();
        ++CurrentCycles;
    }

    public override ShootBehaviour Clone()
    {
        return new ShootSpiral(TotalCycles, CycleTimer.DeepCopy(), projectilePrefab, directions, angleIncreasePerCycle, angleVariation)
        {
            CurrentCycles = CurrentCycles,
            _currentAngle = _currentAngle
        };
    }
}