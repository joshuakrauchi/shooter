using System;
using UnityEngine;

[Serializable]
public class MovePair
{
    [field: SerializeField] public float StartTime { get; private set; }
    [field: SerializeReference] public MoveBehaviour MoveBehaviour { get; private set; }

    public MovePair(float startTime, MoveBehaviour moveBehaviour)
    {
        StartTime = startTime;
        MoveBehaviour = moveBehaviour;
    }
}