using System;
using UnityEngine;

[Serializable]
public class ProjectileDefinition
{
    [field: SerializeField] public MovePair[] MovePairs { get; private set; }

    public GameObject Prefab { get; private set; }

    public ProjectileDefinition(GameObject prefab, MovePair[] movePairs)
    {
        Prefab = prefab;
        MovePairs = movePairs;
    }
}