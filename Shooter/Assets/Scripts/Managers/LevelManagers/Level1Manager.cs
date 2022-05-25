using UnityEngine;

public class Level1Manager : LevelManager
{
    [SerializeField] private GameObject basicShoot;
    [SerializeField] private GameObject archerSingleShot;
    [SerializeField] private GameObject archerDoubleShot;
    [SerializeField] private GameObject eliteArcherShooting;
    [SerializeField] private GameObject eliteArcherDoubleShot;
    [SerializeField] private GameObject boss1;
    [SerializeField] private GameObject boss2;
    [SerializeField] private GameObject boss3;
    [SerializeField] private GameObject slowingArrow;
    [SerializeField] private GameObject smallArrow;
    [SerializeField] private GameObject fastArrow;
    [SerializeField] private GameObject bigArrow;
    [SerializeField] private GameObject anonArrow;

    private const string HDangle = "HDangle";
    private const string HSlide1 = "HSlide1";
    private const string HSlide2 = "HSlide2";
    private const string Test = "Test";
    private const string VDangle = "VDangle";
    private const string VSlide1 = "VSlide1";
    private const string VSlide2 = "VSlide2";
    private const string Wave = "Wave";
    private const string VStraight = "VStraight";
    private const string DropLeave = "DropLeave";

