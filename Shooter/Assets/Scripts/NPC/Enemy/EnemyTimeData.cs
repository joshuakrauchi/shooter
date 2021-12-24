public class EnemyTimeData : TimeData
{
    public float Health { get; private set; }
    public bool IsDisabled { get; private set; }
    public ShootBehaviour ShootBehaviour { get; private set; }

    public EnemyTimeData(float time, float health, bool isDisabled, ShootBehaviour shootBehaviour) : base(time)
    {
        Health = health;
        IsDisabled = isDisabled;
        ShootBehaviour = shootBehaviour;
    }
}