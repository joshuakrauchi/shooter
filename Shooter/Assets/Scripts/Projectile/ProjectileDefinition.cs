using System;
using UnityEngine;

[Serializable]
public class ProjectileDefinition
{
    [field: SerializeField] public MoveBehaviour[] MoveBehaviours { get; private set; }

    public GameObject Prefab { get; private set; }

    public ProjectileDefinition(GameObject prefab, MoveBehaviour[] moveBehaviours)
    {
        Prefab = prefab;
        MoveBehaviours = moveBehaviours;
    }
}