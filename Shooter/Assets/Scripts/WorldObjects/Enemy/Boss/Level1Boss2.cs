using System;
using System.Collections.Generic;
using UnityEngine;

public class Level1Boss2 : Boss
{
    [SerializeField] private GameObject slowArrow;
    [SerializeField] private GameObject fastArrow;
    [SerializeField] private GameObject bigArrow;

    /*private ProjectileDefinition slowingProjectileStraight;
    private ProjectileDefinition smallProjectileStraight;
    private ProjectileDefinition boss2SmallProjectileRain;
    private ProjectileDefinition boss2BigProjectileStraight;
    private ProjectileDefinition boss2BigProjectileRain;*/

    private bool _initiatedDialogue;

    protected override void Awake()
    {
        base.Awake();

/*        slowingProjectileStraight = new ProjectileDefinition(slowArrow, new MoveBehaviour[] {new MoveStraight(0.0f, 0.1f, -0.1f)});
        smallProjectileStraight = new ProjectileDefinition(fastArrow, new MoveBehaviour[] {new MoveStraight(0.0f)});
        boss2SmallProjectileRain = new ProjectileDefinition(fastArrow, new MoveBehaviour[] {new MoveStraight(0.0f), new MoveStraight(2.0f, -90f)});
        boss2BigProjectileStraight = new ProjectileDefinition(bigArrow, new MoveBehaviour[] {new MoveStraight(0.0f)});
        boss2BigProjectileRain = new ProjectileDefinition(bigArrow, new MoveBehaviour[] {new MoveStraight(0.0f), new MoveStraight(2.0f, -90f)});*/

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
                UIManager.StartDialogue();
                _initiatedDialogue = true;
            }



            return true;
        }

        return false;
    }

    private bool Phase3()
    {
        if (Health <= MaxHealth * 0.75f)
        {
            ResetMovement(transform.position, new Vector2(0f, 5f), 2f, 0f);

            return true;
        }

        if (BossMovement.IsFinished(GameState.IsRewinding))
        {
            Rect screenRect = GameData.ScreenRect;
            ResetMovement(transform.position, BossMovement.GetRandomPosition(screenRect.xMin, screenRect.xMax, 0.0f, screenRect.yMax), 2.0f, 3.0f);
        }

        return false;
    }

    private bool Phase4()
    {
        if (BossMovement.IsFinished(GameState.IsRewinding))
        {
            /*ShootBehaviours = new List<ShootBehaviour>
            {
                new ShootNormal(0, new Timer(0.25f), fastArrow, 5, 90f, 10f, 10f),
                new ShootNormal(0, new Timer(0.5f), bigArrow, 7, 90f, 12f, 2f)
            };*/

            return true;
        }

        return false;
    }

    private bool Phase5()
    {
        if (Health <= MaxHealth * 0.5f)
        {
            /*ShootBehaviours = new List<ShootBehaviour>
            {
                new ShootRandom(0, new Timer(0.1f), slowArrow, 10),
                new ShootSpiral(0, new Timer(0.3f), bigArrow, 1, 20f, 0f)
            };*/

            return true;
        }

        return false;
    }

    private bool Phase6()
    {
        if (Health <= MaxHealth * 0.25f)
        {
            /*ShootBehaviours = new List<ShootBehaviour>
            {
                new ShootRandom(0, new Timer(0.1f), bigArrow, 5),
            };*/

            return true;
        }

        return false;
    }

    private bool Phase7()
    {
        return false;
    }
}