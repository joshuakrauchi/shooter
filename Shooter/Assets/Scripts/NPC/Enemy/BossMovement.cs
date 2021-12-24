using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BossMovement : MonoBehaviour
{
    public Vector2 StartPosition { get; private set; }
    public Vector2 EndPosition { get; private set; }
    public float TotalTime { get; private set; }
    [SerializeField] private float moveDelay;

    public float MoveDelay
    {
        get => moveDelay;
        private set => moveDelay = value;
    }

    private float _currentDelay;

    public Stack<Tuple<Vector2, Vector2, float>> FuturePositions { get; private set; }
    private float _elapsedTime;

    public void Awake()
    {
        FuturePositions = new Stack<Tuple<Vector2, Vector2, float>>();
    }

    public void ResetMovement(Vector2 startPosition, Vector2 endPosition, float totalTime, bool overrideFuturePositions)
    {
        if (FuturePositions.Count > 0 && !overrideFuturePositions)
        {
            var position = FuturePositions.Pop();

            StartPosition = position.Item1;
            EndPosition = position.Item2;
            TotalTime = position.Item3;
        }
        else
        {
            if (FuturePositions.Count > 0)
            {
                FuturePositions = new Stack<Tuple<Vector2, Vector2, float>>();
            }

            StartPosition = startPosition;
            EndPosition = endPosition;
            TotalTime = totalTime;
        }

        _elapsedTime = 0f;
        _currentDelay = 0f;
    }

    public Vector2 GetMovement()
    {
        if (GameManager.IsRewinding)
        {
            if (_elapsedTime > 0f)
            {
                _elapsedTime -= Time.deltaTime;
            }
            else if (_currentDelay > 0f)
            {
                _currentDelay -= Time.deltaTime;
            }
        }
        else
        {
            if (_currentDelay < MoveDelay)
            {
                _currentDelay += Time.deltaTime;
            }
            else
            {
                _elapsedTime += Time.deltaTime;
            }
        }

        return Vector2.Lerp(StartPosition, EndPosition, _elapsedTime / TotalTime);
    }

    public bool IsFinished()
    {
        return _elapsedTime >= TotalTime;
    }

    public void SetRewindData(Vector2 startPosition, Vector2 endPosition, float totalTime)
    {
        if (StartPosition == startPosition && EndPosition == endPosition) return;

        FuturePositions.Push(Tuple.Create(StartPosition, EndPosition, TotalTime));

        StartPosition = startPosition;
        EndPosition = endPosition;
        TotalTime = totalTime;
        _elapsedTime = TotalTime - Time.deltaTime;
    }

    public static Vector2 GetRandomPosition()
    {
        return new Vector2(Random.Range(GameManager.Left, GameManager.Right), Random.Range(0f, GameManager.Top));
    }
}