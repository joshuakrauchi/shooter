using System;
using System.Collections.Generic;
using UnityEngine;

public class Level1Boss2 : Boss
{
    [SerializeField] private GameObject slowArrow;
    [SerializeField] private GameObject fastArrow;
    [SerializeField] private GameObject bigArrow;

    private ProjectileDefinition slowingProjectileStraight;
    private ProjectileDefinition smallProjectileStraight;
    private ProjectileDefinition boss2SmallProjectileRain;
    private ProjectileDefinition boss2BigProjectileStraight;
    private ProjectileDefinition boss2BigProjectileRain;

    private bool _initiatedDialogue;

    protected override void Awake()
    {
        base.Awake();

        slowingProjectileStraight = new ProjectileDefinition(slowArrow, new[] {new MovePair(0f, new MoveStraight(0.1f, -0.1f))});
        smallProjectileStraight = new ProjectileDefinition(fastArrow, new[] {new MovePair(0f, new MoveStraight())});
        boss2SmallProjectileRain = new ProjectileDefinition(fastArrow, new[] {new MovePair(0f, new MoveStraight()), new MovePair(2f, new MoveStraight(-90f))});
        boss2BigProjectileStraight = new ProjectileDefinition(bigArrow, new[] {new MovePair(0f, new MoveStraight())});
        boss2BigProjectileRain = new ProjectileDefinition(bigArrow, new[] {new MovePair(0f, new MoveStraight()), new MovePair(2f, new MoveStraight(-90f))});


        Phases = new PhaseBehaviour[] {Phase1, Phase2, Phase3, Phase4, Phase5, Phase6, Phase7};
    }

    private bool Phase1()
    {
        BossMovement.ResetMovement(transform.position, new Vector2(0f, GameManager.ScreenRect.yMax - 10f), 2f, 0f);

        return true;
    }

    private bool Phase2()
    {
        if (BossMovement.IsFinished(GameState.IsRewinding))
        {
            if (!_initiatedDialogue)
            {
                GameManager.UIManager.StartDialogue(new[] {Tuple.Create("Boss", "im gonna kill u"), Tuple.Create("you", "mhm")});
                _initiatedDialogue = true;
            }

            ShootBehaviours = new List<ShootBehaviour>
            {
                //new ShootHoming(0, new LockedTimer(0.1f), smallProjectileStraight, 1, 0f, 5f),
                new ShootSuccessiveHoming(0, new LockedTimer(0.5f), boss2BigProjectileStraight, new LockedTimer(0.25f), 5, 15f, 5f)
            };

            ActivateBoss();

            return true;
        }

        return false;
    }

    private bool Phase3()
    {
        if (Health <= MaxHealth * 0.75f)
        {
            BossMovement.ResetMovement(transform.position, new Vector2(0f, 5f), 2f, 0f);

            return true;
        }

        if (BossMovement.IsFinished(GameState.IsRewinding))
        {
            BossMovement.ResetMovement(transform.position, BossMovement.GetRandomPosition(), 2f, 3f);
        }

        return false;
    }

    private bool Phase4()
    {
        if (BossMovement.IsFinished(GameState.IsRewinding))
        {
            ShootBehaviours = new List<ShootBehaviour>
            {
                new ShootNormal(0, new LockedTimer(0.25f), boss2SmallProjectileRain, 5, 90f, 10f, 10f),
                new ShootNormal(0, new LockedTimer(0.5f), boss2BigProjectileRain, 7, 90f, 12f, 2f)
            };

            return true;
        }

        return false;
    }

    private bool Phase5()
    {
        if (Health <= MaxHealth * 0.5f)
        {
            ShootBehaviours = new List<ShootBehaviour>
            {
                new ShootRandom(0, new LockedTimer(0.1f), slowingProjectileStraight, 10),
                new ShootSpiral(0, new LockedTimer(0.3f), boss2BigProjectileStraight, 1, 20f, 0f)
            };

            return true;
        }

        return false;
    }

    private bool Phase6()
    {
        if (Health <= MaxHealth * 0.25f)
        {
            ShootBehaviours = new List<ShootBehaviour>
            {
                new ShootRandom(0, new LockedTimer(0.1f), boss2BigProjectileStraight, 5),
            };

            return true;
        }

        return false;
    }

    private bool Phase7()
    {
        return false;
    }
}