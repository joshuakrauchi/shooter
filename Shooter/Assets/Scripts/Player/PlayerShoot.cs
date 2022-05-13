using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [field: SerializeField] private GameObject projectilePrefab;
    [field: SerializeField] public float NumberOfShots { get; set; }
    [field: SerializeField] private float ArmSpan { get; set; }
    
    [SerializeField] private float shootDelay;

    public float ShootDelay
    {
        get => shootDelay;
        set
        {
            shootDelay = value;
            ShootTimer = new Timer(ShootDelay);
        }
    }

    private ProjectileDefinition ProjectileDefinition { get; set; }
    private Timer ShootTimer { get; set; }

    public void Awake()
    {
        ShootTimer = new Timer(ShootDelay);
        ProjectileDefinition = new ProjectileDefinition(projectilePrefab, new MoveBehaviour[] {new MoveStraight(0.0f)});
    }

    public void UpdateShoot(bool isShooting, bool isRewinding)
    {
        ShootTimer.UpdateTime(isRewinding);

        if (!ShootTimer.IsFinished(false) || !isShooting) return;

        Vector3 position = transform.position;
        for (var i = 0; i < NumberOfShots; ++i)
        {
            NPCCreator.CreateProjectile(ProjectileDefinition, new Vector2(position.x + ArmSpan * i, position.y), Quaternion.Euler(0f, 0f, 90f));
        }

        ShootTimer.Reset();
    }
}