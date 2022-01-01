using UnityEngine;

public class MoveToPosition : MoveBehaviour
{
    private Vector2 startPosition;
    private Vector2 endPosition;

    public MoveToPosition(Vector2 startPosition, Vector2 endPosition)
    {
        this.startPosition = startPosition;
        this.endPosition = endPosition;
    }

    public override void UpdateMove(ref Vector2 velocity, ref Quaternion rotation, float speed, float timeAlive)
    {
        Vector2.MoveTowards(startPosition, endPosition, speed);
    }
}