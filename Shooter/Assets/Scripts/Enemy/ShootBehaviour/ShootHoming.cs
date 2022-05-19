using UnityEngine;
using Random = UnityEngine.Random;

public class ShootHoming : ShootBehaviour
{
    private class ShootHomingTimeData : ShootTimeData
    {
        public Timer ShotTimer { get; }
        public uint CurrentShots { get; }

        public ShootHomingTimeData(Timer cycleTimer, uint currentCycles, Timer shotTimer, uint currentShots) : base(cycleTimer, currentCycles)
        {
            ShotTimer = shotTimer;
            CurrentShots = currentShots;
        }
    }

    [field: SerializeField] private GameData GameData { get; set; }
    [field: SerializeField] private Timer ShotTimer { get; set; } = new(0.0f);
    [field: SerializeField] private uint ShotsPerCycle { get; set; } = 1;
    [field: SerializeField] private uint ProjectilesPerShot { get; set; } = 3;
    [field: SerializeField] private float AngleBetweenProjectiles { get; set; } = 20.0f;
    [field: SerializeField] private float AngleVariation { get; set; } = 2.0f;
    [field: SerializeField] private float SpeedChangeBetweenShots { get; set; }

    private Transform Target { get; set; }
    private uint CurrentShots { get; set; }

    private void Awake()
    {
        Target = GameData.Player.transform;
    }

    public override ShootTimeData GetRecordData()
    {
        return new ShootHomingTimeData(CycleTimer, CurrentCycles, ShotTimer, CurrentShots);
    }

    public override void SetRewindData(ShootTimeData shootTimeData)
    {
        base.SetRewindData(shootTimeData);

        ShootHomingTimeData shootHomingTimeData = (ShootHomingTimeData) shootTimeData;

        ShotTimer = shootHomingTimeData.ShotTimer;
        CurrentShots = shootHomingTimeData.CurrentShots;
    }

    protected override bool UpdateCycle(bool isRewinding)
    {
        ShotTimer.UpdateTime(isRewinding);

        if (!ShotTimer.IsFinished(false)) return false;

        ShotTimer = new Timer(ShotTimer.TimeToFinish);

        Vector2 direction = Target.position - transform.position;
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        angle -= AngleBetweenProjectiles * Mathf.Floor(ProjectilesPerShot / 2.0f) + Random.Range(-AngleVariation, AngleVariation);

        for (var i = 0; i < ProjectilesPerShot; ++i)
        {
            ProjectileMovement projectileMovement = NPCCreator.CreateProjectile(ProjectilePrefab, transform.position, Quaternion.Euler(0.0f, 0.0f, angle));
            projectileMovement.Speed += SpeedChangeBetweenShots * CurrentShots;
            angle += AngleBetweenProjectiles;
        }

        ++CurrentShots;

        if (CurrentShots < ShotsPerCycle) return false;

        CurrentShots = 0;
        return true;
    }
}