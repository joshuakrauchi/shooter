using System.Collections.Generic;
using UnityEngine;

public class EnemyDefinition
{
    public GameObject Prefab { get; private set; }
    public List<ProjectileDefinition> ProjectileDefinitions { get; private set; }
    public ShootBehaviour ShootBehaviour { get; private set; }
    
    public EnemyDefinition(GameObject prefab, List<ProjectileDefinition> projectileDefinitions, ShootBehaviour shootBehaviour)
    {
        Prefab = prefab;
        ProjectileDefinitions = projectileDefinitions;
        ShootBehaviour = shootBehaviour;
    }
}