using UnityEngine;

public class EnemySpawn
{
    public float CreationTime { get; set; }
    public EnemyDefinition EnemyDefinition { get; private set; }
    public Transform Parent { get; private set; }
    public int AnimationID { get; private set; }

    public EnemySpawn(float creationTime, EnemyDefinition enemyDefinition, Transform parent, int animationID)
    {
        CreationTime = creationTime;
        EnemyDefinition = enemyDefinition;
        Parent = parent;
        AnimationID = animationID;
    }
}