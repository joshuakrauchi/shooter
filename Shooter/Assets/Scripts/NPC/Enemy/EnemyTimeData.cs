public class EnemyTimeData : TimeData
{
    public float Health { get; private set; }
    public bool IsDisabled { get; private set; }

    public EnemyTimeData(float time, float health, bool isDisabled) : base(time)
    {
        Health = health;
        IsDisabled = isDisabled;
    }
}