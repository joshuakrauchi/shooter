using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

[Serializable, GenerateAuthoringComponent]
public struct ProjectileComponent : IComponentData
{
    #region TimeData

    [HideInInspector] public bool isDisabled;

    // If the whole list is full of TimeData is disabled, it's safe to remove from the world.
    public uint disabledTimeDataRunCount;

    #endregion

    #region Projectile

    public GameState gameState;

    public float speed;
    

    #endregion
}