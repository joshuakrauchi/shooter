using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;

[Serializable]
public partial class CollisionTrigger : ITriggerEventsJob
{

    public ComponentDataFromEntity<PhysicsCollider> physicscollider;


    public void Execute(TriggerEvent triggerEvent)
    {
        if (physicscollider.HasComponent(triggerEvent.EntityA))
        {
            
        }
    }
}