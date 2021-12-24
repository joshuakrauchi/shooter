using System.Collections.Generic;
using UnityEngine;

public class Enemy : TimeObject
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
    public Animator Animator { get; private set; }
    public SpriteRenderer SpriteRenderer { get; private set; }
    public bool IsDisabled { get; protected set; }
    public float CreationTime { get; set; }

    protected override void Awake()
    {
        base.Awake();

        Animator = GetComponent<Animator>();
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

    protected override void Record()
    {
        TimeData.AddLast(new EnemyTimeData(Animator.GetCurrentAnimatorStateInfo(0).normalizedTime, Health, IsDisabled, (ShootBehaviour) ShootBehaviour?.Clone()));

        if (((EnemyTimeData) TimeData.First.Value).IsDisabled)
        {
            Die();
        }
    }

    protected override void Rewind()
    {
        if (CreationTime > GameManager.LevelTime)
        {
            Die();
        }

        if (TimeData.Count <= 0) return;

        var timeData = (EnemyTimeData) TimeData.Last.Value;
        Animator.Play(0, 0, timeData.Time);
        Health = timeData.Health;
        IsDisabled = timeData.IsDisabled;
        ShootBehaviour = timeData.ShootBehaviour;
        TimeData.Remove(timeData);
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

    private void Die()
    {
        EnemyManager.Instance.RemoveEnemy(this);
        Destroy(gameObject);
    }
}