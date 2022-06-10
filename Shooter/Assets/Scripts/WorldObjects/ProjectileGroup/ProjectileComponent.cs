using System;
using Unity.Entities;
using UnityEngine;

[Serializable, GenerateAuthoringComponent]
public struct ProjectileComponent : IComponentData
{
    public float speed;

    [HideInInspector] public float timeAlive;
}