using System;
using Unity.Entities;

[Serializable, GenerateAuthoringComponent]
public struct PlayerControllerComponent : IComponentData
{
    public bool isShootHeld;
    public bool isRewindHeld;
    public bool isConfirmDown;
    public bool isSpecialHeld;
    public float scrollDelta;
}