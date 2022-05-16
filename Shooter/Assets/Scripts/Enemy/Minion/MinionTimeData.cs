public class MinionTimeData : EnemyTimeData
{
    public ShootBehaviour ShootBehaviour { get; }

    public MinionTimeData(bool isDisabled, float health, ShootBehaviour shootBehaviour) : base(isDisabled, health)
    {
        ShootBehaviour = shootBehaviour;
    }
}