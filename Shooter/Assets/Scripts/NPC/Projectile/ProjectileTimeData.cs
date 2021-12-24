using UnityEngine;

public class ProjectileTimeData : TimeData
{
    public Vector2 Position { get; private set; }
    public Vector2 Velocity { get; private set; }
    public bool IsDisabled { get; private set; }

    public ProjectileTimeData(float time, Vector2 position, Vector2 velocity, bool isDisabled) : base(time)
    {
        Position = position;
        Velocity = velocity;
        IsDisabled = isDisabled;
    }
}