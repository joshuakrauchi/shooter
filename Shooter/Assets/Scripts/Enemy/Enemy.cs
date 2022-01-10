using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : TimeObject
{
    [SerializeField] private EnemyManager enemyManager;
    [SerializeField] private float health = 1f;
    [SerializeField] private float rewindRecharge = 0.1f;
    [SerializeField] private GameObject[] drops;
    [SerializeField] protected GameData gameData;

    public float Health
    {
        get => health;
        protected set => health = value;
    }

    public float RewindRecharge
    {
        get => rewindRecharge;
        private set => rewindRecharge = value;
    }

    public EnemyCollision EnemyCollision { get; private set; }

    public SpriteRenderer SpriteRenderer { get; private set; }
    public bool IsDisabled { get; protected set; }
    public float CreationTime { get; set; }

    protected override void Awake()
    {
        base.Awake();

        EnemyCollision = GetComponent<EnemyCollision>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
        enemyManager.AddEnemy(this);
    }

    protected override void Record()
    {
        if (((EnemyTimeData) TimeData.First.Value).IsDisabled)
        {
            DestroySelf();
        }
    }

    public void OnHit(Projectile projectile)
    {
        Health -= gameData.ProjectileDamage;

        projectile.IsDisabled = true;

        if (Health <= 0f)
        {
            Disable();

            gameData.RewindCharge += RewindRecharge;

            RewindRecharge /= 2f;
        }
    }

    protected virtual void Disable()
    {
        if (drops.Length > 0)
        {
            NPCCreator.CreateCollectible(drops[0], transform.position);
        }

        IsDisabled = true;
    }

    protected void DestroySelf()
    {
        enemyManager.RemoveEnemy(this);
        Destroy(gameObject);
    }
}