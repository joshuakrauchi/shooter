using System;
using Unity.Entities;

[Serializable, GenerateAuthoringComponent]
public struct VariableSpeedStraightMovementComponent : IComponentData
{
    public float speed;
    public float speedOverTimeMultiplier;
    public float minimumSpeed;
}