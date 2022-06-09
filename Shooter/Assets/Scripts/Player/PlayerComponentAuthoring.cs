using Unity.Entities;
using UnityEngine;

[DisallowMultipleComponent]
public class PlayerComponentAuthoring : MonoBehaviour, IConvertGameObjectToEntity
{
    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        GameInfo.Instance.PlayerEntity = entity;

        dstManager.AddComponent<PlayerComponent>(entity);
    }
}