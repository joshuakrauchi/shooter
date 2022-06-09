using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

[UpdateInGroup(typeof(ProjectileGroup)), UpdateAfter(typeof(ProjectileSystem))]
public partial class ProjectileMovementSystem : SystemBase
{
    protected override void OnUpdate()
    {
        var isRewinding = GameManager.isRewinding;

        if (isRewinding) return;

        var deltaTime = Time.DeltaTime;

        Entities.ForEach((ref Translation translation, ref Rotation rotation, in TimeObjectComponent timeObjectComponent, in StraightMovementComponent straightMovementComponent) =>
        {
            if (timeObjectComponent.isDisabled) return;
            
            UpdateStraightMovement(deltaTime, ref translation, ref rotation, straightMovementComponent);
        }).Schedule();

        Entities.ForEach((ref Translation translation, ref Rotation rotation, in TimeObjectComponent timeObjectComponent, in SineMovementComponent sineMovementComponent, in ProjectileComponent projectileComponent) =>
        {
            if (timeObjectComponent.isDisabled) return;

            UpdateSineMovement(deltaTime, ref translation, ref rotation, sineMovementComponent, projectileComponent);
        }).Schedule();
    }

    private static void UpdateStraightMovement(float deltaTime, ref Translation translation, ref Rotation rotation, in StraightMovementComponent straightMovementComponent)
    {
        float3 straightMovement = new float3(straightMovementComponent.speed, 0.0f, 0.0f);

        translation.Value += math.mul(rotation.Value, straightMovement * deltaTime);
    }

    private static void UpdateSineMovement(float deltaTime, ref Translation translation, ref Rotation rotation, in SineMovementComponent sineMovementComponent,
        in ProjectileComponent projectileComponent)
    {
        float3 sineMovement = new float3(sineMovementComponent.speed * deltaTime,
            sineMovementComponent.amplitude * math.sin(projectileComponent.timeAlive * sineMovementComponent.period + sineMovementComponent.phaseShift), 0.0f);

        translation.Value += math.mul(rotation.Value, sineMovement);
    }
}