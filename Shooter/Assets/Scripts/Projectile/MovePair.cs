using System;
using UnityEngine;

[Serializable]
public class MovePair
{
    [SerializeField] private float startTime;
    [SerializeReference] private MoveBehaviour moveBehaviour;

    public float StartTime
    {
        get => startTime;
        private set => startTime = value;
    }

    public MoveBehaviour MoveBehaviour
    {
        get => moveBehaviour;
        private set => moveBehaviour = value;
    }

    public MovePair(float startTime, MoveBehaviour moveBehaviour)
    {
        StartTime = startTime;
        MoveBehaviour = moveBehaviour;
    }
}