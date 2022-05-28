using UnityEngine;

public class PortalMovement : ProjectileMovement
{
    [field: SerializeField] private GameData GameData { get; set; }
    [field: SerializeField] private GameState GameState { get; set; }
    [field: SerializeField] private float DistanceToScreenEdgeBeforeTeleport { get; set; } = 5.0f;
    [field: SerializeField] private float DistanceToPlayerAfterTeleport { get; set; } = 15.0f;

    private bool HasTeleported { get; set; }
    private SpriteRenderer SpriteRenderer { get; set; }

    private Vector2 _intersection;
    private Timer teleportDelay;

    protected override void Awake()
    {
        base.Awake();
        
        SpriteRenderer = GetComponent<SpriteRenderer>();

        SetIntersectionPoint();
        
        teleportDelay = new Timer(1.5f);
    }

    protected override void UpdateTransform(bool isRewinding)
    {
        if (!HasTeleported && Vector2.Distance(transform.position, _intersection) <= DistanceToScreenEdgeBeforeTeleport)
        {
            Teleport();

            HasTeleported = true;
        }

        if (HasTeleported && !teleportDelay.IsFinished(false))
        {
            teleportDelay.UpdateTime(GameState.IsRewinding);
            return;
        }

        transform.Translate(GetStraightMovement());
    }

    private void SetIntersectionPoint()
    {
        Vector2 origin = transform.position;
        Vector2 travelPoint = transform.right * 100.0f;
        Rect screenRect = GameData.ScreenRect;

        Vector2 bottomRight = new(screenRect.xMax, screenRect.yMin);
        
        // Check bottom screen side.
        if (Utilities.GetLineIntersection(origin, travelPoint, screenRect.min, bottomRight, ref _intersection)) return;
        
        // Check right screen side.
        if (Utilities.GetLineIntersection(origin, travelPoint, screenRect.max, bottomRight, ref _intersection)) return;
        
        Vector2 topLeft = new(screenRect.xMin, screenRect.yMax);

        // Check top screen side.
        if (Utilities.GetLineIntersection(origin, travelPoint, screenRect.max, topLeft, ref _intersection)) return;
        
        // Check left screen side.
        Utilities.GetLineIntersection(origin, travelPoint, screenRect.min, topLeft, ref _intersection);
    }

    private void Teleport()
    {
        Vector2 playerPosition = GameData.Player.transform.position;
        var randomRotation = Random.Range(-180.0f, 180.0f);
        Vector2 newPosition = playerPosition + (Vector2) (Quaternion.Euler(0.0f, 0.0f, randomRotation) * Vector3.right) * DistanceToPlayerAfterTeleport;

        // If the projectile is offscreen, rotate it 90 degrees until it isn't.
        for (var i = 0; i < 3; ++i)
        {
            if (!Utilities.IsOffscreen(newPosition, SpriteRenderer.bounds.extents, GameData.ScreenRect)) break;

            randomRotation += 90.0f;
            newPosition = playerPosition + (Vector2) (Quaternion.Euler(0.0f, 0.0f, randomRotation) * Vector3.right) * DistanceToPlayerAfterTeleport;
        }

        transform.position = newPosition;

        transform.rotation = Quaternion.Euler(0.0f, 0.0f, randomRotation + 180.0f);
    }
}