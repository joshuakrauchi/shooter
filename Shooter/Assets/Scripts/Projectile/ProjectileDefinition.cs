using System;
using UnityEngine;

[Serializable]
public class ProjectileDefinition
{
    public GameObject Prefab { get; private set; }
    [SerializeReference] public MoveBehaviour MoveBehaviour;

    public ProjectileDefinition(GameObject prefab, MoveBehaviour moveBehaviour)
    {
        Prefab = prefab;
        MoveBehaviour = moveBehaviour;
    }
}