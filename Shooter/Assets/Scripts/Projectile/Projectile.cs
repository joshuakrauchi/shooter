using UnityEngine;

public class Projectile : TimeObject
{
    [SerializeField] private ProjectileManager projectileManager;

    public bool IsDisabled { get; set; }

    protected BoxCollider2D Collider;
    protected ProjectileMovement ProjectileMovement;
    protected SpriteRenderer SpriteRenderer;

    private uint _disablesFound;

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

        if (IsDisabled)
        {
            ++_disablesFound;
        }
        else
        {
            _disablesFound = 0;
        }

        if (_disablesFound >= TimeData.Count)
        {
            DestroyProjectile();
        }
    }

    protected override void Rewind()
    {
        if (TimeData.Count <= 0) return;

        var timeData = (ProjectileTimeData) TimeData.Last.Value;
        transform.position = timeData.Position;
        IsDisabled = timeData.IsDisabled;
        TimeData.Remove(timeData);
    }

    public override void UpdateUpdatable()
    {
        SpriteRenderer.enabled = !IsDisabled;
        Collider.enabled = !IsDisabled;

        ProjectileMovement.UpdateMovement(gameState.IsRewinding);
        IsDisabled = IsOffscreen();

        UpdateTimeData();
    }

    protected bool IsOffscreen()
    {
        var position = transform.position;
        var extents = SpriteRenderer.bounds.extents;

        return position.x < GameManager.ScreenRect.xMin - OffscreenThreshold - extents.x || position.x > GameManager.ScreenRect.xMax + OffscreenThreshold + extents.x ||
               position.y < GameManager.ScreenRect.yMin - OffscreenThreshold - extents.y || position.y > GameManager.ScreenRect.yMax + OffscreenThreshold + extents.y;
    }

    public void DestroyProjectile()
    {
        projectileManager.RemoveProjectile(this);
        Destroy(gameObject);
    }
}