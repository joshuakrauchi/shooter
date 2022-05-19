using UnityEngine;

public static class NPCCreator
{
    public static void CreateMinion(MinionData minionData)
    {
        GameObject minionObject = Object.Instantiate(minionData.MinionPrefab, minionData.ParentTransform);

        Animator minionAnimator = minionObject.GetComponent<Animator>();
        minionAnimator.Play(minionData.AnimationName);

        Minion minion = minionObject.GetComponent<Minion>();
        minion.CreationTime = minionData.CreationTime;
        
        var animationClips = minionAnimator.runtimeAnimatorController.animationClips;

        // Loop through the animation clips in the animator to find the active one,
        // and get the length of the clip. This is used for manually setting the
        // playback position of the animation clip.
        foreach (AnimationClip clip in animationClips)
        {
            if (clip.name != minionData.AnimationName) continue;
            
            minion.AnimationLength = clip.length;
        }
    }

    public static void CreateBoss(BossData bossData)
    {
        Boss boss = Object.Instantiate(bossData.BossPrefab, bossData.Position, Quaternion.identity).GetComponent<Boss>();
        boss.CreationTime = bossData.CreationTime;
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