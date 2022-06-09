using UnityEngine;

public class SpiralMovement : ProjectileMovement
{
    [field: SerializeField] private float InitialAmplitude { get; set; } = 0.25f;
    [field: SerializeField] private float AmplitudeIncreaseRate { get; set; } = 1.0f;

    private float CurrentAmplitude { get; set; }
    private Vector2 _deltaPosition;

    protected override void Awake()
    {
        base.Awake();

        CurrentAmplitude = InitialAmplitude;
    }

    public override void UpdateTransform(bool isRewinding)
    {
        if (isRewinding) return;
        
        _deltaPosition.x = Mathf.Sin(TimeAlive * Speed);
        _deltaPosition.y = Mathf.Cos(TimeAlive * Speed);

        Rigidbody.MovePosition(Rigidbody.position + (Vector2) transform.TransformDirection(_deltaPosition * CurrentAmplitude));

        CurrentAmplitude += AmplitudeIncreaseRate * Time.deltaTime;
    }

    public override void ActivatePoolable()
    {
        base.ActivatePoolable();

        CurrentAmplitude = InitialAmplitude;
    }
}