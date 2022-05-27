using System;
using System.Collections.Generic;
using UnityEngine;

public class Level1Boss1 : Boss
{
    private bool _initiatedDialogue;

    [SerializeField] private GameObject slowArrow;
    [SerializeField] private GameObject fastArrow;
    [SerializeField] private GameObject bigArrow;

    /*private ProjectileDefinition slowingProjectileStraight;
    private ProjectileDefinition smallProjectileStraight;
    private ProjectileDefinition boss2SmallProjectileRain;
    private ProjectileDefinition boss2BigProjectileStraight;
    private ProjectileDefinition boss2BigProjectileRain;*/

    protected override void Awake()
    {
        base.Awake();

/*        slowingProjectileStraight = new ProjectileDefinition(slowArrow, new MoveBehaviour[] {new MoveStraight(1.0f, 0.1f, -0.1f)});
        smallProjectileStraight = new ProjectileDefinition(fastArrow, new MoveBehaviour[] {new MoveStraight(1.0f)});
        boss2SmallProjectileRain = new ProjectileDefinition(fastArrow, new MoveBehaviour[] {new MoveStraight(1.0f), new MoveStraight(2.0f, -90.0f)});
        boss2BigProjectileStraight = new ProjectileDefinition(bigArrow, new MoveBehaviour[] {new MoveStraight(1.0f)});
*/
        Phases = new PhaseBehaviour[] {Phase1, Phase2, Phase3, Phase4, Phase5, Phase6, Phase7};
    }

    private bool Phase1()
    {
        ResetMovement(transform.position, new Vector2(0f, GameData.ScreenRect.yMax - 10f), 2f, 0f);

        return true;
    }

    private bool Phase2()
    {
        if (BossMovement.IsFinished(GameState.IsRewinding))
        {
            if (!_initiatedDialogue)
            {
                UIManager.StartDialogue(new[] {Tuple.Create("dude", "fite me bro"), Tuple.Create("you", "fine lol")});
                _initiatedDialogue = true;
            }
            else
            {
                UIManager.StartDialogue(new[] {Tuple.Create("dude", "fite me bro"), Tuple.Create("you", "omg we been through this b4, just die already")});
            }

            //ShootBehaviours = new List<ShootBehaviour> {new ShootSuccessiveHoming(GameData.Player.gameObject, 0, new Timer(0.5f), boss2BigProjectileStraight, new Timer(0.25f), 5, 20f, 5f)};
            PhaseTimer = new Timer(10.0f);

            return true;
        }

        return false;
    }

    private bool Phase3()
    {
        GameState.IsBossActive = true;

        if (Health <= MaxHealth * 0.75f || PhaseTimer.IsFinished(false))
        {
            ResetMovement(transform.position, new Vector2(0.0f, GameData.ScreenRect.yMax - 10.0f), 1.0f, 0.0f);
            //ShootBehaviours.Clear();
            PhaseTimer = new Timer(10.0f);

            return true;
        }

        if (BossMovement.IsFinished(GameState.IsRewinding))
        {
            Rect screenRect = GameData.ScreenRect;
            ResetMovement(transform.position, BossMovement.GetRandomPosition(screenRect.xMin, screenRect.xMax, 0.0f, screenRect.yMax), 1.0f, 2.0f);
        }

        return false;
    }

    private bool Phase4()
    {
        if (BossMovement.IsFinished(GameState.IsRewinding))
        {
            ResetMovement(transform.position, transform.position, 0.0f, 0.0f);
            
            /*ShootBehaviours.AddRange(new ShootBehaviour[]
            {
                new ShootSuccessiveHoming(GameData.Player.gameObject, 0, new Timer(0.5f), boss2BigProjectileStraight, new Timer(0.25f), 5, 20f, 5f),
                new ShootHoming(GameData.Player.gameObject, 0, new Timer(0.25f), smallProjectileStraight, new Timer(0.25f), 2, 10, 10f, 2f, 0f)
            });*/

            return true;
        }

        return false;
    }

    private bool Phase5()
    {
        if (Health <= MaxHealth * 0.5f || PhaseTimer.IsFinished(false))
        {
            return true;
        }

        return false;
    }

    private bool Phase6()
    {
        UIManager.StartDialogue(new[] {Tuple.Create("dude", "dang bro ur stronk"), Tuple.Create("dude", "bye!!")});
        GameState.IsBossActive = false;

        ResetMovement(transform.position, new Vector2(0, GameData.ScreenRect.yMax + 7f), 2f, 0f);

        return true;
    }

    private bool Phase7()
    {
        return BossMovement.IsFinished(GameState.IsRewinding);
    }
}