using Unity.Entities;
using Unity.Transforms;

public struct EntitySpawnBufferElement : IBufferElementData
{
    public Entity Entity;
    public Translation Translation;
    public Rotation Rotation;
}