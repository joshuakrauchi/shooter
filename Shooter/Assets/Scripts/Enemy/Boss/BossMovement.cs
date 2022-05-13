using UnityEngine;

public class BossMovement
{
    private Vector2 StartPosition { get; }
    private Vector2 EndPosition { get; }
    private Timer InitialDelayTimer { get; }
    private Timer MovementTimer { get; }

    public BossMovement(Vector2 startPosition, Vector2 endPosition, float totalTime, float initialDelay)
    {
        StartPosition = startPosition;
        EndPosition = endPosition;
        InitialDelayTimer = new Timer(initialDelay);
        MovementTimer = new Timer(totalTime);
    }

    public Vector2 GetMovement(bool isRewinding)
    {
        if (!InitialDelayTimer.IsFinished(false) || isRewinding && MovementTimer.IsFinished(true))
        {
            InitialDelayTimer.UpdateTime(isRewinding);
        }
        else
        {
            MovementTimer.UpdateTime(isRewinding);
        }

        if (MovementTimer.TotalTime <= 0.0f) return StartPosition;

        return Vector2.Lerp(StartPosition, EndPosition, Mathf.SmoothStep(0.0f, 1.0f, MovementTimer.ElapsedTime / MovementTimer.TotalTime));
    }

    public bool IsFinished(bool isRewinding)
    {
        return !isRewinding && MovementTimer.IsFinished(false);
    }

    public static Vector2 GetRandomPosition(float xMin, float xMax, float yMin, float yMax)
    {
        return new Vector2(Random.Range(xMin, xMax), Random.Range(yMin, yMax));
    }
}