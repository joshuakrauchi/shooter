using UnityEngine;

public class MinionDefinition
{
    public GameObject Prefab { get; private set; }
    public ShootBehaviour ShootBehaviour { get; private set; }

    public MinionDefinition(GameObject prefab, ShootBehaviour shootBehaviour)
    {
        Prefab = prefab;
        ShootBehaviour = shootBehaviour;
    }
}