using System;
using UnityEngine;

[Serializable]
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

    public override void UpdateShoot(Vector2 position, bool isRewinding)
    {
        CycleTimer.UpdateTime(isRewinding);

        if (!CycleTimer.IsFinished(false) || TotalCycles != 0 && CurrentCycles == TotalCycles) return;

        var projectilesPerSide = projectilesPerPetal / 2;
        var angleBetweenPetals = 360f / petals;
        for (var i = 0; i < petals; ++i)
        {
            ProjectileMovement middleProjectile = NPCCreator.CreateProjectile(projectileDefinition, position, Quaternion.Euler(0f, 0f, angleBetweenPetals * i + petalOffset));
            middleProjectile.Speed = projectileSpeed;

            for (var j = 0; j < projectilesPerSide; ++j)
            {
                ProjectileMovement projectileMovement1 = NPCCreator.CreateProjectile(projectileDefinition, position, Quaternion.Euler(0f, 0f, angleBetweenPetals * i + angleBetweenProjectiles * (j + 1) + petalOffset));
                projectileMovement1.Speed = projectileSpeed - projectileSpeed * (j + 1) / projectilesPerSide;

                ProjectileMovement projectileMovement2 = NPCCreator.CreateProjectile(projectileDefinition, position, Quaternion.Euler(0f, 0f, angleBetweenPetals * i + -angleBetweenProjectiles * (j + 1) + petalOffset));
                projectileMovement2.Speed = projectileSpeed - projectileSpeed * (j + 1) / projectilesPerSide;
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