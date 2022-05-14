using UnityEngine;

public class TestLevelManager : LevelManager
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject projectile;

    private readonly int Test = Animator.StringToHash("Test");


    protected override void Awake()
    {
        base.Awake();

        ProjectileDefinition projectileDefinition = new(projectile, new[] {new MoveStraight(0.0f, 0.1f, -0.05f)});
        MinionDefinition basicShoot = new(enemy, new ShootHoming(gameData.Player.gameObject, 2, new Timer(1f), projectileDefinition, new Timer(0f), 1, 3, 20, 2f, 0f));

        Minions.Add(new MinionSpawn(CurrentTime, basicShoot, top, Test));

    }


}