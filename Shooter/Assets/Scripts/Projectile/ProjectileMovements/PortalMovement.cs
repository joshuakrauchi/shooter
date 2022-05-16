using UnityEngine;

public class PortalMovement : ProjectileMovement
{
    private bool HasTeleported { get; set; }
    

    private void Awake()
    {
        // Get vector for direction of projectile
        // Find point of intersection with one of the four sides of the screen
        // 4/5s of the way there or so, change position and rotation.
    }
    
    protected bool IsOffscreen()
    {
        Vector3 position = transform.position;
        Vector3 extents = SpriteRenderer.bounds.extents;

        return position.x < GameData.ScreenRect.xMin - OffscreenThreshold - extents.x || position.x > GameData.ScreenRect.xMax + OffscreenThreshold + extents.x ||
               position.y < GameData.ScreenRect.yMin - OffscreenThreshold - extents.y || position.y > GameData.ScreenRect.yMax + OffscreenThreshold + extents.y;
    }
    
    protected override void UpdateTransform()
    {
        if (!HasTeleported)
        {
            
        }
        Vector2 velocity = new()
        {
            x = Speed,
            y = 0.0f
        };

        transform.Translate(velocity);
    }

    private Vector2 sds()
    {
        Vector2 velocity = new()
        {
            x = Speed,
            y = 0.0f
        };

        transform.Translate(velocity);
    }
}