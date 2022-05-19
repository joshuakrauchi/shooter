using UnityEngine;

public abstract class Projectile : TimeObject
{
    [field: SerializeField] private ProjectileManager ProjectileManager { get; set; }

    protected BoxCollider2D Collider { get; private set; }
    protected ProjectileMovement ProjectileMovement { get; private set; }
    protected SpriteRenderer SpriteRenderer { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        Collider = GetComponent<BoxCollider2D>();
        ProjectileMovement = GetComponent<ProjectileMovement>();
        SpriteRenderer = GetComponent<SpriteRenderer>();

        ProjectileManager.AddProjectile(this);
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
        base.UpdateUpdateable();

        SpriteRenderer.enabled = !IsDisabled;
        Collider.enabled = !IsDisabled;

        ProjectileMovement.UpdateMovement(GameState.IsRewinding);
    }

    public void OnHit()
    {
        IsDisabled = true;
    }

    public void DestroyProjectile()
    {
        ProjectileManager.RemoveProjectile(this);
        Destroy(gameObject);
    }
}