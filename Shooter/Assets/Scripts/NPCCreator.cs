using UnityEngine;

public static class NPCCreator
{
    public static void CreateMinion(MinionSpawn minionSpawn, float animationClipStartTime)
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
        Boss boss = Object.Instantiate(bossSpawn.Prefab, bossSpawn.Position, Quaternion.identity).GetComponent<Boss>();
        boss.CreationTime = bossSpawn.CreationTime;
    }

    public static ProjectileMovement CreateProjectile(GameObject projectilePrefab, Vector2 position, Quaternion rotation)
    {
        ProjectileMovement projectileMovement = Object.Instantiate(projectilePrefab, position, rotation).GetComponent<ProjectileMovement>();

        return projectileMovement;
    }

    public static void CreateCollectible(GameObject collectiblePrefab, Vector2 position)
    {
        Object.Instantiate(collectiblePrefab, position, Quaternion.identity);
    }
}