using System;
using Unity.Entities;

[Serializable, GenerateAuthoringComponent]
public struct MinionComponent : IComponentData
{
    public float dropRate;
    
    
}