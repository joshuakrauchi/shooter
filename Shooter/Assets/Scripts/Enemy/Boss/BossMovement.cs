using UnityEngine;

public class BossMovement : MonoBehaviour
{
    public Vector2 StartPosition { get; private set; }
    public Vector2 EndPosition { get; private set; }

    public Timer InitialDelayTimer { get; private set; }

    public Timer MovementTimer { get; private set; }

    public void Awake()
    {
        InitialDelayTimer = new Timer(0f);
        MovementTimer = new Timer(0f);
        StartPosition = transform.position;
        EndPosition = StartPosition;
    }

    public void ResetMovement(Vector2 startPosition, Vector2 endPosition, float totalTime, float initialDelay)
    {
        StartPosition = startPosition;
        EndPosition = endPosition;
        MovementTimer = new Timer(totalTime);
        InitialDelayTimer = new Timer(initialDelay);
    }

    public Vector2 GetMovement()
    {
        if (!InitialDelayTimer.IsFinished(false) || GameManager.IsRewinding && MovementTimer.IsFinished(true))
        {
            InitialDelayTimer.UpdateTime();
        }
        else
        {
            MovementTimer.UpdateTime();
        }

        return Vector2.Lerp(StartPosition, EndPosition, Mathf.SmoothStep(0f, 1f, MovementTimer.ElapsedTime / MovementTimer.TotalTime));
    }

    public bool IsFinished()
    {
        return !GameManager.IsRewinding && MovementTimer.IsFinished(false);
    }

    public void SetRewindData(Vector2 startPosition, Vector2 endPosition, Timer movementTimer, Timer initialDelayTimer)
    {
        if (StartPosition == startPosition && EndPosition == endPosition) return;

        StartPosition = startPosition;
        EndPosition = endPosition;
        MovementTimer = movementTimer;
        InitialDelayTimer = initialDelayTimer;
    }

    public static Vector2 GetRandomPosition()
    {
        return new Vector2(Random.Range(GameManager.Left, GameManager.Right), Random.Range(0f, GameManager.Top));
    }
}