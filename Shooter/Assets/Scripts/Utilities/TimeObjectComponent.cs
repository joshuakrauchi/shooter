using System;
using Unity.Entities;
using UnityEngine;

[Serializable]
public struct TimeObjectComponent : IComponentData
{
    [HideInInspector] public bool isDisabled;
    // If the whole list is full of TimeData is disabled, it's safe to remove from the world.
    [HideInInspector] public uint disabledTimeDataRunCount;
}