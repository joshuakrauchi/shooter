using System.Collections.Generic;

public abstract class EnemyTimeData : ITimeData
{
    public float Health { get; }
    public bool IsDisabled { get; }
    public List<ShootBehaviour> ShootBehaviours { get; }

    public EnemyTimeData(float health, bool isDisabled, List<ShootBehaviour> shootBehaviours)
    {
        Health = health;
        IsDisabled = isDisabled;
        ShootBehaviours = shootBehaviours;
    }
}