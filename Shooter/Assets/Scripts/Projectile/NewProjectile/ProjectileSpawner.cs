using Unity.Entities;
using UnityEngine;

[DisallowMultipleComponent]
public class ProjectileSpawner : MonoBehaviour, IConvertGameObjectToEntity
{
    [field: SerializeField] private GameObject Projectile { get; set; }

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        // Call methods on 'dstManager' to create runtime components on 'entity' here. Remember that:
        //
        // * You can add more than one component to the entity. It's also OK to not add any at all.
        //
        // * If you want to create more than one entity from the data in this class, use the 'conversionSystem'
        //   to do it, instead of adding entities through 'dstManager' directly.
        //
        // For example,
        //   dstManager.AddComponentData(entity, new Unity.Transforms.Scale { Value = scale });

        conversionSystem.GetPrimaryEntity(Projectile);


    }
}