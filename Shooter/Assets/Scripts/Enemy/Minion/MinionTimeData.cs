public class MinionTimeData : EnemyTimeData
{
    public float AnimationTime { get; }
    public ShootBehaviour ShootBehaviour { get; }

    public MinionTimeData(bool isDisabled, float health, float animationTime, ShootBehaviour shootBehaviour) : base(isDisabled, health)
    {
        AnimationTime = animationTime;
        ShootBehaviour = shootBehaviour;
    }
}