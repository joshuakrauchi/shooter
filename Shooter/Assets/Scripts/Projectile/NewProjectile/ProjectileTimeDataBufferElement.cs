using System;
using Unity.Entities;
using Unity.Mathematics;

[Serializable, GenerateAuthoringComponent]
public struct ProjectileTimeDataBufferElement : IBufferElementData
{
    public bool isDisabled;
    public float3 translation;
    public quaternion rotation;
}