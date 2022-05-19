using UnityEngine;

public class MinionData
{
    public float CreationTime { get; }
    public GameObject MinionPrefab { get; }
    public Transform ParentTransform { get; }
    public string AnimationName { get; }

    public MinionData(float creationTime, GameObject minionPrefab, Transform parentTransform, string animationName)
    {
        CreationTime = creationTime;
        MinionPrefab = minionPrefab;
        ParentTransform = parentTransform;
        AnimationName = animationName;
    }
}