using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelXBossX : Boss
{
    protected override void Awake()
    {
        base.Awake();

        Phases.AddRange(new PhaseBehaviour[] {Phase1, Phase2, Phase3, Phase4});
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
            //UIManager.Instance.StartDialogue(new[] {Tuple.Create("dude", "fite me bro"), Tuple.Create("you", "ok lol")});

            ShootBehaviours = new List<ShootBehaviour> {new ShootOutwardSpiral(1, new LockedTimer(0.5f), ProjectileDefinitions[0])};
            IsActive = true;

            return true;
        }

        return false;
    }

    private bool Phase3()
    {
        if (Health <= MaxHealth * 0.9f)
        {
            BossMovement.ResetMovement(transform.position, new Vector2(0, GameManager.Top - 10f), 1f, 0f);
            ShootBehaviours = new List<ShootBehaviour>();

            return true;
        }

        return false;
    }

    private bool Phase4()
    {
        if (BossMovement.IsFinished())
        {
            ShootBehaviours = new List<ShootBehaviour>
            {
                new ShootSuccessiveHoming(0, new LockedTimer(0.5f), ProjectileDefinitions[0], 5, 20f, 5f, 0.25f),
                new ShootHoming(0, new LockedTimer(0.25f), ProjectileDefinitions[1], 10, 10f, 10f)
            };

            return true;
        }

        return false;
    }
}