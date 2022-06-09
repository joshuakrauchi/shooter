using UnityEngine;

public class ProjectileTimeData : ITimeData
{
    public Vector2 Position { get; }
    public Quaternion Rotation { get; }
    public bool IsDisabled { get; }

    public ProjectileTimeData(Vector2 position, Quaternion rotation, bool isDisabled)
    {
        Position = position;
        Rotation = rotation;
        IsDisabled = isDisabled;
    }
}