using UnityEngine;

public class ShootSpinningFlower : ShootBehaviour
{
    [SerializeField] private ProjectileDefinition projectileDefinition;
    [SerializeField] private uint petals;
    [SerializeField] private uint projectilesPerPetal;
    [SerializeField] private float angleBetweenProjectiles;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float petalOffset;

    public ShootSpinningFlower(uint totalCycles, Timer cycleTimer, ProjectileDefinition projectileDefinition, uint petals, uint projectilesPerPetal, float angleBetweenProjectiles, float projectileSpeed, float petalOffset) : base(totalCycles, cycleTimer)
    {
        this.projectileDefinition = projectileDefinition;
        this.petals = petals;
        this.projectilesPerPetal = projectilesPerPetal;
        this.angleBetweenProjectiles = angleBetweenProjectiles;
        this.projectileSpeed = projectileSpeed;
        this.petalOffset = petalOffset;
    }

    public override void UpdateShoot(Vector2 position)
    {
        CycleTimer.UpdateTime();

        if (!CycleTimer.IsFinished(false) || TotalCycles != 0 && CurrentCycles == TotalCycles) return;

        var angleBetweenPetals = 360f / petals;
        for (var i = 0; i < petals; ++i)
        {
            for (var j = 0; j < projectilesPerPetal; ++j)
            {
                var projectileMovement1 = NPCCreator.CreateProjectile(projectileDefinition, position, Quaternion.Euler(0f, 0f, angleBetweenPetals * i + angleBetweenProjectiles * j + petalOffset));
                projectileMovement1.Speed = projectileSpeed - projectileSpeed * j / projectilesPerPetal;

                var projectileMovement2 = NPCCreator.CreateProjectile(projectileDefinition, position, Quaternion.Euler(0f, 0f, angleBetweenPetals * i + -angleBetweenProjectiles * j + petalOffset));
                projectileMovement2.Speed = projectileSpeed - projectileSpeed * j / projectilesPerPetal;
            }
        }

        CycleTimer.Reset();
        ++CurrentCycles;
    }

    public override ShootBehaviour Clone()
    {
        return new ShootSpinningFlower(TotalCycles, CycleTimer.Clone(), projectileDefinition, petals, projectilesPerPetal, angleBetweenProjectiles, projectileSpeed, petalOffset)
        {
            CurrentCycles = CurrentCycles
        };
    }
}