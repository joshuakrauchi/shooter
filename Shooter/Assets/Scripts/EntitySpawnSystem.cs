using Unity.Entities;
using ParallelWriter = Unity.Entities.EntityCommandBuffer.ParallelWriter;

public partial class EntitySpawnSystem : SystemBase
{
    private BeginInitializationEntityCommandBufferSystem _beginInitializationEntityCommandBufferSystem;

    protected override void OnCreate()
    {
        base.OnCreate();

        _beginInitializationEntityCommandBufferSystem = World.GetOrCreateSystem<BeginInitializationEntityCommandBufferSystem>();
    }

    protected override void OnUpdate()
    {
        ParallelWriter parallelWriter = _beginInitializationEntityCommandBufferSystem.CreateCommandBuffer().AsParallelWriter();
        
        Entities.ForEach((int entityInQueryIndex, DynamicBuffer<EntitySpawnBufferElement> entitySpawnBufferElements) =>
        {
            foreach (EntitySpawnBufferElement entitySpawnBufferElement in entitySpawnBufferElements)
            {
                Entity spawnedEntity = parallelWriter.Instantiate(entityInQueryIndex, entitySpawnBufferElement.Entity);
                parallelWriter.SetComponent(entityInQueryIndex, spawnedEntity, entitySpawnBufferElement.Translation);
                parallelWriter.SetComponent(entityInQueryIndex, spawnedEntity, entitySpawnBufferElement.Rotation);
            }

            entitySpawnBufferElements.Clear();
        }).Schedule();
        
        _beginInitializationEntityCommandBufferSystem.AddJobHandleForProducer(Dependency);
    }
}