using UnityEngine;

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

    public MoveSin()
    {
        xAmplitude = 1f;
        yAmplitude = 1f;
        xPeriod = 1f;
        yPeriod = 1f;
        xPhaseShift = 0f;
        yPhaseShift = 0f;
        xTranslation = 0f;
        yTranslation = 0f;
    }

    public MoveSin(float xAmplitude, float yAmplitude, float xPeriod, float yPeriod, float xPhaseShift, float yPhaseShift, float xTranslation, float yTranslation)
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