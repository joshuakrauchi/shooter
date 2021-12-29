using UnityEngine;

public class MoveStraight : MoveBehaviour
{
    [SerializeField] private bool _isSlowing;
    [SerializeField] private float _minSpeed;
    [SerializeField] private float _slowModifier;

    public MoveStraight()
    {
        _isSlowing = false;
    }

    public MoveStraight(float minSpeed, float slowModifier)
    {
        _minSpeed = minSpeed;
        _slowModifier = slowModifier;
        _isSlowing = true;
    }

    public override void UpdateMove(ref Vector2 velocity, float speed, float timeAlive)
    {
        velocity.x = speed;

        if (!_isSlowing) return;

        velocity.x -= Mathf.Sign(speed) * (timeAlive / _slowModifier);
        if (speed > 0f && velocity.x < _minSpeed || speed < 0f && velocity.x > -_minSpeed)
        {
            velocity.x = _minSpeed * Mathf.Sign(speed);
        }
    }
}