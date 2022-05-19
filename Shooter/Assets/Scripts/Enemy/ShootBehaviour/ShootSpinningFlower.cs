using UnityEngine;

public class ShootSpinningFlower : ShootBehaviour
{
    [SerializeField] private uint petals;
    [SerializeField] private uint projectilesPerPetal;
    [SerializeField] private float angleBetweenProjectiles;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float petalOffset;

    protected override bool UpdateCycle(bool isRewinding)
    {
        var projectilesPerSide = projectilesPerPetal / 2;
        var angleBetweenPetals = 360.0f / petals;
        for (var i = 0; i < petals; ++i)
        {
            ProjectileMovement middleProjectile = NPCCreator.CreateProjectile(ProjectilePrefab, transform.position, Quaternion.Euler(0.0f, 0.0f, angleBetweenPetals * i + petalOffset));
            middleProjectile.Speed = projectileSpeed;

            for (var j = 0; j < projectilesPerSide; ++j)
            {
                ProjectileMovement projectileMovement1 = NPCCreator.CreateProjectile(ProjectilePrefab, transform.position, Quaternion.Euler(0.0f, 0.0f, angleBetweenPetals * i + angleBetweenProjectiles * (j + 1) + petalOffset));
                projectileMovement1.Speed = projectileSpeed - projectileSpeed * (j + 1) / projectilesPerSide;

                ProjectileMovement projectileMovement2 = NPCCreator.CreateProjectile(ProjectilePrefab, transform.position, Quaternion.Euler(0.0f, 0.0f, angleBetweenPetals * i + -angleBetweenProjectiles * (j + 1) + petalOffset));
                projectileMovement2.Speed = projectileSpeed - projectileSpeed * (j + 1) / projectilesPerSide;
            }
        }

        return true;
    }
}