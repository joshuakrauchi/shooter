using UnityEngine;

public class Minion : Enemy
{
    [field: SerializeReference] public ShootBehaviour ShootBehaviour { get; set; }
    public Animator Animator { get; private set; }

    private float CurrentAnimatorTime { get; set; }
    private uint AnimatorTimeOccurrences { get; set; }

    protected override void Awake()
    {
        base.Awake();

        Animator = GetComponent<Animator>();
        AnimatorTimeOccurrences = 1;
    }

    public override void UpdateUpdateable()
    {
        base.UpdateUpdateable();

        EnemyCollision.Collider.enabled = !IsDisabled;

        if (IsDisabled || GameState.IsRewinding) return;

        ShootBehaviour?.UpdateShoot(transform.position, GameState.IsRewinding);
    }

    protected override void Record()
    {
        ShootBehaviour shootClone = ShootBehaviour?.Clone();

        var animatorTime = Animator.GetCurrentAnimatorStateInfo(0).normalizedTime;

        // Due to the Animator's normalizedTime value only getting updated on a per-frame basis,
        // recording frames in a fixed update means lots of TimeData will have the same animator time.
        // To remedy this, every time a different normalizedTime is found, step backwards through the
        // TimeData list and interpolate the times manually, according to how many occurrences were found.
        // For example, if the time 0.5 was recorded 5 times and the current animatorTime is 1.0, that means
        // the TimeData will be 0.5, 0.5, 0.5, 0.5, 0.5, and we'll be storing 1.0. Before that happens,
        // get an increment amount which is current - recorded / occurrences to get 0.1. Then, increment
        // the TimeData animation steps to be 0.5, 0.6, 0.7, 0.8, 0.9, then store the 1.0.
        if (CurrentAnimatorTime == animatorTime)
        {
            ++AnimatorTimeOccurrences;
        }
        else
        {
            if (AnimatorTimeOccurrences > 1)
            {
                var timeInterpolationIncrement = (animatorTime - CurrentAnimatorTime) / AnimatorTimeOccurrences;
                var timeDataNode = TimeData.Last;
                
                for (var i = AnimatorTimeOccurrences - 1; i > 0; --i)
                {
                    if (timeDataNode == null) break;
                    
                    ((MinionTimeData) timeDataNode.Value).AnimationTime += timeInterpolationIncrement * i;
                    timeDataNode = timeDataNode.Previous;
                }
            }

            CurrentAnimatorTime = animatorTime;
            AnimatorTimeOccurrences = 1;
        }

        AddTimeData(new MinionTimeData(IsDisabled, Health, animatorTime, shootClone));
    }

    protected override void Rewind(ITimeData timeData)
    {
        base.Rewind(timeData);

        MinionTimeData minionTimeData = (MinionTimeData) timeData;
        Animator.Play(0, 0, minionTimeData.AnimationTime);
        ShootBehaviour = minionTimeData.ShootBehaviour;
    }
}