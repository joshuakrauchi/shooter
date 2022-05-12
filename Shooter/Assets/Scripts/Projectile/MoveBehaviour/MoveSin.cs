using System;
using UnityEngine;

[Serializable]
public class MoveSin : MoveBehaviour
{
    [SerializeField] private float xAmplitude;
    [SerializeField] private float yAmplitude;
    [SerializeField] private float xPeriod;
    [SerializeField] private float yPeriod;
    [SerializeField] private float xPhaseShift;
    [SerializeField] private float yPhaseShift;
    [SerializeField] private float xTranslation;
    [SerializeField] private float yTranslation;

    public MoveSin(float startTime) : base(startTime)
    {
        xAmplitude = 1.0f;
        yAmplitude = 1.0f;
        xPeriod = 1.0f;
        yPeriod = 1.0f;
        xPhaseShift = 0.0f;
        yPhaseShift = 0.0f;
        xTranslation = 0.0f;
        yTranslation = 0.0f;
    }

    public MoveSin(float startTime, float xAmplitude, float yAmplitude, float xPeriod, float yPeriod, float xPhaseShift, float yPhaseShift, float xTranslation, float yTranslation) : base(startTime)
    {
        this.xAmplitude = xAmplitude;
        this.yAmplitude = yAmplitude;
        this.xPeriod = xPeriod;
        this.yPeriod = yPeriod;
        this.xPhaseShift = xPhaseShift;
        this.yPhaseShift = yPhaseShift;
        this.xTranslation = xTranslation;
        this.yTranslation = yTranslation;
    }

    public override void UpdateMove(ref Vector2 velocity, ref Quaternion rotation, float speed, float timeAlive)
    {
        velocity.x = xAmplitude * Mathf.Sin(xPeriod * (timeAlive + xPhaseShift)) + xTranslation;
        velocity.y = yAmplitude * Mathf.Sin(yPeriod * (timeAlive + yPhaseShift)) + yTranslation;
        velocity *= speed;
    }
}