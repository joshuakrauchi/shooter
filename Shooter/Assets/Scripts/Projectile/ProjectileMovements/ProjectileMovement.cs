using UnityEngine;

public abstract class ProjectileMovement : MonoBehaviour
{
    [field: SerializeField] public float Speed { get; set; } = 0.25f;
    
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

    protected abstract void UpdateTransform();
}