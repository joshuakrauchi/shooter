using System;
using UnityEngine;

[Serializable]
public class MoveStraight
{
    [SerializeField] private float? newRotation;
    [SerializeField] private float minSpeed;
    [SerializeField] private float speedOverTimeMultiplier;

    public MoveStraight(float? newRotation = null)
    {
        this.newRotation = newRotation;
        minSpeed = 0.0f;
        speedOverTimeMultiplier = 0.0f;
    }

    public MoveStraight(float minSpeed, float speedOverTimeMultiplier)
    {
        newRotation = null;
        this.minSpeed = minSpeed;
        this.speedOverTimeMultiplier = speedOverTimeMultiplier;
    }

    public MoveStraight(float newRotation, float minSpeed, float speedOverTimeMultiplier)
    {
        this.newRotation = newRotation;
        this.minSpeed = minSpeed;
        this.speedOverTimeMultiplier = speedOverTimeMultiplier;
    }

    public void UpdateMove(float speed, float timeAlive)
    {
        Vector2 velocity = new Vector2
        {
            x = speed,
            y = 0.0f
        };

        velocity.x += timeAlive * speedOverTimeMultiplier;
        if (speed > 0.0f && velocity.x < minSpeed || speed < 0.0f && velocity.x > -minSpeed)
        {
            velocity.x = minSpeed * Mathf.Sign(speed);
        }
    }
}