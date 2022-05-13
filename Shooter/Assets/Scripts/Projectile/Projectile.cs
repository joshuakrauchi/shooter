using UnityEngine;

public class Projectile : TimeObject
{
    [field: SerializeField] private ProjectileManager ProjectileManager { get; set; }

    protected bool IsDisabled { get; set; }

    protected BoxCollider2D Collider { get; private set; }
    protected ProjectileMovement ProjectileMovement { get; private set; }
    protected SpriteRenderer SpriteRenderer { get; private set; }

    private const float OffscreenThreshold = 2f;

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
        AddTimeData(new ProjectileTimeData(transform.position, IsDisabled));

        if (((ProjectileTimeData)TimeData.First.Value).IsDisabled)
        {
            DestroyProjectile();
        }
    }

    protected override void Rewind(ITimeData timeData)
    {
        ProjectileTimeData projectileTimeData = (ProjectileTimeData)TimeData.Last.Value;
        transform.position = projectileTimeData.Position;
        IsDisabled = projectileTimeData.IsDisabled;
    }

    public override void UpdateUpdateable()
    {
        base.UpdateUpdateable();

        SpriteRenderer.enabled = !IsDisabled;
        Collider.enabled = !IsDisabled;

        ProjectileMovement.UpdateMovement(GameState.IsRewinding);
        IsDisabled = IsOffscreen();
    }

    protected bool IsOffscreen()
    {
        Vector3 position = transform.position;
        Vector3 extents = SpriteRenderer.bounds.extents;

        return position.x < GameData.ScreenRect.xMin - OffscreenThreshold - extents.x || position.x > GameData.ScreenRect.xMax + OffscreenThreshold + extents.x ||
               position.y < GameData.ScreenRect.yMin - OffscreenThreshold - extents.y || position.y > GameData.ScreenRect.yMax + OffscreenThreshold + extents.y;
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