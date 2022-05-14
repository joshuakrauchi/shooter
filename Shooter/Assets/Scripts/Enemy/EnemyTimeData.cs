public abstract class EnemyTimeData : ITimeData
{
    public bool IsDisabled { get; }
    public float Health { get; }
    
    protected EnemyTimeData(bool isDisabled, float health)
    {
        IsDisabled = isDisabled;
        Health = health;
    }
}