using System;
using UnityEngine;

[Serializable]
public class MoveSlide : MoveBehaviour
{
    public MoveSlide(float startTime) : base(startTime)
    {
    }

    public override void UpdateMove(ref Vector2 velocity, ref Quaternion rotation, float speed, float timeAlive)
    {
        velocity.x = speed * Mathf.Pow(Mathf.Sin(timeAlive * 0.25f + 0.7f), 6.0f);
        velocity.y = speed;
    }
}