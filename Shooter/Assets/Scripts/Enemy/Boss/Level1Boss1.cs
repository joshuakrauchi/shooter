using System;
using System.Collections.Generic;
using UnityEngine;

public class Level1Boss1 : Boss
{
    protected override void Awake()
    {
        base.Awake();

        Phases.Add(new Tuple<float, PhaseBehaviour>(Health, Phase1));
        Phases.Add(new Tuple<float, PhaseBehaviour>(Health, Phase2));
        Phases.Add(new Tuple<float, PhaseBehaviour>(Health * 0.9f, Phase3));
    }

    private void Phase1()
    {
        if (IsNewPhase)
        {
            BossMovement.ResetMovement(transform.position, new Vector2(0f, GameManager.Top - 10f), 2f, 0f);
            IsNewPhase = false;
        }

        if (BossMovement.IsFinished())
        {
            UIManager.Instance.StartDialogue(new[] {Tuple.Create("dude", "fite me bro"), Tuple.Create("you", "fine lol")});
            IsActive = true;
        }
    }

    private void Phase2()
    {
        if (IsNewPhase)
        {
            BossMovement.ResetMovement(transform.position, BossMovement.GetRandomPosition(), 1f, 2f);

            ShootBehaviours = new List<ShootBehaviour> {new ShootSuccessiveHoming(0, new LockedTimer(0.5f), ProjectileDefinitions[0], 5, 20f, 5f, 0.25f)};
            IsNewPhase = false;
        }

        if (BossMovement.IsFinished())
        {
            BossMovement.ResetMovement(transform.position, BossMovement.GetRandomPosition(), 1f, 2f);
        }
    }

    private bool phase3marker;

    private void Phase3()
    {
        if (IsNewPhase)
        {
            phase3marker = false;
            ShootBehaviours = new List<ShootBehaviour>();

            if (!BossMovement.IsFinished()) return;

            BossMovement.ResetMovement(transform.position, new Vector2(0, GameManager.Top - 10f), 1f, 0f);


            IsNewPhase = false;
        }

        if (BossMovement.IsFinished() && !phase3marker)
        {
            ShootBehaviours = new List<ShootBehaviour>
            {
                new ShootSuccessiveHoming(0, new LockedTimer(0.5f), ProjectileDefinitions[0], 5, 20f, 5f, 0.25f),
                new ShootHoming(0, new LockedTimer(0.25f), ProjectileDefinitions[1], 10, 10f, 10f)
            };

            phase3marker = true;
        }
    }
}