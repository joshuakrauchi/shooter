using UnityEngine;

public class BossData
{
    public float CreationTime { get; }
    public GameObject BossPrefab { get; }
    public Vector2 Position { get; }

    public BossData(float creationTime, GameObject bossPrefab, Vector2 position)
    {
        CreationTime = creationTime;
        BossPrefab = bossPrefab;
        Position = position;
    }
}