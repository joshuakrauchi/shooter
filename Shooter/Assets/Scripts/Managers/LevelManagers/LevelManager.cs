using System.Collections.Generic;
using UnityEngine;

public abstract class LevelManager : MonoBehaviour
{
    [field: Header("ScriptableObjects")]
    [field: SerializeField]
    protected GameData GameData { get; private set; }

    [field: SerializeField] protected ProjectileManager ProjectileManager { get; private set; }

    private List<MinionData> Minions { get; set; }
    private List<BossData> Bosses { get; set; }
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
        Minions = new List<MinionData>();
        Bosses = new List<BossData>();

        CurrentTime = 0.1f;

        TopTransforms = new Dictionary<int, Transform>();
        TopTransformsFlipped = new Dictionary<int, Transform>();

        Origin = new GameObject("SpawnTransforms").transform;

        Quaternion flippedRotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);

        for (var i = -5; i <= 5; ++i)
        {
            Vector3 transformPosition = new(5.0f * i, GameData.ScreenRect.yMax + Padding, 0.0f);

            Transform newTransform = new GameObject($"SpawnTransform {i}").transform;
            newTransform.position = transformPosition;
            newTransform.rotation = Quaternion.identity;
            newTransform.SetParent(Origin);
            TopTransforms[i] = newTransform;

            Transform newTransformFlipped = new GameObject($"SpawnTransform {i} Flipped").transform;
            newTransformFlipped.position = transformPosition;
            newTransformFlipped.rotation = flippedRotation;
            newTransformFlipped.SetParent(Origin);
            TopTransformsFlipped[i] = newTransformFlipped;
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
            MinionData minionData = Minions[MinionIndex];

            NPCCreator.CreateMinion(minionData);
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

    protected void AddMinion(GameObject minionPrefab, Transform parentTransform, string animationName) => Minions.Add(new MinionData(CurrentTime, minionPrefab, parentTransform, animationName));

    protected void AddBoss(GameObject bossPrefab, Vector2 spawnPosition) => Bosses.Add(new BossData(CurrentTime, bossPrefab, spawnPosition));
}