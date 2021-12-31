using UnityEngine;

public class MoveStraight : MoveBehaviour
{
    [SerializeField] private float _minSpeed;
    [SerializeField] private float _multiplier;

    public MoveStraight()
    {
        _minSpeed = 0f;
        _multiplier = 0f;
    }

    public MoveStraight(float minSpeed, float multiplier)
    {
        _minSpeed = minSpeed;
        _multiplier = multiplier;
    }

    public override void UpdateMove(ref Vector2 velocity, float speed, float timeAlive)
    {
        velocity.x = speed;

        velocity.x += timeAlive * _multiplier;
        if (speed > 0f && velocity.x < _minSpeed || speed < 0f && velocity.x > -_minSpeed)
        {
            velocity.x = _minSpeed * Mathf.Sign(speed);
        }

        velocity.y = 0f;
    }
}