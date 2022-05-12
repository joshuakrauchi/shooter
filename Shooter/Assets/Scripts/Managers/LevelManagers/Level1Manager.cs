using UnityEngine;

public class Level1Manager : LevelManager
{
    [SerializeField] private GameObject footSoldier;
    [SerializeField] private GameObject archer;
    [SerializeField] private GameObject eliteArcher;
    [SerializeField] private GameObject boss1;
    [SerializeField] private GameObject boss2;
    [SerializeField] private GameObject boss3;
    [SerializeField] private GameObject slowArrow;
    [SerializeField] private GameObject fastArrow;
    [SerializeField] private GameObject bigArrow;
    [SerializeField] private GameObject anonArrow;
    [SerializeField] private GameObject smallArrow;

    private readonly int HDangle = Animator.StringToHash("HDangle");
    private readonly int HSlide1 = Animator.StringToHash("HSlide1");
    private readonly int HSlide2 = Animator.StringToHash("HSlide2");
    private readonly int Test = Animator.StringToHash("Test");
    private readonly int VDangle = Animator.StringToHash("VDangle");
    private readonly int VSlide1 = Animator.StringToHash("VSlide1");
    private readonly int VSlide2 = Animator.StringToHash("VSlide2");
    private readonly int Wave = Animator.StringToHash("Wave");
    private readonly int VStraight = Animator.StringToHash("VStraight");
    private readonly int DropLeave = Animator.StringToHash("DropLeave");

