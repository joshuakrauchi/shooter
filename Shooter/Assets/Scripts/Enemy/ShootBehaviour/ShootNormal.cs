using System;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class ShootNormal : ShootBehaviour
{
    [SerializeReference] private GameObject projectilePrefab;
    [SerializeField] private uint numberOfProjectiles;
    [SerializeField] private float initialDirection;
    [SerializeField] private float angleBetweenProjectiles;
    [SerializeField] private float angleVariation;

    public ShootNormal(uint totalCycles, Timer cycleTimer, GameObject projectilePrefab, uint numberOfProjectiles, float initialDirection, float angleBetweenProjectiles, float angleVariation) : base(totalCycles, cycleTimer)
    {
        this.projectilePrefab = projectilePrefab;
        this.numberOfProjectiles = numberOfProjectiles;
        this.initialDirection = initialDirection;
        this.angleBetweenProjectiles = angleBetweenProjectiles;
        this.angleVariation = angleVariation;
    }

    public override void UpdateShoot(Vector2 position, bool isRewinding)
    {
        CycleTimer.UpdateTime(isRewinding);

        if (!CycleTimer.IsFinished(false) || TotalCycles != 0 && CurrentCycles == TotalCycles) return;

        var currentAngle = initialDirection - angleBetweenProjectiles * Mathf.Floor(numberOfProjectiles / 2.0f) + Random.Range(-angleVariation, angleVariation);

        for (var i = 0; i < numberOfProjectiles; ++i)
        {
            NPCCreator.CreateProjectile(projectilePrefab, position, Quaternion.Euler(0.0f, 0.0f, currentAngle));
            currentAngle += angleBetweenProjectiles;
        }

        ++CurrentCycles;
        CycleTimer.Reset();
    }

    public override ShootBehaviour Clone()
    {
        return new ShootNormal(TotalCycles, CycleTimer.DeepCopy(), projectilePrefab, numberOfProjectiles, initialDirection, angleBetweenProjectiles, angleVariation)
        {
            CurrentCycles = CurrentCycles
        };
    }
}