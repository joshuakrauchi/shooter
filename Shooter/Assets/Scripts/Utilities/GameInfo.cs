using System;

public class GameInfo
{
    private static readonly Lazy<GameInfo> Lazy = new Lazy<GameInfo>(() => new GameInfo());
    public static GameInfo Instance => Lazy.Value;

    public float MovementSpeed { get; set; } = 10.0f;
    public float ShootDelay { get; set; } = 0.1f;
    public uint NumberOfShots { get; set; } = 1;
    
    private GameInfo()
    {
    }
}