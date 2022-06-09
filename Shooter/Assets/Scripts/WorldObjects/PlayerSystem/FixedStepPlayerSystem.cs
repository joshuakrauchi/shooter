using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

[UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
public partial class FixedStepPlayerSystem : SystemBase
{
    private static Timer ShootTimer { get; set; }

    protected override void OnCreate()
    {
        base.OnCreate();

        ShootTimer = new Timer(GameInfo.Instance.ShootDelay);
    }

    protected override void OnUpdate()
    {
        var isRewinding = GameManager.isRewinding;
        var numberOfShots = GameInfo.Instance.NumberOfShots;
        
        ShootTimer.UpdateTime(isRewinding);
        var isShootTimerFinished = ShootTimer.IsFinished(false);

        Entities.WithAll<PlayerComponent>().ForEach((ref PlayerControllerComponent playerControllerComponent, ref Translation translation, ref EntitySpawnComponent entitySpawnComponent, ref DynamicBuffer<EntitySpawnBufferElement> entitySpawnBufferElements) =>
        {
            UpdateShoot(isShootTimerFinished, numberOfShots, ref translation, ref playerControllerComponent, ref entitySpawnComponent, ref entitySpawnBufferElements);
        }).Run();

        if (isShootTimerFinished)
        {
            ShootTimer.Reset();
        }
    }
    
    private static void UpdateShoot(bool isShootTimerFinished, float numberOfShots, ref Translation translation, ref PlayerControllerComponent playerControllerComponent, ref EntitySpawnComponent entitySpawnComponent, ref DynamicBuffer<EntitySpawnBufferElement> entitySpawnBufferElements)
    {
        if (!isShootTimerFinished || !playerControllerComponent.isShootHeld) return;
        
        for (var i = 0; i < numberOfShots; ++i)
        {
            entitySpawnBufferElements.Add(new EntitySpawnBufferElement
            {
                Entity = entitySpawnComponent.Entity,
                Translation = translation,
                Rotation = new Rotation
                {
                    Value = quaternion.Euler(0.0f, 0.0f, math.PI / 2.0f)
                }
            });
        }
    }
}