using UnityEngine;

public static class NPCCreator
{
    public static void CreateMinion(MinionSpawn minionSpawn)
    {
        MinionDefinition minionDefinition = minionSpawn.MinionDefinition;
        GameObject minionObject = Object.Instantiate(minionDefinition.Prefab, minionSpawn.ParentTransform);

        Animator minionAnimator = minionObject.GetComponent<Animator>();
        minionAnimator.SetBool(minionSpawn.AnimationID, true);

        Minion minion = minionObject.GetComponent<Minion>();
        minion.CreationTime = minionSpawn.CreationTime;
        minion.ShootBehaviour = minionDefinition.ShootBehaviour.Clone();
    }

    public static void CreateBoss(BossSpawn bossSpawn)
    {
        Object.Instantiate(bossSpawn.Prefab, bossSpawn.Position, Quaternion.identity);
    }

    public static ProjectileMovement CreateProjectile(ProjectileDefinition projectileDefinition, Vector2 position, Quaternion rotation)
    {
        GameObject projectileObject = Object.Instantiate(projectileDefinition.Prefab, position, rotation);

        ProjectileMovement projectileMovement = projectileObject.GetComponent<ProjectileMovement>();
        projectileMovement.MoveBehaviours = projectileDefinition.MoveBehaviours;

        return projectileMovement;
    }

    public static void CreateCollectible(GameObject collectible, Vector2 position)
    {
        Object.Instantiate(collectible, position, Quaternion.identity);
    }
}