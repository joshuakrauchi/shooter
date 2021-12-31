using UnityEngine;

public class ShootHoming : ShootBehaviour
{
    [SerializeField] private ProjectileDefinition projectileDefinition;
    [SerializeField] private uint numberOfProjectiles;
    [SerializeField] private float angleBetweenProjectiles;
    [SerializeField] private float angleVariation;

    public ShootHoming(uint totalCycles, Timer cycleTimer, ProjectileDefinition projectileDefinition, uint numberOfProjectiles, float angleBetweenProjectiles, float angleVariation) : base(totalCycles, cycleTimer)
    {
        this.projectileDefinition = projectileDefinition;
        this.numberOfProjectiles = numberOfProjectiles;
        this.angleBetweenProjectiles = angleBetweenProjectiles;
        this.angleVariation = angleVariation;
    }

    public override void UpdateShoot(Vector2 position)
    {
        CycleTimer.UpdateTime();

        if (!CycleTimer.IsFinished(false) || TotalCycles != 0 && CurrentCycles == TotalCycles) return;

        var direction = (Vector2)GameManager.Player.transform.position - position;
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        angle -= angleBetweenProjectiles * Mathf.Floor(numberOfProjectiles / 2f) + Random.Range(-angleVariation, angleVariation);

        for (var i = 0; i < numberOfProjectiles; ++i)
        {
            NPCCreator.CreateProjectile(projectileDefinition, position, Quaternion.Euler(0f, 0f, angle));
            angle += angleBetweenProjectiles;
        }

        ++CurrentCycles;
        CycleTimer.Reset();
    }

    public override ShootBehaviour Clone()
    {
        return new ShootHoming(TotalCycles, CycleTimer.Clone(), projectileDefinition, numberOfProjectiles, angleBetweenProjectiles, angleVariation)
        {
            CurrentCycles = CurrentCycles
        };
    }
}