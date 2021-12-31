using UnityEngine;

public class EnemyDefinition
{
    public GameObject Prefab { get; private set; }
    public ShootBehaviour ShootBehaviour { get; private set; }

    public EnemyDefinition(GameObject prefab, ShootBehaviour shootBehaviour)
    {
        Prefab = prefab;
        ShootBehaviour = shootBehaviour;
    }
}