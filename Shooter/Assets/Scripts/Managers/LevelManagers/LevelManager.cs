using System.Collections.Generic;
using UnityEngine;

public abstract class LevelManager : MonoBehaviour
{
    [field: SerializeField] protected GameData GameData { get; private set; }

    protected List<MinionSpawn> Minions { get; private set; }
    protected List<BossSpawn> Bosses { get; private set; }
    private int MinionIndex { get; set; }
    private int BossIndex { get; set; }

    protected float CurrentTime { get; set; }
    protected Transform Origin { get; private set; }
    protected Dictionary<int, Transform> TopTransforms { get; private set; }
    protected Dictionary<int, Transform> TopTransformsFlipped { get; private set; }

    private const float Padding = 2.0f;

    protected virtual void Awake()
    {
        GameData.CurrentLevelManager = this;
        Minions = new List<MinionSpawn>();
        Bosses = new List<BossSpawn>();

        CurrentTime = 2.5f;

        TopTransforms = new Dictionary<int, Transform>();
        TopTransformsFlipped = new Dictionary<int, Transform>();
        
        Origin = new GameObject("SpawnTransforms").transform;
        
        for (var i = -5; i <= 5; ++i)
        {
            TopTransforms[i] = Instantiate(Origin, new Vector3(5.0f * i, GameData.ScreenRect.yMax + Padding, 0.0f), Quaternion.identity).transform;
            TopTransformsFlipped[i] = Instantiate(Origin, new Vector3(5.0f * i, GameData.ScreenRect.yMax + Padding, 0.0f), Quaternion.Euler(0.0f, 180.0f, 0.0f)).transform;
        }
    }

    public void UpdateEnemyCreation()
    {
        while (Minions.Count > 0 && MinionIndex > 0 && Minions[MinionIndex - 1].CreationTime > GameData.LevelTime)
        {
            --MinionIndex;
        }

        while (MinionIndex < Minions.Count && Minions[MinionIndex].CreationTime <= GameData.LevelTime)
        {
            MinionSpawn minionSpawn = Minions[MinionIndex];
            
            NPCCreator.CreateMinion(minionSpawn);
            ++MinionIndex;
        }
        
        while (Bosses.Count > 0 && BossIndex > 0 && Bosses[BossIndex - 1].CreationTime > GameData.LevelTime)
        {
            --BossIndex;
        }

        while (BossIndex < Bosses.Count && Bosses[BossIndex].CreationTime <= GameData.LevelTime)
        {
            NPCCreator.CreateBoss(Bosses[BossIndex]);
            ++BossIndex;
        }
    }

    public void AddMinion(MinionDefinition minionDefinition, Transform parentTransform, string animationName)
    {
        Minions.Add(new MinionSpawn(CurrentTime, minionDefinition, parentTransform, animationName));
    }
}