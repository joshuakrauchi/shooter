using System.Collections.Generic;
using UnityEngine;

public abstract class LevelManager : MonoBehaviour
{
    public List<EnemySpawn> Enemies { get; private set; }
    public List<BossSpawn> Bosses { get; private set; }
    public int EnemyIndex { get; set; }

    protected float CurrentTime = 1f;
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
        GameManager.CurrentLevelManager = this;
        Enemies = new List<EnemySpawn>();
        Bosses = new List<BossSpawn>();

        origin = new GameObject("SpawnTransforms").transform;
        top = Instantiate(origin, new Vector3(0f, GameManager.ScreenRect.yMax + Padding, 0f), Quaternion.identity).transform;
        topFlip = Instantiate(origin, new Vector3(0f, GameManager.ScreenRect.yMax + Padding, 0f), new Quaternion(0f, 180f, 0f, 0f)).transform;
        topM5 = Instantiate(origin, new Vector3(-5, GameManager.ScreenRect.yMax + Padding, 0f), Quaternion.identity).transform;
        topM5Flip = Instantiate(origin, new Vector3(-5, GameManager.ScreenRect.yMax + Padding, 0f), new Quaternion(0f, 180f, 0f, 0f)).transform;
        topM10 = Instantiate(origin, new Vector3(-10, GameManager.ScreenRect.yMax + Padding, 0f), Quaternion.identity).transform;
        topM10Flip = Instantiate(origin, new Vector3(-10, GameManager.ScreenRect.yMax + Padding, 0f), new Quaternion(0f, 180f, 0f, 0f)).transform;
        topM15 = Instantiate(origin, new Vector3(-15, GameManager.ScreenRect.yMax + Padding, 0f), Quaternion.identity).transform;
        topM15Flip = Instantiate(origin, new Vector3(-15, GameManager.ScreenRect.yMax + Padding, 0f), new Quaternion(0f, 180f, 0f, 0f)).transform;
        topM20 = Instantiate(origin, new Vector3(-20, GameManager.ScreenRect.yMax + Padding, 0f), Quaternion.identity).transform;
        topM20Flip = Instantiate(origin, new Vector3(-20, GameManager.ScreenRect.yMax + Padding, 0f), new Quaternion(0f, 180f, 0f, 0f)).transform;
        topM25 = Instantiate(origin, new Vector3(-25, GameManager.ScreenRect.yMax + Padding, 0f), Quaternion.identity).transform;
        topM25Flip = Instantiate(origin, new Vector3(-25, GameManager.ScreenRect.yMax + Padding, 0f), new Quaternion(0f, 180f, 0f, 0f)).transform;
        topP5 = Instantiate(origin, new Vector3(5, GameManager.ScreenRect.yMax + Padding, 0f), Quaternion.identity).transform;
        topP5Flip = Instantiate(origin, new Vector3(5, GameManager.ScreenRect.yMax + Padding, 0f), new Quaternion(0f, 180f, 0f, 0f)).transform;
        topP10 = Instantiate(origin, new Vector3(10, GameManager.ScreenRect.yMax + Padding, 0f), Quaternion.identity).transform;
        topP10Flip = Instantiate(origin, new Vector3(10, GameManager.ScreenRect.yMax + Padding, 0f), new Quaternion(0f, 180f, 0f, 0f)).transform;
        topP15 = Instantiate(origin, new Vector3(15, GameManager.ScreenRect.yMax + Padding, 0f), Quaternion.identity).transform;
        topP15Flip = Instantiate(origin, new Vector3(15, GameManager.ScreenRect.yMax + Padding, 0f), new Quaternion(0f, 180f, 0f, 0f)).transform;
        topP20 = Instantiate(origin, new Vector3(20, GameManager.ScreenRect.yMax + Padding, 0f), Quaternion.identity).transform;
        topP20Flip = Instantiate(origin, new Vector3(20, GameManager.ScreenRect.yMax + Padding, 0f), new Quaternion(0f, 180f, 0f, 0f)).transform;
        topP25 = Instantiate(origin, new Vector3(25, GameManager.ScreenRect.yMax + Padding, 0f), Quaternion.identity).transform;
        topP25Flip = Instantiate(origin, new Vector3(25, GameManager.ScreenRect.yMax + Padding, 0f), new Quaternion(0f, 180f, 0f, 0f)).transform;
    }

    public void UpdateEnemyCreation()
    {
        while (Enemies.Count > 0 && EnemyIndex > 0 && Enemies[EnemyIndex - 1].CreationTime > GameManager.LevelTime)
        {
            --EnemyIndex;
        }

        while (EnemyIndex < Enemies.Count && Enemies[EnemyIndex].CreationTime <= GameManager.LevelTime)
        {
            NPCCreator.CreateEnemy(Enemies[EnemyIndex]);
            ++EnemyIndex;
        }

        if (Bosses.Count > 0 && Bosses[0].CreationTime <= GameManager.LevelTime)
        {
            NPCCreator.CreateBoss(Bosses[0]);
            Bosses.RemoveAt(0);
        }
    }
}