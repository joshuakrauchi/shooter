using UnityEngine;

public class ShootSpinningFlower : ShootBehaviour
{
    private bool _isFinished;
    private ProjectileDefinition _projectile;

    public ShootSpinningFlower(uint totalCycles, Timer cycleTimer, ProjectileDefinition projectileDefinition) : base(totalCycles, cycleTimer)
    {
        _projectile = projectileDefinition;
    }

    public override void UpdateShoot(Vector2 position)
    {
        if (!_isFinished)
        {
            for (var j = 0; j < 8; ++j)
            {
                var petals = 30;
                var speed = 0.5f;
                for (var i = 0; i < petals; ++i)
                {
                    var projectileMovement1 = NPCCreator.CreateProjectile(_projectile, position, Quaternion.Euler(0f, 0f, 2f * i + 45 * j));
                    projectileMovement1.XSpeed = speed - i / (float)petals * speed;

                    var projectileMovement2 = NPCCreator.CreateProjectile(_projectile, position, Quaternion.Euler(0f, 0f, -2f * i + 45 * j));
                    projectileMovement2.XSpeed = speed - i / (float)petals * speed;
                }
            }

            _isFinished = true;
        }
    }

    public override object Clone()
    {
        return new ShootSpinningFlower(TotalCycles, CycleTimer, _projectile);
    }
}