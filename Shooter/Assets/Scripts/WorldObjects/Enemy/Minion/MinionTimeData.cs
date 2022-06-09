public class MinionTimeData : EnemyTimeData
{
    public ShootBehaviour.ShootTimeData ShootTimeData { get; }

    public MinionTimeData(bool isDisabled, float health, ShootBehaviour.ShootTimeData shootTimeData) : base(isDisabled, health)
    {
        ShootTimeData = shootTimeData;
    }
}