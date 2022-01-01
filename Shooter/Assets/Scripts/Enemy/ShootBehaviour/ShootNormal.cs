using UnityEngine;

public class ShootNormal : ShootBehaviour
{
    [SerializeField] private ProjectileDefinition projectileDefinition;
    [SerializeField] private uint numberOfProjectiles;
    [SerializeField] private float initialDirection;
    [SerializeField] private float angleBetweenProjectiles;
    [SerializeField] private float angleVariation;

    public ShootNormal(uint totalCycles, Timer cycleTimer, ProjectileDefinition projectileDefinition, uint numberOfProjectiles, float initialDirection, float angleBetweenProjectiles, float angleVariation) : base(totalCycles, cycleTimer)
    {
        this.projectileDefinition = projectileDefinition;
        this.numberOfProjectiles = numberOfProjectiles;
        this.initialDirection = initialDirection;
        this.angleBetweenProjectiles = angleBetweenProjectiles;
        this.angleVariation = angleVariation;
    }

    public override void UpdateShoot(Vector2 position)
    {
        CycleTimer.UpdateTime();

        if (!CycleTimer.IsFinished(false) || TotalCycles != 0 && CurrentCycles == TotalCycles) return;

        var currentAngle = initialDirection - angleBetweenProjectiles * Mathf.Floor(numberOfProjectiles / 2f) + Random.Range(-angleVariation, angleVariation);

        for (var i = 0; i < numberOfProjectiles; ++i)
        {
            NPCCreator.CreateProjectile(projectileDefinition, position, Quaternion.Euler(0f, 0f, currentAngle));
            currentAngle += angleBetweenProjectiles;
        }

        ++CurrentCycles;
        CycleTimer.Reset();
    }

    public override ShootBehaviour Clone()
    {
        return new ShootNormal(TotalCycles, CycleTimer.Clone(), projectileDefinition, numberOfProjectiles, initialDirection, angleBetweenProjectiles, angleVariation)
        {
            CurrentCycles = CurrentCycles
        };
    }
}