    protected override void Awake()
    {
        base.Awake();

        ProjectileDefinition smallProjectile = new(smallArrow, new[] {new MoveStraight(0.0f, 0.1f, -0.05f)});
        ProjectileDefinition slowingProjectile = new(slowArrow, new[] {new MoveStraight(0.0f, 0.1f, -0.05f)});
        ProjectileDefinition projectile = new(slowArrow, new[] {new MoveStraight(0.0f, 0.1f, -0.05f)});
        ProjectileDefinition fastProjectile = new(fastArrow, new[] {new MoveStraight(0.0f)});
        ProjectileDefinition bigProjectile = new(bigArrow, new[] {new MoveStraight(0.0f), new MoveStraight(2.0f, -90f)});

        MinionDefinition basicShoot = new(footSoldier, new ShootHoming(gameData.Player.gameObject, 2, new LockedTimer(1f), smallProjectile, new LockedTimer(0f), 1, 3, 20, 2f, 0f));
        MinionDefinition archerSingleShot = new(archer, new ShootHoming(gameData.Player.gameObject, 1, new LockedTimer(1f), slowingProjectile, new LockedTimer(0.1f), 1, 15, 10f, 2f, -0.05f));
        MinionDefinition archerDoubleShot = new(archer, new ShootHoming(gameData.Player.gameObject, 1, new LockedTimer(1f), slowingProjectile, new LockedTimer(0.1f), 2, 15, 10f, 2f, -0.05f));
        MinionDefinition archerNarrowShot = new(archer, new ShootHoming(gameData.Player.gameObject, 2, new LockedTimer(1f), slowingProjectile, new LockedTimer(0f), 1, 15, 5f, 2f, -0.05f));

        MinionDefinition eliteArcherShooting = new(eliteArcher, new ShootHoming(gameData.Player.gameObject, 0, new LockedTimer(1f), fastProjectile, new LockedTimer(0.1f), 3, 15, 15f, 5f, 0f));
        MinionDefinition eliteArcherDoubleShot = new(eliteArcher, new ShootHoming(gameData.Player.gameObject, 1, new LockedTimer(1f), fastProjectile, new LockedTimer(0.2f), 3, 15, 15f, 2f, 0f));

        // 1
        for (var i = 0; i < 10; ++i)
        {
            Enemies.Add(new MinionSpawn(CurrentTime, basicShoot, topM15, VSlide1));
            CurrentTime += 0.25f;
        }

        for (var i = 0; i < 10; ++i)
        {
            Enemies.Add(new MinionSpawn(CurrentTime, basicShoot, topM15, VSlide1));
            Enemies.Add(new MinionSpawn(CurrentTime, basicShoot, topP15Flip, VSlide1));
            CurrentTime += 0.25f;
        }

        // 2
        CurrentTime += 2f;

        Enemies.Add(new MinionSpawn(CurrentTime, archerDoubleShot, topP15Flip, DropLeave));
        CurrentTime += 1f;
        Enemies.Add(new MinionSpawn(CurrentTime, archerSingleShot, topM15, DropLeave));
        CurrentTime += 1f;
        Enemies.Add(new MinionSpawn(CurrentTime, archerSingleShot, topP5Flip, DropLeave));
        CurrentTime += 1f;
        Enemies.Add(new MinionSpawn(CurrentTime, archerDoubleShot, topM5, DropLeave));
        CurrentTime += 1f;
        Enemies.Add(new MinionSpawn(CurrentTime, archerDoubleShot, topP20Flip, DropLeave));
        CurrentTime += 0.75f;
        Enemies.Add(new MinionSpawn(CurrentTime, archerSingleShot, topM20, DropLeave));
        CurrentTime += 0.75f;
        Enemies.Add(new MinionSpawn(CurrentTime, archerSingleShot, topP10Flip, DropLeave));
        CurrentTime += 0.75f;
        Enemies.Add(new MinionSpawn(CurrentTime, archerDoubleShot, topM10, DropLeave));
        CurrentTime += 0.75f;
        Enemies.Add(new MinionSpawn(CurrentTime, archerDoubleShot, topP15Flip, DropLeave));
        CurrentTime += 0.5f;
        Enemies.Add(new MinionSpawn(CurrentTime, archerSingleShot, topM5, DropLeave));
        CurrentTime += 0.5f;
        Enemies.Add(new MinionSpawn(CurrentTime, archerSingleShot, topP20Flip, DropLeave));
        CurrentTime += 0.5f;
        Enemies.Add(new MinionSpawn(CurrentTime, archerDoubleShot, topM10, DropLeave));
        CurrentTime += 0.5f;
        Enemies.Add(new MinionSpawn(CurrentTime, archerDoubleShot, top, DropLeave));
        CurrentTime += 0.25f;
        Enemies.Add(new MinionSpawn(CurrentTime, archerSingleShot, topM15, DropLeave));
        CurrentTime += 0.25f;
        Enemies.Add(new MinionSpawn(CurrentTime, archerSingleShot, topP15Flip, DropLeave));
        CurrentTime += 0.25f;
        Enemies.Add(new MinionSpawn(CurrentTime, archerDoubleShot, topM5, DropLeave));
        CurrentTime += 0.25f;
        Enemies.Add(new MinionSpawn(CurrentTime, archerDoubleShot, topP5Flip, DropLeave));

        // 3
        CurrentTime += 5.5f;

        for (var i = 0; i < 10; ++i)
        {
            Enemies.Add(new MinionSpawn(CurrentTime, basicShoot, top, HSlide1));
            Enemies.Add(new MinionSpawn(CurrentTime, basicShoot, topFlip, HSlide1));

            CurrentTime += 0.25f;
        }

        Enemies.Add(new MinionSpawn(CurrentTime, eliteArcherShooting, top, VDangle));


        for (var i = 0; i < 15; ++i)
        {
            Enemies.Add(new MinionSpawn(CurrentTime, basicShoot, top, HSlide1));
            Enemies.Add(new MinionSpawn(CurrentTime, basicShoot, topFlip, HSlide1));

            CurrentTime += 0.25f;
        }

        // Boss 1
        CurrentTime += 5f;

        Bosses.Add(new BossSpawn(CurrentTime, boss1, new Vector2(0f, gameData.ScreenRect.yMax + 5f)));

        // 5
        CurrentTime += 5f;

        for (var i = 0; i < 5; ++i)
        {
            Enemies.Add(new MinionSpawn(CurrentTime, basicShoot, topM15, VSlide1));

            CurrentTime += 0.25f;
        }

        for (var i = 0; i < 5; ++i)
        {
            Enemies.Add(new MinionSpawn(CurrentTime, basicShoot, topM15, VSlide1));
            Enemies.Add(new MinionSpawn(CurrentTime, basicShoot, topP15Flip, VSlide1));

            CurrentTime += 0.25f;
        }

        for (var i = 0; i < 5; ++i)
        {
            Enemies.Add(new MinionSpawn(CurrentTime, basicShoot, topP15Flip, VSlide1));
            Enemies.Add(new MinionSpawn(CurrentTime, basicShoot, top, HSlide1));

            CurrentTime += 0.25f;
        }

        for (var i = 0; i < 5; ++i)
        {
            Enemies.Add(new MinionSpawn(CurrentTime, basicShoot, top, HSlide1));
            Enemies.Add(new MinionSpawn(CurrentTime, basicShoot, topFlip, HSlide1));

            CurrentTime += 0.25f;
        }

        // 6
        CurrentTime += 4f;

        for (var i = 0; i < 5; ++i)
        {
            Enemies.Add(new MinionSpawn(CurrentTime, basicShoot, top, HSlide1));
            Enemies.Add(new MinionSpawn(CurrentTime, basicShoot, topFlip, HSlide1));

            CurrentTime += 0.25f;
        }

        Enemies.Add(new MinionSpawn(CurrentTime, archerDoubleShot, topM15, DropLeave));
        Enemies.Add(new MinionSpawn(CurrentTime, archerDoubleShot, topP15Flip, DropLeave));

        for (var i = 0; i < 5; ++i)
        {
            Enemies.Add(new MinionSpawn(CurrentTime, basicShoot, top, HSlide1));
            Enemies.Add(new MinionSpawn(CurrentTime, basicShoot, topFlip, HSlide1));

            CurrentTime += 0.25f;
        }

        Enemies.Add(new MinionSpawn(CurrentTime, archerDoubleShot, topM20, DropLeave));
        Enemies.Add(new MinionSpawn(CurrentTime, archerDoubleShot, topP20Flip, DropLeave));

        for (var i = 0; i < 10; ++i)
        {
            Enemies.Add(new MinionSpawn(CurrentTime, basicShoot, top, HSlide1));
            Enemies.Add(new MinionSpawn(CurrentTime, basicShoot, topFlip, HSlide1));

            CurrentTime += 0.25f;
        }

        // 7
        CurrentTime += 7f;

        Enemies.Add(new MinionSpawn(CurrentTime, archerDoubleShot, topP15Flip, DropLeave));
        CurrentTime += 1f;
        Enemies.Add(new MinionSpawn(CurrentTime, archerSingleShot, topM15, DropLeave));
        CurrentTime += 1f;
        Enemies.Add(new MinionSpawn(CurrentTime, archerSingleShot, topP5Flip, DropLeave));
        CurrentTime += 1f;
        Enemies.Add(new MinionSpawn(CurrentTime, archerDoubleShot, topM5, DropLeave));
        CurrentTime += 1f;
        Enemies.Add(new MinionSpawn(CurrentTime, archerDoubleShot, topP20Flip, DropLeave));
        CurrentTime += 0.75f;
        Enemies.Add(new MinionSpawn(CurrentTime, archerSingleShot, topM20, DropLeave));
        CurrentTime += 0.75f;
        Enemies.Add(new MinionSpawn(CurrentTime, archerSingleShot, topP10Flip, DropLeave));
        CurrentTime += 0.75f;
        Enemies.Add(new MinionSpawn(CurrentTime, archerDoubleShot, topM10, DropLeave));
        CurrentTime += 0.75f;
        Enemies.Add(new MinionSpawn(CurrentTime, archerDoubleShot, topP15Flip, DropLeave));
        CurrentTime += 0.5f;
        Enemies.Add(new MinionSpawn(CurrentTime, archerSingleShot, topM5, DropLeave));
        CurrentTime += 0.5f;
        Enemies.Add(new MinionSpawn(CurrentTime, archerSingleShot, topP20Flip, DropLeave));
        CurrentTime += 0.5f;
        Enemies.Add(new MinionSpawn(CurrentTime, archerDoubleShot, topM10, DropLeave));
        CurrentTime += 0.5f;
        Enemies.Add(new MinionSpawn(CurrentTime, eliteArcherDoubleShot, top, DropLeave));
        CurrentTime += 0.25f;
        Enemies.Add(new MinionSpawn(CurrentTime, archerSingleShot, topM15, DropLeave));
        CurrentTime += 0.25f;
        Enemies.Add(new MinionSpawn(CurrentTime, archerSingleShot, topP15Flip, DropLeave));
        CurrentTime += 0.25f;
        Enemies.Add(new MinionSpawn(CurrentTime, eliteArcherDoubleShot, topM5, DropLeave));
        CurrentTime += 0.25f;
        Enemies.Add(new MinionSpawn(CurrentTime, eliteArcherDoubleShot, topP5Flip, DropLeave));

        CurrentTime += 7f;

        Bosses.Add(new BossSpawn(CurrentTime, boss2, new Vector2(0f, gameData.ScreenRect.yMax + 5f)));



        Debug.Log(CurrentTime + " " + Enemies.Count);
    }
}