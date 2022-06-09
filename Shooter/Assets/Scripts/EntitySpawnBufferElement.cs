using Unity.Entities;
using Unity.Transforms;

[GenerateAuthoringComponent]
public struct EntitySpawnBufferElement : IBufferElementData
{
    public Entity Entity;
    public Translation Translation;
    public Rotation Rotation;
}