using System;
using Unity.Entities;

[Serializable, GenerateAuthoringComponent]
public struct EntitySpawnComponent : IComponentData
{
    public Entity Entity;
}