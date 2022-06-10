using System;
using Unity.Entities;

[Serializable, GenerateAuthoringComponent]
public struct VariableSpeedStraightMovementComponent : IComponentData
{
    public float speedOverTimeMultiplier;
    public float minimumSpeed;
}