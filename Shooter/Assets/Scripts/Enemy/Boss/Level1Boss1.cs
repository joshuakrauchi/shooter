using System;
using System.Collections.Generic;
using UnityEngine;

public class Level1Boss1 : Boss
{
    private bool _initiatedDialogue;

    [SerializeField] private GameObject slowArrow;
    [SerializeField] private GameObject fastArrow;
    [SerializeField] private GameObject bigArrow;

    private ProjectileDefinition slowingProjectileStraight;
    private ProjectileDefinition smallProjectileStraight;
    private ProjectileDefinition boss2SmallProjectileRain;
    private ProjectileDefinition boss2BigProjectileStraight;
    private ProjectileDefinition boss2BigProjectileRain;

    protected override void Awake()
    {
        base.Awake();

        slowingProjectileStraight = new ProjectileDefinition(slowArrow, new MoveBehaviour[] {new MoveStraight(1.0f, 0.1f, -0.1f)});
        smallProjectileStraight = new ProjectileDefinition(fastArrow, new MoveBehaviour[] {new MoveStraight(1.0f)});
        boss2SmallProjectileRain = new ProjectileDefinition(fastArrow, new MoveBehaviour[] {new MoveStraight(1.0f), new MoveStraight(2.0f, -90f)});
        boss2BigProjectileStraight = new ProjectileDefinition(bigArrow, new MoveBehaviour[] {new MoveStraight(1.0f)});

        Phases = new PhaseBehaviour[] {Phase1, Phase2, Phase3, Phase4, Phase5, Phase6, Phase7};
    }

    private bool Phase1()
    {
        BossMovement.ResetMovement(transform.position, new Vector2(0f, GameManager.ScreenRect.yMax - 10f), 2f, 0f);

        return true;
    }

    private bool Phase2()
    {
        if (BossMovement.IsFinished(gameState.IsRewinding))
        {
            if (!_initiatedDialogue)
            {
                GameManager.UIManager.StartDialogue(new[] {Tuple.Create("dude", "fite me bro"), Tuple.Create("you", "fine lol")});
                _initiatedDialogue = true;
            }
            else
            {
                GameManager.UIManager.StartDialogue(new[] {Tuple.Create("dude", "fite me bro"), Tuple.Create("you", "omg we been through this b4, just die already")});
            }

            ShootBehaviours = new List<ShootBehaviour> {new ShootSuccessiveHoming(0, new LockedTimer(0.5f), boss2BigProjectileStraight, new LockedTimer(0.25f), 5, 20f, 5f)};
            PhaseTimer = new LockedTimer(10f);

            ActivateBoss();
            return true;
        }

        return false;
    }

    private bool Phase3()
    {
        Debug.Log(PhaseTimer.ElapsedTime);
        if (Health <= MaxHealth * 0.75f || PhaseTimer.IsFinished(false))
        {
            BossMovement.ResetMovement(transform.position, new Vector2(0, GameManager.ScreenRect.yMax - 10f), 1f, 0f);
            ShootBehaviours.Clear();
            PhaseTimer = new LockedTimer(10f);

            return true;
        }

        if (BossMovement.IsFinished(gameState.IsRewinding))
        {
            BossMovement.ResetMovement(transform.position, BossMovement.GetRandomPosition(), 1f, 2f);
        }

        return false;
    }

    private bool Phase4()
    {
        if (BossMovement.IsFinished(gameState.IsRewinding))
        {
            ShootBehaviours.AddRange(new ShootBehaviour[]
            {
                new ShootSuccessiveHoming(0, new LockedTimer(0.5f), boss2BigProjectileStraight, new LockedTimer(0.25f), 5, 20f, 5f),
                new ShootHoming(0, new LockedTimer(0.25f), smallProjectileStraight, new LockedTimer(0.25f), 2, 10, 10f, 2f, 0f)
            });

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
        GameManager.UIManager.StartDialogue(new[] {Tuple.Create("dude", "dang bro ur stronk"), Tuple.Create("dude", "bye!!")});

        BossMovement.ResetMovement(transform.position, new Vector2(0, GameManager.ScreenRect.yMax + 7f), 2f, 0f);

        return true;
    }

    private bool Phase7()
    {
        if (BossMovement.IsFinished(gameState.IsRewinding))
        {
            Disable();
        }

        return false;
    }
}