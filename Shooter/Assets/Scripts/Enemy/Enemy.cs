using UnityEngine;

public abstract class Enemy : TimeObject
{
    [field: SerializeField] public float Health { get; protected set; } = 1f;
    [field: SerializeField] public float RewindRecharge { get; protected set; }= 0.1f;
    [SerializeField] protected EnemyManager enemyManager;
    [SerializeField] protected GameObject[] drops;
    [SerializeField] protected GameData gameData;

    public EnemyCollision EnemyCollision { get; protected set; }
    public SpriteRenderer SpriteRenderer { get; protected set; }
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

    public void OnHit(PlayerProjectile projectile)
    {
        Health -= projectile.Damage;

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