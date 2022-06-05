using System;
using UnityEngine;

public class GrowingCircleMovement : ProjectileMovement
{
    [field: SerializeField] private float InitialAmplitude { get; set; } = 0.25f;
    [field: SerializeField] private float AmplitudeIncreaseRate { get; set; } = 1.0f;

    private float CurrentAmplitude { get; set; }
    private Vector2 _deltaPosition;
    
    public override void UpdateTransform(bool isRewinding)
    {
        if (isRewinding) return;
        
        _deltaPosition.x = Mathf.Sin(TimeAlive * Speed);

        if (Math.Abs(Mathf.Sin(TimeAlive)) <= 0.01)
        {
            CurrentAmplitude += AmplitudeIncreaseRate * Time.deltaTime;
        }
        
        _deltaPosition.y = Mathf.Cos(TimeAlive * Speed);

        Rigidbody.MovePosition(Rigidbody.position + (Vector2) transform.TransformDirection(_deltaPosition * CurrentAmplitude));

    }
    
    public override void ActivatePoolable()
    {
        base.ActivatePoolable();

        CurrentAmplitude = InitialAmplitude;
    }
}