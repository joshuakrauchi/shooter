using UnityEngine;

public class VariableMovement : ProjectileMovement
{
    [field: SerializeField] private float MinimumSpeed { get; set; } = 0.1f;
    [field: SerializeField] private float SpeedOverTimeMultiplier { get; set; } = -0.05f;

    protected override void UpdateTransform()
    {
        Vector2 velocity = new()
        {
            x = Speed,
            y = 0.0f
        };

        velocity.x += TimeAlive * SpeedOverTimeMultiplier;
        if (Speed > 0.0f && velocity.x < MinimumSpeed || Speed < 0.0f && velocity.x > -MinimumSpeed)
        {
            velocity.x = MinimumSpeed * Mathf.Sign(Speed);
        }
        
        transform.Translate(velocity);
    }
}