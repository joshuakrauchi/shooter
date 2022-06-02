using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    [field: SerializeField] public float Speed { get; set; } = 5.0f;
    [field: SerializeField] private bool IsVariableSpeed { get; set; }

    // Only takes effect if IsVariableSpeed.
    [field: SerializeField] private float MinimumSpeed { get; set; } = 0.1f;

    // Only takes effect if IsVariableSpeed.
    [field: SerializeField] private float SpeedOverTimeMultiplier { get; set; } = -0.05f;

    protected Rigidbody2D Rigidbody { get; private set; }
    protected float TimeAlive { get; private set; }

    protected virtual void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
    }

    public void UpdateMovement(bool isRewinding)
    {
        TimeAlive += isRewinding ? -Time.deltaTime : Time.deltaTime;
        
        if (TimeAlive < 0)
        {
            GetComponent<Projectile>().DestroyProjectile();
        }

        UpdateTransform(isRewinding);
    }

    protected virtual void UpdateTransform(bool isRewinding)
    {
        if (isRewinding) return;

        Rigidbody.MovePosition(Rigidbody.position + (Vector2) transform.TransformDirection(GetStraightMovement() * Time.deltaTime));
    }

    public virtual void ActivatePoolable()
    {
        TimeAlive = 0.0f;
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