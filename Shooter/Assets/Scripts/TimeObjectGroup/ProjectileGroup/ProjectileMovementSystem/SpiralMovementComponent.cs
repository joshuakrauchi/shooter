using System;
using Unity.Entities;

[Serializable, GenerateAuthoringComponent]
public struct SpiralMovementComponent : IComponentData
{
    public float speed;
    public float initialAmplitude;
    public float amplitudeIncreaseRate;
}