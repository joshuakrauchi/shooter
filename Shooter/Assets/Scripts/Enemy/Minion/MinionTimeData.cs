public class MinionTimeData : EnemyTimeData
{
    public float AnimationTime { get; private set; }

    public MinionTimeData(float health, bool isDisabled, ShootBehaviour shootBehaviour, float animationTime) : base(health, isDisabled, shootBehaviour)
    {
        AnimationTime = animationTime;
    }
}