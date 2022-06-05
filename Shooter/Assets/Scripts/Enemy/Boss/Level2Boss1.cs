using UnityEngine;

public class Level2Boss1 : Boss
{
    private bool HasInitiatedDialogue { get; set; }

    protected override void Awake()
    {
        base.Awake();

        Phases = new PhaseBehaviour[] {Phase1, Phase2, Phase3, Phase4, Phase5, Phase6, Phase7};
    }

    private bool Phase1()
    {
        ResetMovement(transform.position, new Vector2(0.0f, GameData.ScreenRect.yMax - 10.0f), 1.0f, 0.0f);

        return true;
    }

    private bool Phase2()
    {
        if (!BossMovement.IsFinished(GameState.IsRewinding)) return false;
        
        if (!HasInitiatedDialogue)
        {
            UIManager.AddDialogue("Boss", "Hi");
            UIManager.AddDialogue("You", "What's up");
            //UIManager.StartDialogue();
            
            HasInitiatedDialogue = true;
        }
        else
        {
            UIManager.AddDialogue("You", "We talked already");
            UIManager.StartDialogue();
        }

        ShootBehaviours[0].IsDisabled = false;

        return true;
    }

    private bool Phase3()
    {
        GameState.IsBossActive = true;
        
        if (Health <= MaxHealth * 0.75f)
        {
            ResetMovement(transform.position, new Vector2(0f, 5f), 2f, 0f);

            return true;
        }

        if (false)//BossMovement.IsFinished(GameState.IsRewinding))
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