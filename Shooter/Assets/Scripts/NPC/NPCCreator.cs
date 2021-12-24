using UnityEngine;

public static class NPCCreator
{
    public static void CreateEnemy(EnemySpawn enemySpawn)
    {
        var enemyDefinition = enemySpawn.EnemyDefinition;
        var enemyObject = Object.Instantiate(enemyDefinition.Prefab, enemySpawn.Parent);

        var enemyAnimator = enemyObject.GetComponent<Animator>();
        enemyAnimator.SetBool(enemySpawn.AnimationID, true);

        var enemyShoot = enemyObject.GetComponent<EnemyShoot>();
        enemyShoot.ShootBehaviour = enemyDefinition.ShootBehaviour;
        enemyShoot.ProjectileDefinitions = enemyDefinition.ProjectileDefinitions;

        var enemy = enemyObject.GetComponent<Enemy>();
        enemy.CreationTime = enemySpawn.CreationTime;
    }

    public static void CreateBoss(BossSpawn bossSpawn)
    {
        var bossObject = Object.Instantiate(bossSpawn.Prefab, bossSpawn.Position, Quaternion.identity);
        var bossShoot = bossObject.GetComponent<EnemyShoot>();
        bossShoot.ProjectileDefinitions = bossSpawn.ProjectileDefinitions;
    }

    public static GameObject CreateProjectile(ProjectileDefinition projectileDefinition, Vector2 position, Quaternion rotation)
    {
        var projectileObject = Object.Instantiate(projectileDefinition.Prefab, position, rotation);
        var projectileMovement = projectileObject.GetComponent<NPCMovement>();
        projectileMovement.Pattern = projectileDefinition.Pattern;

        return projectileObject;
    }
}