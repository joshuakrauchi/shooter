using UnityEngine;

public class Projectile : TimeObject
{
    public ProjectileMovement ProjectileMovement { get; private set; }
    public SpriteRenderer SpriteRenderer { get; private set; }
    public BoxCollider2D Collider { get; private set; }
    public bool IsDisabled { get; set; }
    public float CreationTime { get; private set; }

    private uint _disablesFound;

    private const float OffscreenThreshold = 2f;

    protected override void Awake()
    {
        base.Awake();

        ProjectileMovement = GetComponent<ProjectileMovement>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
        Collider = GetComponent<BoxCollider2D>();
        CreationTime = GameManager.LevelTime;
        ProjectileManager.Instance.AddProjectile(this);
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
        if (CreationTime > GameManager.LevelTime)
        {
            DestroyProjectile();
        }

        if (TimeData.Count <= 0) return;

        var timeData = (ProjectileTimeData) TimeData.Last.Value;
        transform.position = timeData.Position;
        IsDisabled = timeData.IsDisabled;
        TimeData.Remove(timeData);
    }

    public void UpdateProjectile()
    {
        SpriteRenderer.enabled = !IsDisabled;
        Collider.enabled = !IsDisabled;

        ProjectileMovement.UpdateMovement();
        if (IsOffscreen())
        {
            IsDisabled = true;
        }

        UpdateTimeData();
    }

    private bool IsOffscreen()
    {
        var position = transform.position;
        var spriteSize = SpriteRenderer.bounds.size;

        return position.x < GameManager.Left - OffscreenThreshold - spriteSize.x || position.x > GameManager.Right + OffscreenThreshold + spriteSize.x ||
               position.y < GameManager.Bottom - OffscreenThreshold - spriteSize.y || position.y > GameManager.Top + OffscreenThreshold + spriteSize.y;
    }

    public void DestroyProjectile()
    {
        ProjectileManager.Instance.RemoveProjectile(this);
        Destroy(gameObject);
    }
}