    protected override void Awake()
    {
        base.Awake();

        //MinionDefinition basicShoot = new(footSoldier, new ShootHoming(GameData.Player.gameObject, 1, new Timer(1f), smallArrow, new Timer(0f), 1, 3, 20, 2f, 0f));
        //MinionDefinition archerSingleShot = new(archer, new ShootHoming(GameData.Player.gameObject, 1, new Timer(1f), slowingArrow, new Timer(0.1f), 1, 15, 10f, 2f, -0.05f));
        //MinionDefinition archerDoubleShot = new(archer, new ShootHoming(GameData.Player.gameObject, 1, new Timer(1f), slowingArrow, new Timer(0.1f), 2, 15, 10f, 2f, -0.05f));

        //MinionDefinition eliteArcherShooting = new(eliteArcher, new ShootHoming(GameData.Player.gameObject, 0, new Timer(1f), fastArrow, new Timer(0.1f), 3, 15, 15f, 5f, 0f));
        //MinionDefinition eliteArcherDoubleShot = new(eliteArcher, new ShootHoming(GameData.Player.gameObject, 1, new Timer(1f), fastArrow, new Timer(0.2f), 3, 15, 15f, 2f, 0f));


        return;
        
        
        Minions.Add(new MinionData(CurrentTime, basicShoot, TopTransforms[-3], VSlide1));

        // 1
        for (var i = 0; i < 10; ++i)
        {
            AddMinion(basicShoot, TopTransforms[-3], VSlide1);
            CurrentTime += 0.25f;
        }

        for (var i = 0; i < 10; ++i)
        {
            Minions.Add(new MinionData(CurrentTime, basicShoot, TopTransforms[-3], VSlide1));
            Minions.Add(new MinionData(CurrentTime, basicShoot, TopTransformsFlipped[3], VSlide1));
            CurrentTime += 0.25f;
        }

        // 2
        CurrentTime += 2f;

        Minions.Add(new MinionData(CurrentTime, archerDoubleShot, TopTransformsFlipped[3], DropLeave));
        CurrentTime += 1f;
        Minions.Add(new MinionData(CurrentTime, archerSingleShot, TopTransforms[-3], DropLeave));
        CurrentTime += 1f;
        Minions.Add(new MinionData(CurrentTime, archerSingleShot, TopTransformsFlipped[1], DropLeave));
        CurrentTime += 1f;
        Minions.Add(new MinionData(CurrentTime, archerDoubleShot, TopTransforms[-1], DropLeave));
        CurrentTime += 1f;
        Minions.Add(new MinionData(CurrentTime, archerDoubleShot, TopTransformsFlipped[4], DropLeave));
        CurrentTime += 0.75f;
        Minions.Add(new MinionData(CurrentTime, archerSingleShot, TopTransforms[-4], DropLeave));
        CurrentTime += 0.75f;
        Minions.Add(new MinionData(CurrentTime, archerSingleShot, TopTransformsFlipped[2], DropLeave));
        CurrentTime += 0.75f;
        Minions.Add(new MinionData(CurrentTime, archerDoubleShot, TopTransforms[-2], DropLeave));
        CurrentTime += 0.75f;
        Minions.Add(new MinionData(CurrentTime, archerDoubleShot, TopTransformsFlipped[3], DropLeave));
        CurrentTime += 0.5f;
        Minions.Add(new MinionData(CurrentTime, archerSingleShot, TopTransforms[-1], DropLeave));
        CurrentTime += 0.5f;
        Minions.Add(new MinionData(CurrentTime, archerSingleShot, TopTransformsFlipped[4], DropLeave));
        CurrentTime += 0.5f;
        Minions.Add(new MinionData(CurrentTime, archerDoubleShot, TopTransforms[-2], DropLeave));
        CurrentTime += 0.5f;
        Minions.Add(new MinionData(CurrentTime, archerDoubleShot, TopTransforms[0], DropLeave));
        CurrentTime += 0.25f;
        Minions.Add(new MinionData(CurrentTime, archerSingleShot, TopTransforms[-3], DropLeave));
        CurrentTime += 0.25f;
        Minions.Add(new MinionData(CurrentTime, archerSingleShot, TopTransformsFlipped[3], DropLeave));
        CurrentTime += 0.25f;
        Minions.Add(new MinionData(CurrentTime, archerDoubleShot, TopTransforms[-1], DropLeave));
        CurrentTime += 0.25f;
        Minions.Add(new MinionData(CurrentTime, archerDoubleShot, TopTransformsFlipped[1], DropLeave));

        // 3
        CurrentTime += 5.5f;

        for (var i = 0; i < 10; ++i)
        {
            Minions.Add(new MinionData(CurrentTime, basicShoot, TopTransforms[0], HSlide1));
            Minions.Add(new MinionData(CurrentTime, basicShoot, TopTransformsFlipped[0], HSlide1));

            CurrentTime += 0.25f;
        }

        Minions.Add(new MinionData(CurrentTime, eliteArcherShooting, TopTransforms[0], VDangle));


        for (var i = 0; i < 15; ++i)
        {
            Minions.Add(new MinionData(CurrentTime, basicShoot, TopTransforms[0], HSlide1));
            Minions.Add(new MinionData(CurrentTime, basicShoot, TopTransformsFlipped[0], HSlide1));

            CurrentTime += 0.25f;
        }

        // Boss 1
        CurrentTime += 5f;

        Bosses.Add(new BossData(CurrentTime, boss1, new Vector2(0f, GameData.ScreenRect.yMax + 5f)));

        // 5
        CurrentTime += 5f;

        for (var i = 0; i < 5; ++i)
        {
            Minions.Add(new MinionData(CurrentTime, basicShoot, TopTransforms[-3], VSlide1));

            CurrentTime += 0.25f;
        }

        for (var i = 0; i < 5; ++i)
        {
            Minions.Add(new MinionData(CurrentTime, basicShoot, TopTransforms[-3], VSlide1));
            Minions.Add(new MinionData(CurrentTime, basicShoot, TopTransformsFlipped[3], VSlide1));

            CurrentTime += 0.25f;
        }

        for (var i = 0; i < 5; ++i)
        {
            Minions.Add(new MinionData(CurrentTime, basicShoot, TopTransformsFlipped[3], VSlide1));
            Minions.Add(new MinionData(CurrentTime, basicShoot, TopTransforms[0], HSlide1));

            CurrentTime += 0.25f;
        }

        for (var i = 0; i < 5; ++i)
        {
            Minions.Add(new MinionData(CurrentTime, basicShoot, TopTransforms[0], HSlide1));
            Minions.Add(new MinionData(CurrentTime, basicShoot, TopTransformsFlipped[0], HSlide1));

            CurrentTime += 0.25f;
        }

        // 6
        CurrentTime += 4f;

        for (var i = 0; i < 5; ++i)
        {
            Minions.Add(new MinionData(CurrentTime, basicShoot, TopTransforms[0], HSlide1));
            Minions.Add(new MinionData(CurrentTime, basicShoot, TopTransformsFlipped[0], HSlide1));

            CurrentTime += 0.25f;
        }

        Minions.Add(new MinionData(CurrentTime, archerDoubleShot, TopTransforms[-3], DropLeave));
        Minions.Add(new MinionData(CurrentTime, archerDoubleShot, TopTransformsFlipped[3], DropLeave));

        for (var i = 0; i < 5; ++i)
        {
            Minions.Add(new MinionData(CurrentTime, basicShoot, TopTransforms[0], HSlide1));
            Minions.Add(new MinionData(CurrentTime, basicShoot, TopTransformsFlipped[0], HSlide1));

            CurrentTime += 0.25f;
        }

        Minions.Add(new MinionData(CurrentTime, archerDoubleShot, TopTransforms[-4], DropLeave));
        Minions.Add(new MinionData(CurrentTime, archerDoubleShot, TopTransformsFlipped[4], DropLeave));

        for (var i = 0; i < 10; ++i)
        {
            Minions.Add(new MinionData(CurrentTime, basicShoot, TopTransforms[0], HSlide1));
            Minions.Add(new MinionData(CurrentTime, basicShoot, TopTransformsFlipped[0], HSlide1));

            CurrentTime += 0.25f;
        }

        // 7
        CurrentTime += 7f;

        Minions.Add(new MinionData(CurrentTime, archerDoubleShot, TopTransformsFlipped[3], DropLeave));
        CurrentTime += 1f;
        Minions.Add(new MinionData(CurrentTime, archerSingleShot, TopTransforms[-3], DropLeave));
        CurrentTime += 1f;
        Minions.Add(new MinionData(CurrentTime, archerSingleShot, TopTransformsFlipped[1], DropLeave));
        CurrentTime += 1f;
        Minions.Add(new MinionData(CurrentTime, archerDoubleShot, TopTransforms[-1], DropLeave));
        CurrentTime += 1f;
        Minions.Add(new MinionData(CurrentTime, archerDoubleShot, TopTransformsFlipped[4], DropLeave));
        CurrentTime += 0.75f;
        Minions.Add(new MinionData(CurrentTime, archerSingleShot, TopTransforms[-4], DropLeave));
        CurrentTime += 0.75f;
        Minions.Add(new MinionData(CurrentTime, archerSingleShot, TopTransformsFlipped[2], DropLeave));
        CurrentTime += 0.75f;
        Minions.Add(new MinionData(CurrentTime, archerDoubleShot, TopTransforms[-2], DropLeave));
        CurrentTime += 0.75f;
        Minions.Add(new MinionData(CurrentTime, archerDoubleShot, TopTransformsFlipped[3], DropLeave));
        CurrentTime += 0.5f;
        Minions.Add(new MinionData(CurrentTime, archerSingleShot, TopTransforms[-1], DropLeave));
        CurrentTime += 0.5f;
        Minions.Add(new MinionData(CurrentTime, archerSingleShot, TopTransformsFlipped[4], DropLeave));
        CurrentTime += 0.5f;
        Minions.Add(new MinionData(CurrentTime, archerDoubleShot, TopTransforms[-2], DropLeave));
        CurrentTime += 0.5f;
        Minions.Add(new MinionData(CurrentTime, eliteArcherDoubleShot, TopTransforms[0], DropLeave));
        CurrentTime += 0.25f;
        Minions.Add(new MinionData(CurrentTime, archerSingleShot, TopTransforms[-3], DropLeave));
        CurrentTime += 0.25f;
        Minions.Add(new MinionData(CurrentTime, archerSingleShot, TopTransformsFlipped[3], DropLeave));
        CurrentTime += 0.25f;
        Minions.Add(new MinionData(CurrentTime, eliteArcherDoubleShot, TopTransforms[-1], DropLeave));
        CurrentTime += 0.25f;
        Minions.Add(new MinionData(CurrentTime, eliteArcherDoubleShot, TopTransformsFlipped[1], DropLeave));

        CurrentTime += 7f;

        Bosses.Add(new BossData(CurrentTime, boss2, new Vector2(0f, GameData.ScreenRect.yMax + 5f)));



        Debug.Log(CurrentTime + " " + Minions.Count);
    }
}