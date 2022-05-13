public abstract class EnemyTimeData : ITimeData
{
    public float Health { get; }
    public bool IsDisabled { get; }

    public EnemyTimeData(float health, bool isDisabled)
    {
        Health = health;
        IsDisabled = isDisabled;
    }
}