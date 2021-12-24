using UnityEngine;

public static class Pattern
{
    private const float MinimumSpeed = 0.1f;
    
    // MOVEMENT PATTERNS
    public static void MoveStraight(ref Vector2 velocity, float xSpeed, float ySpeed, float timeAlive)
    {
        velocity.x = xSpeed;
        velocity.y = ySpeed;
    }
    
    public static void MoveStraightSlowing(ref Vector2 velocity, float xSpeed, float ySpeed, float timeAlive)
    {
        const float slowModifier = 5f;
        velocity.x = xSpeed - Mathf.Sign(xSpeed) * (timeAlive / slowModifier);
        if (xSpeed > 0f && velocity.x < MinimumSpeed || xSpeed < 0f && velocity.x > -MinimumSpeed)
        {
            velocity.x = MinimumSpeed * Mathf.Sign(xSpeed);
        }
    }

    public static void MoveCos(ref Vector2 velocity, float xSpeed, float ySpeed, float timeAlive)
    {
        velocity.x = Mathf.Cos(timeAlive) * xSpeed;
        velocity.y = ySpeed;
    }

    public static void MoveSlide(ref Vector2 velocity, float xSpeed, float ySpeed, float timeAlive)
    {
        velocity.x = xSpeed * Mathf.Pow(Mathf.Sin(timeAlive * 0.25f + 0.7f), 6);
        velocity.y = ySpeed;
    }
}