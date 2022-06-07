using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

[UpdateInGroup(typeof(ProjectileGroup))]
public partial class ProjectileSystem : SystemBase
{
    protected override void OnUpdate()
    {
        var isRewinding = GameManager.isRewinding;
        float3 offscreenPosition = Utilities.OffscreenPosition;

        Entities.ForEach((ref Translation translation, ref Rotation rotation, ref ProjectileComponent projectileComponent,
            ref DynamicBuffer<ProjectileTimeDataBufferElement> timeData) =>
        {
            UpdateTimeData(isRewinding, ref translation, ref rotation, ref projectileComponent, ref timeData);

            if (projectileComponent.isDisabled)
            {
                translation.Value = offscreenPosition;
            }
        }).Schedule();
    }

    private static void UpdateTimeData(bool isRewinding, ref Translation translation, ref Rotation rotation, ref ProjectileComponent projectileComponent,
        ref DynamicBuffer<ProjectileTimeDataBufferElement> timeData)
    {
        if (isRewinding)
        {
            if (timeData.Length <= 0) return;

            if (projectileComponent.disabledTimeDataRunCount > 0)
            {
                --projectileComponent.disabledTimeDataRunCount;
            }

            Rewind(ref translation, ref rotation, ref projectileComponent, ref timeData);

            timeData.RemoveAt(timeData.Length - 1);
        }
        else
        {
            while (timeData.Length >= projectileComponent.maxTimeData)
            {
                timeData.RemoveAt(0);
            }

            Record(ref translation, ref rotation, ref projectileComponent, ref timeData);

            if (projectileComponent.isDisabled)
            {
                ++projectileComponent.disabledTimeDataRunCount;

                if (projectileComponent.disabledTimeDataRunCount >= projectileComponent.maxTimeData)
                {
                    DestroyProjectile();
                }
            }
            else
            {
                projectileComponent.disabledTimeDataRunCount = 0;
            }
        }
    }

    private static void Record(ref Translation translation, ref Rotation rotation, ref ProjectileComponent projectileComponent, ref DynamicBuffer<ProjectileTimeDataBufferElement> timeData)
    {
        timeData.Add(new ProjectileTimeDataBufferElement
        {
            isDisabled = projectileComponent.isDisabled,
            translation = translation.Value,
            rotation = rotation.Value
        });
    }

    private static void Rewind(ref Translation translation, ref Rotation rotation, ref ProjectileComponent projectileComponent, ref DynamicBuffer<ProjectileTimeDataBufferElement> timeData)
    {
        ProjectileTimeDataBufferElement lastTimeData = timeData[timeData.Length - 1];

        projectileComponent.isDisabled = lastTimeData.isDisabled;
        translation.Value = lastTimeData.translation;
        rotation.Value = lastTimeData.rotation;
    }

    public void ActivatePoolable()
    {
        //IsDisabled = false;

        //Speed = DefaultSpeed;
        //TimeAlive = 0.0f;
    }

    private static void DestroyProjectile()
    {
        //ProjectileManager.RemoveProjectile(this);
    }
}