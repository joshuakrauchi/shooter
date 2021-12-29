using UnityEngine;

public static class Pattern
{
    private const float MinimumSpeed = 0.1f;
    private const float MaximumSpeed = 0.5f;

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

    public static void MoveCircle(ref Vector2 velocity, float xSpeed, float ySpeed, float timeAlive)
    {
        velocity.x = Mathf.Sin(timeAlive) * xSpeed;
        velocity.y = Mathf.Cos(timeAlive) * xSpeed;
    }

    public static void MoveForwardSpinAndRelease(ref Vector2 velocity, float xSpeed, float ySpeed, float timeAlive)
    {
        if (timeAlive < 1f)
        {
            MoveStraightSlowing(ref velocity, xSpeed, ySpeed, timeAlive);
        }
        else if (timeAlive < 2f)
        {
            MoveCircle(ref velocity, xSpeed, ySpeed, timeAlive - 1f);
        }
        else
        {
            velocity.x *= 1.01f;
            velocity.y *= 1.01f;
        }
    }

    public static void SpiralOutwards(ref Vector2 velocity, float xSpeed, float ySpeed, float timeAlive)
    {
        velocity.x = (2f * Mathf.Cos(timeAlive + 1f) + 1f) * xSpeed;
        velocity.y = (2f * Mathf.Sin(timeAlive + 0.5f) - 1f) * xSpeed;
    }

    public static void UpThenDown(ref Vector2 velocity, float xSpeed, float ySpeed, float timeAlive)
    {
        if (timeAlive < 1f)
        {
            velocity.x += xSpeed;
        }
        else
        {
            velocity.x -= xSpeed;
            if (xSpeed > 0f && velocity.x > MaximumSpeed || xSpeed < 0f && velocity.x < -MaximumSpeed)
            {
                velocity.x = MaximumSpeed * Mathf.Sign(xSpeed);
            }
        }
    }
}