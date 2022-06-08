using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

[UpdateInGroup(typeof(ProjectileGroup)), UpdateAfter(typeof(ProjectileSystem))]
public partial class ProjectileMovementSystem : SystemBase
{
    protected override void OnUpdate()
    {
        var deltaTime = Time.DeltaTime;
        var isRewinding = GameManager.isRewinding;
        
        Entities.ForEach((ref StraightMovementComponent straightMovementComponent, ref Translation translation, ref Rotation rotation, ref ProjectileComponent projectileComponent) => {
            UpdateMovement(deltaTime, isRewinding, ref translation, ref rotation, ref projectileComponent);
        }).Schedule();
    }
    
    private static void UpdateMovement(float deltaTime, bool isRewinding, ref Translation translation, ref Rotation rotation, ref ProjectileComponent projectileComponent)
    {
        projectileComponent.timeAlive += isRewinding ? -deltaTime : deltaTime;

        if (projectileComponent.timeAlive < 0)
        {
            //DestroyProjectile();
        }

        if (isRewinding) return;
        
        float3 straightMovement = GetStraightMovement(ref projectileComponent) * deltaTime;
        straightMovement.y = 0.4f * math.sin(projectileComponent.timeAlive * 1.4f);
        
        translation.Value += math.mul(rotation.Value, straightMovement);
    }

    private static float3 GetStraightMovement(ref ProjectileComponent projectileComponent)
    {
        return new float3(projectileComponent.speed, 0.0f, 0.0f);
    }
}