using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : TimeObject
{
    [SerializeField] private float health = 1f;

    public float Health
    {
        get => health;
        protected set => health = value;
    }

    public List<ProjectileDefinition> ProjectileDefinitions { get; set; }
    public EnemyCollision EnemyCollision { get; private set; }
    public ShootBehaviour ShootBehaviour { get; set; }
    public SpriteRenderer SpriteRenderer { get; private set; }
    public bool IsDisabled { get; protected set; }
    public float CreationTime { get; set; }

    protected override void Awake()
    {
        base.Awake();


        EnemyCollision = GetComponent<EnemyCollision>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
        EnemyManager.Instance.AddEnemy(this);
    }

    public virtual void UpdateEnemy()
    {
        SpriteRenderer.enabled = !IsDisabled;

        if (!IsDisabled && !GameManager.IsRewinding)
        {
            EnemyCollision.UpdateCollision();
            ShootBehaviour?.UpdateShoot(transform.position);
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
        }
    }

    protected virtual void Disable()
    {
        IsDisabled = true;
    }
}