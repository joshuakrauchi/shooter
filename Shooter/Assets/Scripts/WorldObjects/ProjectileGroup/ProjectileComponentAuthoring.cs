using Unity.Entities;
using UnityEngine;

[DisallowMultipleComponent]
public class ProjectileComponentAuthoring : MonoBehaviour, IConvertGameObjectToEntity
{
    [field: SerializeField] private float Speed { get; set; } = 10.0f;

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        dstManager.AddComponent<TimeObjectComponent>(entity);
        dstManager.AddComponent<ProjectileComponent>(entity);
        dstManager.SetComponentData(entity, new ProjectileComponent {speed = Speed});
        dstManager.AddBuffer<ProjectileTimeDataBufferElement>(entity);
    }
}