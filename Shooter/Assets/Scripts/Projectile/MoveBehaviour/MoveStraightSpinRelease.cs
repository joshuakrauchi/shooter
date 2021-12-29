using UnityEngine;

public class MoveStraightSpinRelease : MoveBehaviour
{
    [SerializeReference] private MoveCircle _moveCircle;
    [SerializeReference] private MoveStraight _moveStraight;

    public MoveStraightSpinRelease(MoveCircle moveCircle, MoveStraight moveStraight)
    {
        _moveCircle = moveCircle;
        _moveStraight = moveStraight;
    }

    public override void UpdateMove(ref Vector2 velocity, float speed, float timeAlive)
    {
        if (timeAlive < 1f)
        {
            _moveStraight.UpdateMove(ref velocity, speed, timeAlive);
        }
        else if (timeAlive < 2f)
        {
            _moveCircle.UpdateMove(ref velocity, speed, timeAlive - 1f);
        }
        else
        {
            velocity.x *= 1.01f;
            velocity.y *= 1.01f;
        }
    }
}