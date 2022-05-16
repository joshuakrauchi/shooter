using UnityEngine;

public class TestLevelManager : LevelManager
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject projectile;


    protected override void Awake()
    {
        base.Awake();
        
        //ProjectileDefinition projectileDefinition = new(projectile, new MoveBehaviour[] {new MoveStraight(0.0f, 0.1f, -0.05f), new MoveSin(2.0f, 0.0f, 5.0f, 2.0f, 1.0f, 0.0f, 0.0f, 0.0f, 0.0f)});

/*        var x = new ShootSpinningFlower(1, new Timer(5f), new ProjectileDefinition(projectile, new MoveBehaviour[]
        {
            new MoveStraight(0.0f),
            new MoveSin(1.0f, 1f, 1f, 1f, 1f, 0f, 0f, 0f, 0f),
            new MoveStraight(3.0f)
        }), 8, 30, 2f, 0.25f, 0f);
*/
        var y = new ShootHoming(GameData.Player.gameObject, 0, new Timer(4.0f), projectile, new Timer(0.25f), 10, 1, 0.0f, 0.0f, 0.0f);

        MinionDefinition basicShoot = new(enemy, y);

        Minions.Add(new MinionSpawn(CurrentTime + 1.0f, basicShoot, top, "Test"));

    }


}