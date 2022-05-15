using System;
using UnityEngine;

[Serializable]
public abstract class MoveBehaviour
{
    [field: SerializeField] public float StartTime { get; private set; }

    protected MoveBehaviour(float startTime)
    {
        StartTime = startTime;
    }

    public abstract void UpdateMove(ref Vector2 velocity, ref Quaternion rotation, float speed, float timeAlive);
}