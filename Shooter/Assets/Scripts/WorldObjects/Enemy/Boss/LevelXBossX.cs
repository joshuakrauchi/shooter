using UnityEngine;

public class LevelXBossX : Boss
{
    protected override void Awake()
    {
        base.Awake();

        Phases = new PhaseBehaviour[] {Phase1, Phase2};
    }

    private bool Phase1()
    {
        transform.position = new Vector3();

        return true;
    }

    private bool Phase2()
    {
        GameState.IsBossActive = true;
        
        return false;
    }
}