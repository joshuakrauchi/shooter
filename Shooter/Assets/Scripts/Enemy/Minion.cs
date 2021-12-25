using UnityEngine;

public class Minion : Enemy
{
    public Animator Animator { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        Animator = GetComponent<Animator>();
    }

    protected override void Record()
    {
        AddTimeData(new EnemyTimeData(Animator.GetCurrentAnimatorStateInfo(0).normalizedTime, Health, IsDisabled, (ShootBehaviour) ShootBehaviour?.Clone()));

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

    private void Die()
    {
        EnemyManager.Instance.RemoveEnemy(this);
        Destroy(gameObject);
    }
}