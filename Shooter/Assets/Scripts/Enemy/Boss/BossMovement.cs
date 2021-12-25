using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BossMovement : MonoBehaviour
{
    public Vector2 StartPosition { get; private set; }
    public Vector2 EndPosition { get; private set; }

    public Timer InitialDelayTimer { get; private set; }

    public Stack<Tuple<Vector2, Vector2, float, float>> FuturePositions { get; private set; }
    public Timer MovementTimer { get; private set; }

    public void Awake()
    {
        FuturePositions = new Stack<Tuple<Vector2, Vector2, float, float>>();
        InitialDelayTimer = new Timer(0f);
        MovementTimer = new Timer(0f);
        StartPosition = transform.position;
        EndPosition = StartPosition;
    }

    public void ResetMovement(Vector2 startPosition, Vector2 endPosition, float totalTime, float initialDelay, bool overrideFuturePositions)
    {
        if (FuturePositions.Count > 0 && !overrideFuturePositions)
        {
            var position = FuturePositions.Pop();

            StartPosition = position.Item1;
            EndPosition = position.Item2;
            MovementTimer = new Timer(position.Item3);
            InitialDelayTimer = new Timer(position.Item4);
        }
        else
        {
            if (FuturePositions.Count > 0)
            {
                FuturePositions = new Stack<Tuple<Vector2, Vector2, float, float>>();
            }

            StartPosition = startPosition;
            EndPosition = endPosition;
            MovementTimer = new Timer(totalTime);
            InitialDelayTimer = new Timer(initialDelay);
        }

        InitialDelayTimer.Reset(false);
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

    public void SetRewindData(Vector2 startPosition, Vector2 endPosition, float totalTime, float initialDelay)
    {
        if (StartPosition == startPosition && EndPosition == endPosition) return;

        FuturePositions.Push(Tuple.Create(StartPosition, EndPosition, MovementTimer.TotalTime, InitialDelayTimer.TotalTime));

        StartPosition = startPosition;
        EndPosition = endPosition;
        MovementTimer = new Timer(totalTime);
        MovementTimer.SetToFinished();
        InitialDelayTimer = new Timer(initialDelay);
        InitialDelayTimer.SetToFinished();
    }

    public static Vector2 GetRandomPosition()
    {
        return new Vector2(Random.Range(GameManager.Left, GameManager.Right), Random.Range(0f, GameManager.Top));
    }
}