using UnityEngine;

public class MinionSpawn
{
    public float CreationTime { get; private set; }
    public MinionDefinition MinionDefinition { get; private set; }
    public Transform ParentTransform { get; private set; }
    public int AnimationID { get; private set; }

    public MinionSpawn(float creationTime, MinionDefinition minionDefinition, Transform parentTransform, int animationID)
    {
        CreationTime = creationTime;
        MinionDefinition = minionDefinition;
        ParentTransform = parentTransform;
        AnimationID = animationID;
    }
}