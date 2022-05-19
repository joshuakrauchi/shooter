using UnityEngine;
using Random = UnityEngine.Random;

public class ShootRandom : ShootBehaviour
{
    [SerializeField] private uint projectilesPerCycle;

    protected override bool UpdateCycle(bool isRewinding)
    {
        for (var i = 0; i < projectilesPerCycle; ++i)
        {
            NPCCreator.CreateProjectile(ProjectilePrefab, transform.position, Quaternion.Euler(0.0f, 0.0f, Random.Range(-180.0f, 180.0f)));
        }
        
        return true;
    }
}