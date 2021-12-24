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

    private Timer timer;

    public void Awake()
    {
        timer = new Timer(ShootDelay);
    }

    public void UpdateShoot()
    {
        if (timer.IsFinished())
        {
            if (IsShooting)
            {
                NPCCreator.CreateProjectile(new ProjectileDefinition(projectilePrefab, Pattern.MoveStraight), transform.position, Quaternion.identity);
                timer.SubtractTotalTime();
            }
        }
        else
        {
            timer.UpdateTime();
        }
    }
}