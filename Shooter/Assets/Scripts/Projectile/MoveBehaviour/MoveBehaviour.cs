using System;
using UnityEngine;

[Serializable]
public abstract class MoveBehaviour
{
    public abstract void UpdateMove(ref Vector2 velocity, ref Quaternion rotation, float speed, float timeAlive);
}