using UnityEngine;

public abstract class ProjectileMovement : MonoBehaviour
{
    [field: Header("Debug")]
    [field: SerializeField] private bool DoesDrawDebugPath { get; set; }

    [field: Header("Speed")]
    [field: SerializeField] public float Speed { get; set; } = 10.0f;
    
    protected Rigidbody2D Rigidbody { get; private set; }
    protected float TimeAlive { get; private set; }
    
    private float DefaultSpeed { get; set; }

    protected virtual void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        DefaultSpeed = Speed;
    }

    public void UpdateMovement(bool isRewinding)
    {
        if (DoesDrawDebugPath)
        {
            Vector3 position = transform.position;
            Debug.DrawLine(position, position + (Vector3) Vector2.one, Color.red, 10.0f);
        }
        
        TimeAlive += isRewinding ? -Time.deltaTime : Time.deltaTime;
        
        if (TimeAlive < 0)
        {
            GetComponent<Projectile>().DestroyProjectile();
        }

        UpdateTransform(isRewinding);
    }

    public abstract void UpdateTransform(bool isRewinding);

    public virtual void ActivatePoolable()
    {
        Speed = DefaultSpeed;
        TimeAlive = 0.0f;
    }
}