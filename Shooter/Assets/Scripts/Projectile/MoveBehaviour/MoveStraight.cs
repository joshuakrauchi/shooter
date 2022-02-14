using System;
using UnityEngine;

[Serializable]
public class MoveStraight : MoveBehaviour
{
    [SerializeField] private float? newRotation;
    [SerializeField] private float minSpeed;
    [SerializeField] private float multiplier;

    public MoveStraight(float startTime, float? newRotation = null) : base(startTime)
    {
        this.newRotation = newRotation;
        minSpeed = 0f;
        multiplier = 0f;
    }

    public MoveStraight(float startTime, float minSpeed, float multiplier) : base(startTime)
    {
        newRotation = null;
        this.minSpeed = minSpeed;
        this.multiplier = multiplier;
    }

    public MoveStraight(float startTime, float newRotation, float minSpeed, float multiplier) : base(startTime)
    {
        this.newRotation = newRotation;
        this.minSpeed = minSpeed;
        this.multiplier = multiplier;
    }

    public override void UpdateMove(ref Vector2 velocity, ref Quaternion rotation, float speed, float timeAlive)
    {
        velocity.x = speed;

        velocity.x += timeAlive * multiplier;
        if (speed > 0f && velocity.x < minSpeed || speed < 0f && velocity.x > -minSpeed)
        {
            velocity.x = minSpeed * Mathf.Sign(speed);
        }

        velocity.y = 0f;

        if (newRotation != null)
        {
            rotation.eulerAngles = new Vector3(0f, 0f, (float) newRotation);
        }
    }
}