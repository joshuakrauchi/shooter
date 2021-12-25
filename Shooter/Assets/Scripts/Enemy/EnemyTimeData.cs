using System.Collections.Generic;

public abstract class EnemyTimeData : ITimeData
{
    public float Health { get; private set; }
    public bool IsDisabled { get; private set; }
    public List<ShootBehaviour> ShootBehaviours { get; private set; }

    public EnemyTimeData(float health, bool isDisabled, List<ShootBehaviour> shootBehaviours)
    {
        Health = health;
        IsDisabled = isDisabled;
        ShootBehaviours = shootBehaviours;
    }
}