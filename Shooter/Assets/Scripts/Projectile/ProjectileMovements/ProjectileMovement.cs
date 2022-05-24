using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    [field: SerializeField] public float Speed { get; set; } = 0.25f;
    [field: SerializeField] private bool IsVariableSpeed { get; set; }

    // Only takes effect if IsVariableSpeed.
    [field: SerializeField] private float MinimumSpeed { get; set; } = 0.1f;

    // Only takes effect if IsVariableSpeed.
    [field: SerializeField] private float SpeedOverTimeMultiplier { get; set; } = -0.05f;

    protected float TimeAlive { get; private set; }

    public void UpdateMovement(bool isRewinding)
    {
        if (!isRewinding)
        {
            TimeAlive += Time.deltaTime;

            UpdateTransform();
        }
        else
        {
            TimeAlive -= Time.deltaTime;

            if (TimeAlive < 0)
            {
                GetComponent<Projectile>().DestroyProjectile();
            }
        }
    }

    protected virtual void UpdateTransform()
    {
        transform.Translate(GetStraightMovement() * Time.deltaTime);
    }

    protected Vector2 GetStraightMovement()
    {
        Vector2 velocity = new()
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