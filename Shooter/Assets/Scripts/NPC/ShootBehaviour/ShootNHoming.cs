using System.Collections.Generic;
using UnityEngine;

public class ShootNHoming : ShootBehaviour
{
    public uint N { get; private set; }

    public ShootNHoming(uint n)
    {
        N = n;
    }
    
    public override void UpdateShoot(IReadOnlyList<ProjectileDefinition> projectileDefinitions, Vector2 position)
    {
        const float offset = 0.2f;
        
        var direction = (Vector2)GameManager.Player.transform.position - position;
        var angle = Mathf.Atan2(direction.y, direction.x);

        angle -= offset * Mathf.Floor(N / 2f) + Random.Range(-0.1f, 0.1f);
        
        for (var i = 0; i < N; ++i)
        {
            NPCCreator.CreateProjectile(projectileDefinitions[0], position, Quaternion.Euler(0f, 0f, angle * Mathf.Rad2Deg));
            angle += offset;
        }
    }
}