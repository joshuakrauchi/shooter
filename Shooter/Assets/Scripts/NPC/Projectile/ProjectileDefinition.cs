using UnityEngine;

public class ProjectileDefinition
{
    public GameObject Prefab { get; private set; }
    public NPCMovement.NPCPattern Pattern { get; private set; }

    public ProjectileDefinition(GameObject prefab, NPCMovement.NPCPattern pattern)
    {
        Prefab = prefab;
        Pattern = pattern;
    }
}