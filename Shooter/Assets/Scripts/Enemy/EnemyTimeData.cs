public class EnemyTimeData : ITimeData
{
    public float Time { get; private set; }
    public float Health { get; private set; }
    public bool IsDisabled { get; private set; }
    public ShootBehaviour ShootBehaviour { get; private set; }

    public EnemyTimeData(float time, float health, bool isDisabled, ShootBehaviour shootBehaviour)
    {
        Time = time;
        Health = health;
        IsDisabled = isDisabled;
        ShootBehaviour = shootBehaviour;
    }
}