using UnityEngine;

public class MinionSpawn
{
    public float CreationTime { get; }
    public MinionDefinition MinionDefinition { get; }
    public Transform ParentTransform { get; }
    public string AnimationName { get; }

    public MinionSpawn(float creationTime, MinionDefinition minionDefinition, Transform parentTransform, string animationName)
    {
        CreationTime = creationTime;
        MinionDefinition = minionDefinition;
        ParentTransform = parentTransform;
        AnimationName = animationName;
    }
}