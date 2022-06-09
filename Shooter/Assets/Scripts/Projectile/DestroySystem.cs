using Unity.Entities;
using ParallelWriter = Unity.Entities.EntityCommandBuffer.ParallelWriter;

public partial class DestroySystem : SystemBase
{
    private BeginFixedStepSimulationEntityCommandBufferSystem _beginFixedStepSimulationEntityCommandBufferSystem;
    
    protected override void OnCreate()
    {
        base.OnCreate();
        
        _beginFixedStepSimulationEntityCommandBufferSystem = World.GetOrCreateSystem<BeginFixedStepSimulationEntityCommandBufferSystem>();
    }

    protected override void OnUpdate()
    {
        ParallelWriter parallelWriter = _beginFixedStepSimulationEntityCommandBufferSystem.CreateCommandBuffer().AsParallelWriter();
        
        Entities.WithAll<DestroyTag>().ForEach((int entityInQueryIndex, ref Entity entity) => {
            parallelWriter.DestroyEntity(entityInQueryIndex, entity);
        }).ScheduleParallel();
        
        _beginFixedStepSimulationEntityCommandBufferSystem.AddJobHandleForProducer(Dependency);
    }
}