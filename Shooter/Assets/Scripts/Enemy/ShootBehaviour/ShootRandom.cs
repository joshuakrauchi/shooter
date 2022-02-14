using System;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class ShootRandom : ShootBehaviour
{
    [SerializeField] private ProjectileDefinition projectileDefinition;
    [SerializeField] private uint projectilesPerCycle;

    public ShootRandom(uint totalCycles, Timer cycleTimer, ProjectileDefinition projectileDefinition, uint projectilesPerCycle) : base(totalCycles, cycleTimer)
    {
        this.projectileDefinition = projectileDefinition;
        this.projectilesPerCycle = projectilesPerCycle;
    }

    public override void UpdateShoot(Vector2 position, bool isRewinding)
    {
        CycleTimer.UpdateTime(isRewinding);

        if (!CycleTimer.IsFinished(false) || TotalCycles != 0 && CurrentCycles == TotalCycles) return;

        for (var i = 0; i < projectilesPerCycle; ++i)
        {
            NPCCreator.CreateProjectile(projectileDefinition, position, Quaternion.Euler(0f, 0f, Random.Range(-180f, 180f)));
        }

        CycleTimer.Reset();
        ++CurrentCycles;
    }

    public override ShootBehaviour Clone()
    {
        return new ShootRandom(TotalCycles, CycleTimer, projectileDefinition, projectilesPerCycle)
        {
            CurrentCycles = CurrentCycles
        };
    }
}