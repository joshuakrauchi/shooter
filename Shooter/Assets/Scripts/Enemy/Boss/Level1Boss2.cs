using System;
using System.Collections.Generic;
using UnityEngine;

public class Level1Boss2 : Boss
{
    private bool _initiatedDialogue;

    protected override void Awake()
    {
        base.Awake();

        Phases.AddRange(new PhaseBehaviour[] {Phase1, Phase2, Phase3, Phase4, Phase5, Phase6, Phase7});
    }

    private bool Phase1()
    {
        BossMovement.ResetMovement(transform.position, new Vector2(0f, GameManager.Top - 10f), 2f, 0f);

        return true;
    }

    private bool Phase2()
    {
        if (BossMovement.IsFinished())
        {
            if (!_initiatedDialogue)
            {
                UIManager.Instance.StartDialogue(new[] {Tuple.Create("Boss", "im gonna kill u"), Tuple.Create("you", "mhm")});
                _initiatedDialogue = true;
            }

            ShootBehaviours = new List<ShootBehaviour>
            {
                new ShootHoming(0, new LockedTimer(0.1f), ProjectileDefinitions[0], 1, 0f, 5f),
                new ShootSuccessiveHoming(0, new LockedTimer(0.5f), ProjectileDefinitions[1], new LockedTimer(0.25f), 5, 15f, 5f)
            };

            IsActive = true;

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

        if (BossMovement.IsFinished())
        {
            BossMovement.ResetMovement(transform.position, BossMovement.GetRandomPosition(), 2f, 3f);
        }

        return false;
    }

    private bool Phase4()
    {
        if (BossMovement.IsFinished())
        {
            ShootBehaviours = new List<ShootBehaviour>
            {
                new ShootNormal(0, new LockedTimer(0.25f), ProjectileDefinitions[2], 5, 90f, 10f, 10f),
                new ShootNormal(0, new LockedTimer(0.5f), ProjectileDefinitions[3], 7, 90f, 12f, 2f)
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
                new ShootRandom(0, new LockedTimer(0.1f), ProjectileDefinitions[4], 10),
                new ShootSpiral(0, new LockedTimer(0.3f), ProjectileDefinitions[1], 1, 20f, 0f)
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
                new ShootRandom(0, new LockedTimer(0.1f), ProjectileDefinitions[1], 5),
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