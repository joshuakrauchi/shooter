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
    protected Transform origin;
    protected Transform top;
    protected Transform topFlip;
    protected Transform topM5;
    protected Transform topM5Flip;
    protected Transform topM10;
    protected Transform topM10Flip;
    protected Transform topM15;
    protected Transform topM15Flip;
    protected Transform topM20;
    protected Transform topM20Flip;
    protected Transform topM25;
    protected Transform topM25Flip;
    protected Transform topP5;
    protected Transform topP5Flip;
    protected Transform topP10;
    protected Transform topP10Flip;
    protected Transform topP15;
    protected Transform topP15Flip;
    protected Transform topP20;
    protected Transform topP20Flip;
    protected Transform topP25;
    protected Transform topP25Flip;

    private const float Padding = 2f;

    protected virtual void Awake()
    {
        GameData.CurrentLevelManager = this;
        Minions = new List<MinionSpawn>();
        Bosses = new List<BossSpawn>();

        CurrentTime = 2.5f;

        origin = new GameObject("SpawnTransforms").transform;
        top = Instantiate(origin, new Vector3(0f, GameData.ScreenRect.yMax + Padding, 0f), Quaternion.identity).transform;
        topFlip = Instantiate(origin, new Vector3(0f, GameData.ScreenRect.yMax + Padding, 0f), new Quaternion(0f, 180f, 0f, 0f)).transform;
        topM5 = Instantiate(origin, new Vector3(-5, GameData.ScreenRect.yMax + Padding, 0f), Quaternion.identity).transform;
        topM5Flip = Instantiate(origin, new Vector3(-5, GameData.ScreenRect.yMax + Padding, 0f), new Quaternion(0f, 180f, 0f, 0f)).transform;
        topM10 = Instantiate(origin, new Vector3(-10, GameData.ScreenRect.yMax + Padding, 0f), Quaternion.identity).transform;
        topM10Flip = Instantiate(origin, new Vector3(-10, GameData.ScreenRect.yMax + Padding, 0f), new Quaternion(0f, 180f, 0f, 0f)).transform;
        topM15 = Instantiate(origin, new Vector3(-15, GameData.ScreenRect.yMax + Padding, 0f), Quaternion.identity).transform;
        topM15Flip = Instantiate(origin, new Vector3(-15, GameData.ScreenRect.yMax + Padding, 0f), new Quaternion(0f, 180f, 0f, 0f)).transform;
        topM20 = Instantiate(origin, new Vector3(-20, GameData.ScreenRect.yMax + Padding, 0f), Quaternion.identity).transform;
        topM20Flip = Instantiate(origin, new Vector3(-20, GameData.ScreenRect.yMax + Padding, 0f), new Quaternion(0f, 180f, 0f, 0f)).transform;
        topM25 = Instantiate(origin, new Vector3(-25, GameData.ScreenRect.yMax + Padding, 0f), Quaternion.identity).transform;
        topM25Flip = Instantiate(origin, new Vector3(-25, GameData.ScreenRect.yMax + Padding, 0f), new Quaternion(0f, 180f, 0f, 0f)).transform;
        topP5 = Instantiate(origin, new Vector3(5, GameData.ScreenRect.yMax + Padding, 0f), Quaternion.identity).transform;
        topP5Flip = Instantiate(origin, new Vector3(5, GameData.ScreenRect.yMax + Padding, 0f), new Quaternion(0f, 180f, 0f, 0f)).transform;
        topP10 = Instantiate(origin, new Vector3(10, GameData.ScreenRect.yMax + Padding, 0f), Quaternion.identity).transform;
        topP10Flip = Instantiate(origin, new Vector3(10, GameData.ScreenRect.yMax + Padding, 0f), new Quaternion(0f, 180f, 0f, 0f)).transform;
        topP15 = Instantiate(origin, new Vector3(15, GameData.ScreenRect.yMax + Padding, 0f), Quaternion.identity).transform;
        topP15Flip = Instantiate(origin, new Vector3(15, GameData.ScreenRect.yMax + Padding, 0f), new Quaternion(0f, 180f, 0f, 0f)).transform;
        topP20 = Instantiate(origin, new Vector3(20, GameData.ScreenRect.yMax + Padding, 0f), Quaternion.identity).transform;
        topP20Flip = Instantiate(origin, new Vector3(20, GameData.ScreenRect.yMax + Padding, 0f), new Quaternion(0f, 180f, 0f, 0f)).transform;
        topP25 = Instantiate(origin, new Vector3(25, GameData.ScreenRect.yMax + Padding, 0f), Quaternion.identity).transform;
        topP25Flip = Instantiate(origin, new Vector3(25, GameData.ScreenRect.yMax + Padding, 0f), new Quaternion(0f, 180f, 0f, 0f)).transform;
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
}