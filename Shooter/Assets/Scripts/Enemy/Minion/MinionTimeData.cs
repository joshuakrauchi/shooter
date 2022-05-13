public class MinionTimeData : EnemyTimeData
{
    public float AnimationTime { get; }
    public ShootBehaviour ShootBehaviour { get; }

    public MinionTimeData(float health, bool isDisabled, float animationTime, ShootBehaviour shootBehaviour) : base(health, isDisabled)
    {
        AnimationTime = animationTime;
        ShootBehaviour = shootBehaviour;
    }
}