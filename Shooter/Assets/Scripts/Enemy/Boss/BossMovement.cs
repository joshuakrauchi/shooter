using UnityEngine;

public class BossMovement : MonoBehaviour
{
    [SerializeField] private GameData gameData;
    public Vector2 StartPosition { get; private set; }
    public Vector2 EndPosition { get; private set; }

    public Timer InitialDelayTimer { get; private set; }

    public Timer MovementTimer { get; private set; }

    public void Awake()
    {
        InitialDelayTimer = new Timer(0.0f);
        MovementTimer = new Timer(0.0f);
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

        return Vector2.Lerp(StartPosition, EndPosition, Mathf.SmoothStep(0.0f, 1.0f, MovementTimer.ElapsedTime / MovementTimer.TotalTime));
    }

    public bool IsFinished(bool isRewinding)
    {
        return !isRewinding && MovementTimer.IsFinished(false);
    }

    public void SetRewindData(Vector2 startPosition, Vector2 endPosition, Timer movementTimer, Timer initialDelayTimer)
    {
        if (StartPosition == startPosition && EndPosition == endPosition) return;

        StartPosition = startPosition;
        EndPosition = endPosition;
        MovementTimer = movementTimer;
        InitialDelayTimer = initialDelayTimer;
    }

    public Vector2 GetRandomPosition()
    {
        return new Vector2(Random.Range(gameData.ScreenRect.xMin, gameData.ScreenRect.xMax), Random.Range(0.0f, gameData.ScreenRect.yMax));
    }
}