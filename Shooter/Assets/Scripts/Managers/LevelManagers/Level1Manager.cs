using System.Collections.Generic;
using UnityEngine;

public class Level1Manager : LevelManager
{
    [SerializeField] private GameObject footSoldier;
    [SerializeField] private GameObject archer;
    [SerializeField] private GameObject eliteArcher;
    [SerializeField] private GameObject boss1;
    [SerializeField] private GameObject slowArrow;
    [SerializeField] private GameObject fastArrow;
    [SerializeField] private GameObject bigArrow;

    private readonly int HDangle = Animator.StringToHash("HDangle");
    private readonly int HSlide1 = Animator.StringToHash("HSlide1");
    private readonly int HSlide2 = Animator.StringToHash("HSlide2");
    private readonly int Test = Animator.StringToHash("Test");
    private readonly int VDangle = Animator.StringToHash("VDangle");
    private readonly int VSlide1 = Animator.StringToHash("VSlide1");
    private readonly int VSlide2 = Animator.StringToHash("VSlide2");
    private readonly int Wave = Animator.StringToHash("Wave");
    private readonly int VStraight = Animator.StringToHash("VStraight");

    protected override void Awake()
    {
        base.Awake();

        var projectile = new ProjectileDefinition(slowArrow, Pattern.MoveStraightSlowing);
        var fastProjectile = new ProjectileDefinition(fastArrow, Pattern.MoveStraight);
        var bigProjectile = new ProjectileDefinition(bigArrow, Pattern.MoveStraight);
        var basic = new EnemyDefinition(footSoldier, null, null);
        var basicShoot = new EnemyDefinition(archer, null, new ShootHoming(0, new LockedTimer(0.25f), projectile, 3, 20f, 5f));
        var mediumShoot = new EnemyDefinition(eliteArcher, null, new ShootHoming(0, new LockedTimer(0.25f), fastProjectile, 5, 20f, 5f));
        var boss1Projectiles = new List<ProjectileDefinition> {bigProjectile, projectile, fastProjectile};


        // 1
        for (var i = 0; i < 10; ++i)
        {
            Enemies.Add(new EnemySpawn(CurrentTime, basic, topCenter, VSlide1));
            Enemies.Add(new EnemySpawn(CurrentTime, basic, topCenter, VSlide2));
            CurrentTime += 0.25f;
        }

        CurrentTime += 0.5f;

        for (var i = 0; i < 10; ++i)
        {
            Enemies.Add(new EnemySpawn(CurrentTime, basic, topCenterFlip, VSlide1));
            Enemies.Add(new EnemySpawn(CurrentTime, basic, topCenterFlip, VSlide2));
            CurrentTime += 0.25f;
        }

        // 2
        CurrentTime += 2f;

        Enemies.Add(new EnemySpawn(CurrentTime, basicShoot, topMidRight, VDangle));
        CurrentTime += 1f;
        Enemies.Add(new EnemySpawn(CurrentTime, basicShoot, topMidLeft, VDangle));
        CurrentTime += 1f;
        Enemies.Add(new EnemySpawn(CurrentTime, basicShoot, topCenterFlip, HDangle));
        CurrentTime += 1f;
        Enemies.Add(new EnemySpawn(CurrentTime, basicShoot, topCenter, HDangle));
        CurrentTime += 1f;
        Enemies.Add(new EnemySpawn(CurrentTime, basicShoot, topCenter, VDangle));

        // 3
        CurrentTime += 5.5f;

        for (var i = 0; i < 10; ++i)
        {
            Enemies.Add(new EnemySpawn(CurrentTime, basic, topCenter, HSlide1));
            Enemies.Add(new EnemySpawn(CurrentTime, basic, topCenterFlip, HSlide1));

            CurrentTime += 0.25f;
        }

        Enemies.Add(new EnemySpawn(CurrentTime, mediumShoot, topCenter, VDangle));

        for (var i = 0; i < 15; ++i)
        {
            Enemies.Add(new EnemySpawn(CurrentTime, basic, topCenter, HSlide1));
            Enemies.Add(new EnemySpawn(CurrentTime, basic, topCenterFlip, HSlide1));

            CurrentTime += 0.25f;
        }

        // 4
        CurrentTime += 2f;

        for (var i = 0; i < 5; ++i)
        {
            Enemies.Add(new EnemySpawn(CurrentTime, basic, topLeft, Wave));
            Enemies.Add(new EnemySpawn(CurrentTime, basic, topMidLeft, Wave));
            Enemies.Add(new EnemySpawn(CurrentTime, basic, topCenter, Wave));
            Enemies.Add(new EnemySpawn(CurrentTime, basic, topMidRight, Wave));
            Enemies.Add(new EnemySpawn(CurrentTime, basic, topRight, Wave));

            CurrentTime += 0.25f;
        }

        Enemies.Add(new EnemySpawn(CurrentTime, basicShoot, topCenter, HSlide1));
        Enemies.Add(new EnemySpawn(CurrentTime, basicShoot, topCenterFlip, HSlide1));

        for (var i = 0; i < 20; ++i)
        {
            Enemies.Add(new EnemySpawn(CurrentTime, basic, topLeft, Wave));
            Enemies.Add(new EnemySpawn(CurrentTime, basic, topMidLeft, Wave));
            Enemies.Add(new EnemySpawn(CurrentTime, basic, topCenter, Wave));
            Enemies.Add(new EnemySpawn(CurrentTime, basic, topMidRight, Wave));
            Enemies.Add(new EnemySpawn(CurrentTime, basic, topRight, Wave));

            CurrentTime += 0.25f;
        }

        CurrentTime += 5f;

        Bosses.Add(new BossSpawn(CurrentTime, boss1, new Vector2(0f, GameManager.Top + 5f), boss1Projectiles));

        //Debug.Log(CurrentTime);
    }
}