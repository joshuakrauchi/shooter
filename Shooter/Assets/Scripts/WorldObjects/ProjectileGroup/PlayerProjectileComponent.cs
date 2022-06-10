using System;
using Unity.Entities;
using UnityEngine;

[Serializable, GenerateAuthoringComponent]
public struct PlayerProjectileComponent : IComponentData
{
    public float damageMultiplier;
    
    [HideInInspector] public bool hasHitEnemyThisFrame;
}