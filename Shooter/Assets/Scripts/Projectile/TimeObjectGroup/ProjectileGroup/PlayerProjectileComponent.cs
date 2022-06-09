using System;
using Unity.Entities;

[Serializable, GenerateAuthoringComponent]
public struct PlayerProjectileComponent : IComponentData
{
    public float damageMultiplier;
}