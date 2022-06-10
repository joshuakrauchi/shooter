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

        Entities.ForEach((ref Translation translation, ref Rotation rotation, in TimeObjectComponent timeObjectComponent, in StraightMovementComponent straightMovementComponent,
            in ProjectileComponent projectileComponent) =>
        {
            if (timeObjectComponent.isDisabled) return;

            UpdateStraightMovement(deltaTime, ref translation, ref rotation, straightMovementComponent, projectileComponent);
        }).Schedule();

        Entities.ForEach((ref Translation translation, ref Rotation rotation, in TimeObjectComponent timeObjectComponent, in SineMovementComponent sineMovementComponent,
            in ProjectileComponent projectileComponent) =>
        {
            if (timeObjectComponent.isDisabled) return;

            UpdateSineMovement(deltaTime, ref translation, ref rotation, sineMovementComponent, projectileComponent);
        }).Schedule();

        Entities.ForEach((ref Translation translation, ref Rotation rotation, in TimeObjectComponent timeObjectComponent,
            in VariableSpeedStraightMovementComponent variableSpeedStraightMovementComponent, in ProjectileComponent projectileComponent) =>
        {
            if (timeObjectComponent.isDisabled) return;

            UpdateVariableSpeedStraightMovement(deltaTime, ref translation, ref rotation, variableSpeedStraightMovementComponent, projectileComponent);
        }).Schedule();

        Entities.ForEach((ref Translation translation, ref Rotation rotation, ref TimeObjectComponent timeObjectComponent,
            ref SpiralMovementComponent spiralMovementComponent, in ProjectileComponent projectileComponent) =>
        {
            if (timeObjectComponent.isDisabled) return;

            UpdateSpiralMovement(deltaTime, ref translation, ref rotation, ref spiralMovementComponent, projectileComponent);
        }).Schedule();
    }

    private static void UpdateStraightMovement(float deltaTime, ref Translation translation, ref Rotation rotation, in StraightMovementComponent straightMovementComponent, in ProjectileComponent projectileComponent)
    {
        float3 straightMovement = new float3(projectileComponent.speed * deltaTime, 0.0f, 0.0f);

        translation.Value += math.mul(rotation.Value, straightMovement);
    }

    private static void UpdateSineMovement(float deltaTime, ref Translation translation, ref Rotation rotation, in SineMovementComponent sineMovementComponent,
        in ProjectileComponent projectileComponent)
    {
        float3 sineMovement = new float3(projectileComponent.speed * deltaTime,
            sineMovementComponent.amplitude * math.sin(projectileComponent.timeAlive * sineMovementComponent.period + sineMovementComponent.phaseShift), 0.0f);

        translation.Value += math.mul(rotation.Value, sineMovement);
    }

    private static void UpdateVariableSpeedStraightMovement(float deltaTime, ref Translation translation, ref Rotation rotation,
        in VariableSpeedStraightMovementComponent variableSpeedStraightMovementComponent, in ProjectileComponent projectileComponent)
    {
        var speed = projectileComponent.speed;
        var minimumSpeed = variableSpeedStraightMovementComponent.minimumSpeed;

        var variableSpeed = projectileComponent.timeAlive * variableSpeedStraightMovementComponent.speedOverTimeMultiplier + speed;

        if (speed > 0.0f && variableSpeed < minimumSpeed ||
            speed < 0.0f && variableSpeed > -minimumSpeed)
        {
            variableSpeed = minimumSpeed * math.sign(speed);
        }

        float3 variableSpeedStraightMovement = new float3(variableSpeed * deltaTime, 0.0f, 0.0f);

        translation.Value += math.mul(rotation.Value, variableSpeedStraightMovement);
    }

    private static void UpdateSpiralMovement(float deltaTime, ref Translation translation, ref Rotation rotation, ref SpiralMovementComponent spiralMovementComponent,
        in ProjectileComponent projectileComponent)
    {
        var speed = projectileComponent.speed;
        var timeAlive = projectileComponent.timeAlive;
        var movement = timeAlive * speed;
        spiralMovementComponent.initialAmplitude += deltaTime * spiralMovementComponent.amplitudeIncreaseRate;

        float3 spiralMovement = new float3(math.sin(movement), math.cos(movement), 0.0f) * spiralMovementComponent.initialAmplitude;
        //translation.Value += math.mul(rotation.Value, spiralMovement);
    }
}