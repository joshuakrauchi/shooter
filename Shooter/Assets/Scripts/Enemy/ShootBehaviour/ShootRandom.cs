using System;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class ShootRandom : ShootBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private uint projectilesPerCycle;

    public ShootRandom(uint totalCycles, Timer cycleTimer, GameObject projectilePrefab, uint projectilesPerCycle) : base(totalCycles, cycleTimer)
    {
        this.projectilePrefab = projectilePrefab;
        this.projectilesPerCycle = projectilesPerCycle;
    }

    public override void UpdateShoot(Vector2 position, bool isRewinding)
    {
        CycleTimer.UpdateTime(isRewinding);

        if (!CycleTimer.IsFinished(false) || TotalCycles != 0 && CurrentCycles == TotalCycles) return;

        for (var i = 0; i < projectilesPerCycle; ++i)
        {
            NPCCreator.CreateProjectile(projectilePrefab, position, Quaternion.Euler(0.0f, 0.0f, Random.Range(-180.0f, 180.0f)));
        }

        CycleTimer.Reset();
        ++CurrentCycles;
    }

    public override ShootBehaviour Clone()
    {
        return new ShootRandom(TotalCycles, CycleTimer, projectilePrefab, projectilesPerCycle)
        {
            CurrentCycles = CurrentCycles
        };
    }
}