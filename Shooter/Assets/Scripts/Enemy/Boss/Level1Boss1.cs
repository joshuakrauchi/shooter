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
            BossMovement.ResetMovement(transform.position, new Vector2(0f, GameManager.Top - 10f), 2f, 0f, false);
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
            BossMovement.ResetMovement(transform.position, BossMovement.GetRandomPosition(), 1f, 2f, false);

            ShootBehaviours = new List<ShootBehaviour> {new ShootSuccessiveHoming(0, new Timer(0.5f), ProjectileDefinitions[0], 5, 20f, 5f, 0.25f)};
            IsNewPhase = false;
        }

        if (BossMovement.IsFinished())
        {
            BossMovement.ResetMovement(transform.position, BossMovement.GetRandomPosition(), 1f, 2f, false);
        }
    }

    private void Phase3()
    {
        if (IsNewPhase)
        {
            BossMovement.ResetMovement(transform.position, new Vector2(GameManager.Right / 2f, GameManager.Top - 10f), 1f, 2f, true);

            ShootBehaviours = new List<ShootBehaviour>
            {
                new ShootSuccessiveHoming(0, new Timer(0.5f), ProjectileDefinitions[0], 5, 20f, 5f, 0.25f),
                new ShootHoming(0, new Timer(0.25f), ProjectileDefinitions[1], 10, 10f, 10f)
            };

            IsNewPhase = false;
        }
    }
}