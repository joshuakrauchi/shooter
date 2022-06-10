using System;
using Unity.Entities;
using UnityEngine;

[Serializable]
public struct ProjectileComponent : IComponentData
{
    public float speed;

    [HideInInspector] public float timeAlive;
}