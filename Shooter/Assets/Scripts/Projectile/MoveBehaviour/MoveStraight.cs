using System;
using UnityEngine;

[Serializable]
public class MoveStraight : MoveBehaviour
{
    [SerializeField] private float? newRotation;
    [SerializeField] private float minSpeed;
    [SerializeField] private float speedOverTimeMultiplier;

    public MoveStraight(float startTime, float? newRotation = null) : base(startTime)
    {
        this.newRotation = newRotation;
        minSpeed = 0.0f;
        speedOverTimeMultiplier = 0.0f;
    }

    public MoveStraight(float startTime, float minSpeed, float speedOverTimeMultiplier) : base(startTime)
    {
        newRotation = null;
        this.minSpeed = minSpeed;
        this.speedOverTimeMultiplier = speedOverTimeMultiplier;
    }

    public MoveStraight(float startTime, float newRotation, float minSpeed, float speedOverTimeMultiplier) : base(startTime)
    {
        this.newRotation = newRotation;
        this.minSpeed = minSpeed;
        this.speedOverTimeMultiplier = speedOverTimeMultiplier;
    }

    public override void UpdateMove(ref Vector2 velocity, ref Quaternion rotation, float speed, float timeAlive)
    {
        velocity.x = speed;

        velocity.x += timeAlive * speedOverTimeMultiplier;
        if (speed > 0.0f && velocity.x < minSpeed || speed < 0.0f && velocity.x > -minSpeed)
        {
            velocity.x = minSpeed * Mathf.Sign(speed);
        }

        velocity.y = 0.0f;

        if (newRotation != null)
        {
            rotation.eulerAngles = new Vector3(0.0f, 0.0f, (float) newRotation);
        }
    }
}