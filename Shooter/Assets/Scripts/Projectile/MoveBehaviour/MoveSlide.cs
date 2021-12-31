using UnityEngine;

public class MoveSlide : MoveBehaviour
{
    public override void UpdateMove(ref Vector2 velocity, float speed, float timeAlive)
    {
        velocity.x = speed * Mathf.Pow(Mathf.Sin(timeAlive * 0.25f + 0.7f), 6);
        velocity.y = speed;
    }
}