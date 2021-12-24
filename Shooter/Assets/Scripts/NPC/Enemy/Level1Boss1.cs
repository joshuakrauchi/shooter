using System;
using UnityEngine;

public class Level1Boss1 : Boss
{
    protected override void Awake()
    {
        base.Awake();

        Phases.Add(new Tuple<float, PhaseBehaviour>(Health, Phase1));
        Phases.Add(new Tuple<float, PhaseBehaviour>(Health, Phase2));
    }

    private void Phase1()
    {
        if (IsNewPhase)
        {
            BossMovement.ResetMovement(transform.position, new Vector2(0f, GameManager.Top - 10f), 2f, false);
            IsNewPhase = false;
        }

        if (BossMovement.IsFinished())
        {
            IsActive = true;
        }
    }

    private void Phase2()
    {
        if (IsNewPhase)
        {
            BossMovement.ResetMovement(transform.position, BossMovement.GetRandomPosition(), 1f, false);

            ShootBehaviour = new ShootSuccessiveHoming(0, new Timer(0.5f), ProjectileDefinitions[0], 5, 10f, 5f, 0.25f);
            IsNewPhase = false;
        }

        if (BossMovement.IsFinished())
        {
            BossMovement.ResetMovement(transform.position, BossMovement.GetRandomPosition(), 1f, false);
        }
    }
}