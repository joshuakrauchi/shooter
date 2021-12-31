using System;
using UnityEngine;

[Serializable]
public class ProjectileDefinition
{
    [SerializeField] private MovePair[] movePairs;

    public GameObject Prefab { get; private set; }

    public MovePair[] MovePairs
    {
        get => movePairs;
        private set => movePairs = value;
    }

    public ProjectileDefinition(GameObject prefab, MovePair[] movePairs)
    {
        Prefab = prefab;
        MovePairs = movePairs;
    }
}