using UnityEngine;

public class StraightMovement : ProjectileMovement
{
    [field: SerializeField] private bool IsVariableSpeed { get; set; }

    // Only takes effect if IsVariableSpeed.
    [field: SerializeField] private float MinimumSpeed { get; set; } = 0.1f;
    // Only takes effect if IsVariableSpeed.
    [field: SerializeField] private float SpeedOverTimeMultiplier { get; set; } = -0.05f;

    public override void UpdateTransform(bool isRewinding)
    {
        if (isRewinding) return;

        Rigidbody.MovePosition(Rigidbody.position + (Vector2) transform.TransformDirection(GetStraightMovement() * Time.deltaTime));
    }
    
    private Vector2 GetStraightMovement()
    {
        Vector2 velocity = new Vector2()
        {
            x = Speed,
            y = 0.0f
        };

        if (!IsVariableSpeed) return velocity;

        velocity.x += TimeAlive * SpeedOverTimeMultiplier;
        if (Speed > 0.0f && velocity.x < MinimumSpeed || Speed < 0.0f && velocity.x > -MinimumSpeed)
        {
            velocity.x = MinimumSpeed * Mathf.Sign(Speed);
        }

        return velocity;
    }
}