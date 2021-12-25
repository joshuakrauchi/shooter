using System.Collections.Generic;

public class MinionTimeData : EnemyTimeData
{
    public float AnimationTime { get; private set; }

    public MinionTimeData(float health, bool isDisabled, List<ShootBehaviour> shootBehaviours, float animationTime) : base(health, isDisabled, shootBehaviours)
    {
        AnimationTime = animationTime;
    }
}