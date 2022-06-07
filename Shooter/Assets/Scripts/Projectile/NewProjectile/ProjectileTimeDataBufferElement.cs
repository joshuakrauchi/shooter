using System;
using Unity.Entities;
using Unity.Mathematics;

[Serializable, GenerateAuthoringComponent]
public struct ProjectileTimeDataBufferElement : IBufferElementData
{
    public float2 position;
    public float4 rotation;
    public bool isDisabled;
}
