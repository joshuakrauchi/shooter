using UnityEngine;

public class BossSpawn
{
    public float CreationTime { get; }
    public GameObject Prefab { get; }
    public Vector2 Position { get; }

    public BossSpawn(float creationTime, GameObject prefab, Vector2 position)
    {
        CreationTime = creationTime;
        Prefab = prefab;
        Position = position;
    }
}