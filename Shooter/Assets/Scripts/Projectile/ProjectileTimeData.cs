using UnityEngine;

public class ProjectileTimeData : ITimeData
{
    public Vector2 Position { get; }
    public bool IsDisabled { get; }

    public ProjectileTimeData(Vector2 position, bool isDisabled)
    {
        Position = position;
        IsDisabled = isDisabled;
    }
}