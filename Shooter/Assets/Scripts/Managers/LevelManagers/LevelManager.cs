using System.Collections.Generic;
using UnityEngine;

public abstract class LevelManager : MonoBehaviour
{
    public List<EnemySpawn> Enemies { get; private set; }
    public List<BossSpawn> Bosses { get; private set; }
    public int EnemyIndex { get; set; }

    protected float CurrentTime = 1f;
    protected Transform origin;
    protected Transform rotate;
    protected Transform topCenter;
    protected Transform topCenterFlip;
    protected Transform topMidLeft;
    protected Transform topMidLeftFlip;
    protected Transform topLeft;
    protected Transform topRight;
    protected Transform topMidRight;
    protected Transform topMidRightFlip;

    private const float Padding = 2f;

    protected virtual void Awake()
    {
        Enemies = new List<EnemySpawn>();
        Bosses = new List<BossSpawn>();

        origin = Instantiate(new GameObject(), new Vector3(), Quaternion.identity).transform;
        rotate = Instantiate(new GameObject(), new Vector3(), new Quaternion(0f, 0f, 180f, 0f)).transform;
        topCenter = Instantiate(new GameObject(), new Vector3(0f, GameManager.Top + Padding, 0f), Quaternion.identity).transform;
        topCenterFlip = Instantiate(new GameObject(), new Vector3(0f, GameManager.Top + Padding, 0f), new Quaternion(0f, 180f, 0f, 0f)).transform;
        topMidLeft = Instantiate(new GameObject(), new Vector3(GameManager.Left / 2, GameManager.Top + Padding, 0f), Quaternion.identity).transform;
        topMidLeftFlip = Instantiate(new GameObject(), new Vector3(GameManager.Left / 2, GameManager.Top + Padding, 0f), new Quaternion(0f, 180f, 0f, 0f)).transform;
        topLeft = Instantiate(new GameObject(), new Vector3(GameManager.Left + Padding, GameManager.Top + Padding, 0f), Quaternion.identity).transform;
        topRight = Instantiate(new GameObject(), new Vector3(GameManager.Right - Padding, GameManager.Top + Padding, 0f), Quaternion.identity).transform;
        topMidRight = Instantiate(new GameObject(), new Vector3(GameManager.Right / 2, GameManager.Top + Padding, 0f), Quaternion.identity).transform;
        topMidRightFlip = Instantiate(new GameObject(), new Vector3(GameManager.Right / 2, GameManager.Top + Padding, 0f), new Quaternion(0f, 180f, 0f, 0f)).transform;
    }

    private void FixedUpdate()
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