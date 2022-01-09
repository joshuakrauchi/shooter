using UnityEngine;

public class Projectile : TimeObject
{
    [SerializeField] private ProjectileManager projectileManager;
    public ProjectileMovement ProjectileMovement { get; private set; }
    public SpriteRenderer SpriteRenderer { get; private set; }
    public BoxCollider2D Collider { get; private set; }
    public bool IsDisabled { get; set; }

    private uint _disablesFound;

    private const float OffscreenThreshold = 2f;

    protected override void Awake()
    {
        base.Awake();

        ProjectileMovement = GetComponent<ProjectileMovement>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
        Collider = GetComponent<BoxCollider2D>();
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

        ProjectileMovement.UpdateMovement();
        IsDisabled = IsOffscreen();

        UpdateTimeData();
    }

    protected bool IsOffscreen()
    {
        var position = transform.position;
        var spriteSize = SpriteRenderer.bounds.size;

        return position.x < GameManager.ScreenRect.xMin - OffscreenThreshold - spriteSize.x || position.x > GameManager.ScreenRect.xMax + OffscreenThreshold + spriteSize.x ||
               position.y < GameManager.ScreenRect.yMin - OffscreenThreshold - spriteSize.y || position.y > GameManager.ScreenRect.yMax + OffscreenThreshold + spriteSize.y;
    }

    public void DestroyProjectile()
    {
        projectileManager.RemoveProjectile(this);
        Destroy(gameObject);
    }
}