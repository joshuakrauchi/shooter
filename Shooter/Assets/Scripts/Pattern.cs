using UnityEngine;

public static class Pattern
{
    private const float MinimumSpeed = 0.1f;
    private const float MaximumSpeed = 0.5f;


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