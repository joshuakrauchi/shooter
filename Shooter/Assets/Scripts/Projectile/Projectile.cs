using UnityEngine;

public class Projectile : TimeObject
{
    [SerializeField] private ProjectileManager projectileManager;

    protected bool IsDisabled;

    protected BoxCollider2D Collider;
    protected ProjectileMovement ProjectileMovement;
    protected SpriteRenderer SpriteRenderer;

    private const float OffscreenThreshold = 2f;

    protected override void Awake()
    {
        base.Awake();

        Collider = GetComponent<BoxCollider2D>();
        ProjectileMovement = GetComponent<ProjectileMovement>();
        SpriteRenderer = GetComponent<SpriteRenderer>();

        projectileManager.AddProjectile(this);
    }

    protected override void Record()
    {
        AddTimeData(new ProjectileTimeData(transform.position, IsDisabled));

        if (((ProjectileTimeData) TimeData.First.Value).IsDisabled)
        {
            DestroyProjectile();
        }
    }

    protected override void Rewind()
    {
        if (TimeData.Count <= 0) return;

        ProjectileTimeData timeData = (ProjectileTimeData) TimeData.Last.Value;
        transform.position = timeData.Position;
        IsDisabled = timeData.IsDisabled;
        TimeData.Remove(timeData);
    }

    public override void UpdateUpdateable()
    {
        base.UpdateUpdateable();

        SpriteRenderer.enabled = !IsDisabled;
        Collider.enabled = !IsDisabled;

        ProjectileMovement.UpdateMovement(gameState.IsRewinding);
        IsDisabled = IsOffscreen();
    }

    protected bool IsOffscreen()
    {
        Vector3 position = transform.position;
        Vector3 extents = SpriteRenderer.bounds.extents;

        return position.x < gameData.ScreenRect.xMin - OffscreenThreshold - extents.x || position.x > gameData.ScreenRect.xMax + OffscreenThreshold + extents.x ||
               position.y < gameData.ScreenRect.yMin - OffscreenThreshold - extents.y || position.y > gameData.ScreenRect.yMax + OffscreenThreshold + extents.y;
    }

    public void OnHit()
    {
        IsDisabled = true;
    }

    public void DestroyProjectile()
    {
        projectileManager.RemoveProjectile(this);
        Destroy(gameObject);
    }
}