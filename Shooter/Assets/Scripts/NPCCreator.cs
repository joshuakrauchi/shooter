using System.Collections.Generic;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

public static class NPCCreator
{
    private static Dictionary<GameObject, Entity> ConvertedGameObjects { get; set; }

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

    public static void CreateCollectible(GameObject collectiblePrefab, Vector2 position)
    {
        Object.Instantiate(collectiblePrefab, position, Quaternion.identity);
    }

    public static Entity CreateProjectile(GameObject projectilePrefab, Vector3 position, Quaternion rotation)
    {
        EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

        GameObjectConversionSettings settings = GameObjectConversionSettings.FromWorld(World.DefaultGameObjectInjectionWorld, GameManager.BlobAssetStore);
        Entity entity = GameObjectConversionUtility.ConvertGameObjectHierarchy(projectilePrefab, settings);

        Entity projectile = entityManager.Instantiate(entity);
        entityManager.SetComponentData(projectile, new Translation {Value = position});
        entityManager.SetComponentData(projectile, new Rotation {Value = rotation});

        return projectile;
    }
}