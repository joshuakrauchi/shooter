using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Transforms;
using UnityEngine;

[UpdateInGroup(typeof(ProjectileGroup))]
public partial class ProjectileSystem : SystemBase
{
    private EndInitializationEntityCommandBufferSystem _endInitializationEntityCommandBufferSystem;

    protected override void OnCreate()
    {
        base.OnCreate();

        _endInitializationEntityCommandBufferSystem = World.GetOrCreateSystem<EndInitializationEntityCommandBufferSystem>();
    }

    protected override void OnUpdate()
    {
        EntityCommandBuffer entityCommandBuffer = _endInitializationEntityCommandBufferSystem.CreateCommandBuffer();

        var deltaTime = Time.DeltaTime;
        var isRewinding = GameManager.isRewinding;
        var maxTimeData = Utilities.MaxTimeData;
        float3 offscreenPosition = Utilities.OffscreenPosition;

        Entities.ForEach((ref Entity entity, ref Translation translation, ref Rotation rotation, ref TimeObjectComponent timeObjectComponent, ref ProjectileComponent projectileComponent,
            ref DynamicBuffer<ProjectileTimeDataBufferElement> timeData) =>
        {
            UpdateTimeData(isRewinding, maxTimeData, ref translation, ref rotation, ref timeObjectComponent, ref timeData);

            if (timeObjectComponent.isDisabled)
            {
                translation.Value = offscreenPosition;
            }

            projectileComponent.timeAlive += isRewinding ? -deltaTime : deltaTime;

            if (projectileComponent.timeAlive < 0 || timeObjectComponent.disabledTimeDataRunCount >= maxTimeData)
            {
                DestroyProjectile(ref entity, ref entityCommandBuffer);
            }
        }).Schedule();

        Rect screenRect = Utilities.ScreenRect;

        Entities.WithAll<PlayerProjectileComponent>().ForEach((ref TimeObjectComponent timeObjectComponent, in Translation translation, in WorldRenderBounds worldRenderBounds) =>
        {
            if (Utilities.IsOutsideRect(translation.Value, worldRenderBounds.Value.Extents, screenRect))
            {
                timeObjectComponent.isDisabled = true;
            }
        }).Schedule();

        Entities.WithAll<EnemyProjectileComponent>().ForEach((ref TimeObjectComponent timeObjectComponent, in Translation translation, in WorldRenderBounds worldRenderBounds) =>
        {
            timeObjectComponent.isDisabled = Utilities.IsOutsideRect(translation.Value, worldRenderBounds.Value.Extents, screenRect);
        }).Schedule();
    }

    private static void UpdateTimeData(bool isRewinding, uint maxTimeData, ref Translation translation, ref Rotation rotation, ref TimeObjectComponent timeObjectComponent,
        ref DynamicBuffer<ProjectileTimeDataBufferElement> timeData)
    {
        if (isRewinding)
        {
            if (timeData.Length <= 0) return;

            if (timeObjectComponent.disabledTimeDataRunCount > 0)
            {
                --timeObjectComponent.disabledTimeDataRunCount;
            }

            Rewind(ref translation, ref rotation, ref timeObjectComponent, ref timeData);

            timeData.RemoveAt(timeData.Length - 1);
        }
        else
        {
            while (timeData.Length >= maxTimeData)
            {
                timeData.RemoveAt(0);
            }

            Record(ref translation, ref rotation, ref timeObjectComponent, ref timeData);

            if (timeObjectComponent.isDisabled)
            {
                ++timeObjectComponent.disabledTimeDataRunCount;
            }
            else
            {
                timeObjectComponent.disabledTimeDataRunCount = 0;
            }
        }
    }

    private static void Record(ref Translation translation, ref Rotation rotation, ref TimeObjectComponent timeObjectComponent, ref DynamicBuffer<ProjectileTimeDataBufferElement> timeData)
    {
        timeData.Add(new ProjectileTimeDataBufferElement
        {
            isDisabled = timeObjectComponent.isDisabled,
            translation = translation.Value,
            rotation = rotation.Value
        });
    }

    private static void Rewind(ref Translation translation, ref Rotation rotation, ref TimeObjectComponent timeObjectComponent, ref DynamicBuffer<ProjectileTimeDataBufferElement> timeData)
    {
        ProjectileTimeDataBufferElement lastTimeData = timeData[timeData.Length - 1];

        timeObjectComponent.isDisabled = lastTimeData.isDisabled;
        translation.Value = lastTimeData.translation;
        rotation.Value = lastTimeData.rotation;
    }

    private static void DestroyProjectile(ref Entity entity, ref EntityCommandBuffer entityCommandBuffer)
    {
        entityCommandBuffer.AddComponent<DestroyTag>(entity);
    }
}