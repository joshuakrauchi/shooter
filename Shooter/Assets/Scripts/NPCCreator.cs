using System.Collections.Generic;
using UnityEngine;

public static class NPCCreator
{
    public static void CreateEnemy(EnemySpawn enemySpawn)
    {
        var enemyDefinition = enemySpawn.EnemyDefinition;
        var enemyObject = Object.Instantiate(enemyDefinition.Prefab, enemySpawn.Parent);

        var enemyAnimator = enemyObject.GetComponent<Animator>();
        enemyAnimator.SetBool(enemySpawn.AnimationID, true);

        var enemy = enemyObject.GetComponent<Enemy>();
        enemy.CreationTime = enemySpawn.CreationTime;
        enemy.ProjectileDefinitions = enemyDefinition.ProjectileDefinitions;
        enemy.ShootBehaviours = new List<ShootBehaviour> {enemyDefinition.ShootBehaviour};
    }

    public static void CreateBoss(BossSpawn bossSpawn)
    {
        var bossObject = Object.Instantiate(bossSpawn.Prefab, bossSpawn.Position, Quaternion.identity);

        var boss = bossObject.GetComponent<Boss>();
        boss.ProjectileDefinitions = bossSpawn.ProjectileDefinitions;
    }

    public static void CreateProjectile(ProjectileDefinition projectileDefinition, Vector2 position, Quaternion rotation)
    {
        var projectileObject = Object.Instantiate(projectileDefinition.Prefab, position, rotation);

        var projectileMovement = projectileObject.GetComponent<ProjectileMovement>();
        projectileMovement.Pattern = projectileDefinition.Pattern;
    }

    public static void CreateCollectible(GameObject collectible, Vector2 position)
    {
        Object.Instantiate(collectible, position, Quaternion.identity);
    }
}