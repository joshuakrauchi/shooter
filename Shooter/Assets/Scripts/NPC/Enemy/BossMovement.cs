using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BossMovement : MonoBehaviour
{
    [SerializeField] private float initialDelay;
    public Vector2 StartPosition { get; private set; }
    public Vector2 EndPosition { get; private set; }

    public float InitialDelay
    {
        get => initialDelay;
        private set => initialDelay = value;
    }

    private Timer _initialDelayTimer;

    public Stack<Tuple<Vector2, Vector2, Timer>> FuturePositions { get; private set; }
    public Timer MovementTimer { get; private set; }

    public void Awake()
    {
        FuturePositions = new Stack<Tuple<Vector2, Vector2, Timer>>();
        _initialDelayTimer = new Timer(InitialDelay);
        MovementTimer = new Timer(0);
        StartPosition = transform.position;
        EndPosition = StartPosition;
    }

    public void ResetMovement(Vector2 startPosition, Vector2 endPosition, float totalTime, bool overrideFuturePositions)
    {
        if (FuturePositions.Count > 0 && !overrideFuturePositions)
        {
            var position = FuturePositions.Pop();

            StartPosition = position.Item1;
            EndPosition = position.Item2;
            MovementTimer = position.Item3;
        }
        else
        {
            if (FuturePositions.Count > 0)
            {
                FuturePositions = new Stack<Tuple<Vector2, Vector2, Timer>>();
            }

            StartPosition = startPosition;
            EndPosition = endPosition;
            MovementTimer = new Timer(totalTime);
        }

        _initialDelayTimer.Reset();
    }

    public Vector2 GetMovement()
    {
        if (!_initialDelayTimer.IsFinished())
        {
            _initialDelayTimer.UpdateTime();
        }
        else
        {
            MovementTimer.UpdateTime();
        }

        return Vector2.Lerp(StartPosition, EndPosition, MovementTimer.ElapsedTime / MovementTimer.TotalTime);
    }

    public bool IsFinished()
    {
        return !GameManager.IsRewinding && MovementTimer.IsFinished();
    }

    public void SetRewindData(Vector2 startPosition, Vector2 endPosition, Timer movementTimer)
    {
        if (StartPosition == startPosition && EndPosition == endPosition) return;

        FuturePositions.Push(Tuple.Create(StartPosition, EndPosition, MovementTimer));

        StartPosition = startPosition;
        EndPosition = endPosition;
        MovementTimer = movementTimer;
    }

    public static Vector2 GetRandomPosition()
    {
        return new Vector2(Random.Range(GameManager.Left, GameManager.Right), Random.Range(0f, GameManager.Top));
    }
}