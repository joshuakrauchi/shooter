using System;
using Unity.Entities;

[Serializable, GenerateAuthoringComponent]
public struct SineMovementComponent : IComponentData
{
    public float amplitude;
    public float period;
    public float phaseShift;
}