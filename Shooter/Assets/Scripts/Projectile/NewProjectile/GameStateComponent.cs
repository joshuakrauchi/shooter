using System;
using Unity.Entities;

[Serializable, GenerateAuthoringComponent]
public struct GameStateComponent : IComponentData
{
    public bool isRewinding;
}