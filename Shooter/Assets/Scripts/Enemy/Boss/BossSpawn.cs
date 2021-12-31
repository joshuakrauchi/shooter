using UnityEngine;

public class BossSpawn
{
    public float CreationTime { get; private set; }
    public GameObject Prefab { get; private set; }
    public Vector2 Position { get; private set; }
    public ProjectileDefinition[] ProjectileDefinitions { get; private set; }

    public BossSpawn(float creationTime, GameObject prefab, Vector2 position, ProjectileDefinition[] projectileDefinitions)
    {
        CreationTime = creationTime;
        Prefab = prefab;
        Position = position;
        ProjectileDefinitions = projectileDefinitions;
    }
}