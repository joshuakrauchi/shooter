using UnityEngine;

public class PlayerAOEProjectile : Projectile, IDamager
{
    [field: SerializeField] public float Damage { get; set; } = 1.0f;
    [field: SerializeField] private float TimeToLive { get; set; } = 1.0f;

    private PlayerProjectileCollision PlayerProjectileCollision { get; set; }
    private Timer LifeTimer { get; set; }
    
    protected override void Awake()
    {
        base.Awake();

        PlayerProjectileCollision = GetComponent<PlayerProjectileCollision>();
        LifeTimer = new Timer(TimeToLive);
    }

    public override void ActivatePoolable()
    {
        base.ActivatePoolable();

        LifeTimer = new Timer(TimeToLive);
    }

    public override void UpdateUpdateable()
    {
        base.UpdateUpdateable();
        
        LifeTimer.UpdateTime(GameState.IsRewinding);
        
        if (LifeTimer.IsFinished(false))
        {
            IsDisabled = true;
        }

        if (!GameState.IsRewinding && !IsDisabled)
        {
            PlayerProjectileCollision.UpdateCollision();
        }
    }

    public void OnDamage()
    {
        // This projectile is piercing.
    }
}