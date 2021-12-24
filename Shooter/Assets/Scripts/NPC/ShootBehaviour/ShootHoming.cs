using UnityEngine;

public class ShootHoming : ShootBehaviour
{
    public ProjectileDefinition ProjectileDefinition { get; private set; }
    public uint NumberOfProjectiles { get; private set; }
    public float ProjectileOffset { get; private set; }
    public float AngleVariation { get; private set; }

    public ShootHoming(uint totalCycles, float timeBetweenCycles, ProjectileDefinition projectileDefinition, uint numberOfProjectiles, float projectileOffset, float angleVariation) : base(totalCycles, timeBetweenCycles)
    {
        ProjectileDefinition = projectileDefinition;
        NumberOfProjectiles = numberOfProjectiles;
        ProjectileOffset = projectileOffset;
        AngleVariation = angleVariation;
    }

    public override void UpdateShoot(Vector2 position)
    {
        if (CurrentTime < TimeBetweenCycles)
        {
            CurrentTime += Time.deltaTime;
            return;
        }

        if (TotalCycles != 0 && CurrentCycles == TotalCycles) return;

        var direction = (Vector2)GameManager.Player.transform.position - position;
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        var variation = AngleVariation / 2f;
        angle -= ProjectileOffset * Mathf.Floor(NumberOfProjectiles / 2f) + Random.Range(-variation, variation);

        for (var i = 0; i < NumberOfProjectiles; ++i)
        {
            NPCCreator.CreateProjectile(ProjectileDefinition, position, Quaternion.Euler(0f, 0f, angle));
            angle += ProjectileOffset;
        }

        ++CurrentCycles;
        CurrentTime -= TimeBetweenCycles;
    }

    public override object Clone()
    {
        return new ShootHoming(TotalCycles, TimeBetweenCycles, ProjectileDefinition, NumberOfProjectiles, ProjectileOffset, AngleVariation)
        {
            CurrentCycles = CurrentCycles,
            CurrentTime = CurrentTime
        };
    }
}