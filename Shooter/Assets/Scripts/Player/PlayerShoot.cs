using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [field: SerializeField] public float NumberOfShots { get; set; }

    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float shootDelay;
    [SerializeField] private float armSpan;

    public float ShootDelay
    {
        get => shootDelay;
        private set
        {
            shootDelay = value;
            _shootTimer = new LockedTimer(ShootDelay);
        }
    }

    private ProjectileDefinition _projectileDefinition;
    private Timer _shootTimer;

    public void Awake()
    {
        _shootTimer = new LockedTimer(ShootDelay);
        _projectileDefinition = new ProjectileDefinition(projectilePrefab, new MoveBehaviour[] {new MoveStraight(0.0f)});
    }

    public void UpdateShoot(bool isShooting, bool isRewinding)
    {
        _shootTimer.UpdateTime(isRewinding);

        if (!_shootTimer.IsFinished(false) || !isShooting) return;

        Vector3 position = transform.position;
        for (var i = 0; i < NumberOfShots; ++i)
        {
            NPCCreator.CreateProjectile(_projectileDefinition, new Vector2(position.x + armSpan * i, position.y), Quaternion.Euler(0f, 0f, 90f));
        }

        _shootTimer.Reset();
    }
}