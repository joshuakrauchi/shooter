using UnityEngine;

public class ProjectileDefinition
{
    public GameObject Prefab { get; private set; }
    public ProjectileMovement.MovementPattern Pattern { get; private set; }

    public ProjectileDefinition(GameObject prefab, ProjectileMovement.MovementPattern pattern)
    {
        Prefab = prefab;
        Pattern = pattern;
    }
}