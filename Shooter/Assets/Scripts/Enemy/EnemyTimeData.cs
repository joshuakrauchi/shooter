public abstract class EnemyTimeData : ITimeData
{
    public float Health { get; private set; }
    public bool IsDisabled { get; private set; }
    public ShootBehaviour ShootBehaviour { get; private set; }

    public EnemyTimeData(float health, bool isDisabled, ShootBehaviour shootBehaviour)
    {
        Health = health;
        IsDisabled = isDisabled;
        ShootBehaviour = shootBehaviour;
    }
}