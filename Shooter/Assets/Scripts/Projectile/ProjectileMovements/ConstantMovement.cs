using UnityEngine;

public class ConstantMovement : ProjectileMovement
{
    protected override void UpdateTransform()
    {
        Vector2 velocity = new()
        {
            x = Speed,
            y = 0.0f
        };

        transform.Translate(velocity);
    }
}