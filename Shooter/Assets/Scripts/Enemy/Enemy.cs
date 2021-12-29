using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : TimeObject
{
    [SerializeField] private float health = 1f;
    [SerializeField] private float rewindRecharge = 0.1f;
    [SerializeField] private GameObject[] drops;

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

    public List<ProjectileDefinition> ProjectileDefinitions { get; set; }
    public EnemyCollision EnemyCollision { get; private set; }
    [SerializeReference] private List<ShootBehaviour> shootBehaviours;
    public List<ShootBehaviour> ShootBehaviours {
        get => shootBehaviours;
        set => shootBehaviours = value;
    }
    public SpriteRenderer SpriteRenderer { get; private set; }
    public bool IsDisabled { get; protected set; }
    public float CreationTime { get; set; }

    protected override void Awake()
    {
        base.Awake();

        EnemyCollision = GetComponent<EnemyCollision>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
        ShootBehaviours = new List<ShootBehaviour>();
        EnemyManager.Instance.AddEnemy(this);
    }

    protected override void Record()
    {
        if (((EnemyTimeData) TimeData.First.Value).IsDisabled)
        {
            DestroySelf();
        }
    }

    public virtual void UpdateEnemy()
    {
        SpriteRenderer.enabled = !IsDisabled;
        EnemyCollision.Collider.enabled = !IsDisabled;

        if (!IsDisabled && !GameManager.IsRewinding)
        {
            EnemyCollision.UpdateCollision();
            foreach (var shootBehaviour in ShootBehaviours)
            {
                shootBehaviour?.UpdateShoot(transform.position);
            }
        }

        UpdateTimeData();
    }

    public void OnHit(Projectile projectile)
    {
        Health -= GameManager.ProjectileDamage;

        projectile.IsDisabled = true;

        if (Health <= 0f)
        {
            Disable();

            GameManager.RewindCharge += RewindRecharge;

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
        EnemyManager.Instance.RemoveEnemy(this);
        Destroy(gameObject);
    }
}