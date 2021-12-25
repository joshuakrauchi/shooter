using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float shootDelay = 1f;

    public float ShootDelay
    {
        get => shootDelay;
        set => shootDelay = value;
    }

    public bool IsShooting { get; set; }
    public Timer ShootTimer { get; private set; }

    public void Awake()
    {
        ShootTimer = new Timer(ShootDelay);
    }

    public void UpdateShoot()
    {
        if (ShootTimer.IsFinished())
        {
            if (IsShooting)
            {
                NPCCreator.CreateProjectile(new ProjectileDefinition(projectilePrefab, Pattern.MoveStraight), transform.position, Quaternion.identity);
                ShootTimer.SubtractTotalTime();
            }
        }
        else
        {
            ShootTimer.UpdateTime();
        }
    }
}