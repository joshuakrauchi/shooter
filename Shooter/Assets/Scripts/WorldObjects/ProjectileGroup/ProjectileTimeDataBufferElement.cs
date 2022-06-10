using System;
using Unity.Entities;
using Unity.Mathematics;

[Serializable]
public struct ProjectileTimeDataBufferElement : IBufferElementData
{
    public bool isDisabled;
    public float3 translation;
    public quaternion rotation;
}