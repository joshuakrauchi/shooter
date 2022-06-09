using System;
using Unity.Entities;
using UnityEngine;

[Serializable, GenerateAuthoringComponent]
public struct ProjectileComponent : IComponentData
{
    [HideInInspector] public float timeAlive;
}