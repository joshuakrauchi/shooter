using UnityEngine;

public class MoveCircle : MoveBehaviour
{
    public override void UpdateMove(ref Vector2 velocity, float speed, float timeAlive)
    {
        velocity.x = Mathf.Sin(timeAlive) * speed;
        velocity.y = Mathf.Cos(timeAlive) * speed;
    }
}