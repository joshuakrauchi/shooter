using UnityEngine;

public abstract class Projectile : TimeObject, IPoolable
{
    [field: SerializeField] private ProjectileManager ProjectileManager { get; set; }

    public int PoolID { get; set; }

    private Rigidbody2D Rigidbody { get; set; }
    private ProjectileMovement ProjectileMovement { get; set; }
    protected SpriteRenderer SpriteRenderer { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        Rigidbody = GetComponent<Rigidbody2D>();
        ProjectileMovement = GetComponent<ProjectileMovement>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void Record()
    {
        AddTimeData(new ProjectileTimeData(transform.position, transform.rotation, IsDisabled));
    }

    protected override void Rewind(ITimeData timeData)
    {
        ProjectileTimeData projectileTimeData = (ProjectileTimeData) timeData;
        transform.position = projectileTimeData.Position;
        transform.rotation = projectileTimeData.Rotation;
        IsDisabled = projectileTimeData.IsDisabled;
    }

    protected override void OnFullyDisabled()
    {
        DestroyProjectile();
    }

    public override void UpdateUpdateable()
    {
        //base.UpdateUpdateable();

        //SpriteRenderer.enabled = true;//!IsDisabled;
        //Rigidbody.simulated = true;//!IsDisabled;

        //ProjectileMovement.UpdateMovement(GameState.IsRewinding);
    }

    public virtual void ActivatePoolable()
    {
        IsDisabled = false;
        
        ProjectileMovement.ActivatePoolable();
    }

    public void DestroyProjectile()
    {
        ProjectileManager.RemoveProjectile(this);
    }
}