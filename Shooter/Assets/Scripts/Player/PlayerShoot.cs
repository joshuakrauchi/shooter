using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float shootDelay;

    public float ShootDelay
    {
        get => shootDelay;
        private set
        {
            shootDelay = value;
            ShootTimer = new LockedTimer(ShootDelay);
        }
    }

    public Timer ShootTimer { get; private set; }

    public void Awake()
    {
        ShootTimer = new LockedTimer(ShootDelay);
    }

    public void UpdateShoot(bool isShooting)
    {
        ShootTimer.UpdateTime();

        if (!ShootTimer.IsFinished(false) || !isShooting) return;

        NPCCreator.CreateProjectile(new ProjectileDefinition(projectilePrefab, Pattern.MoveStraight), transform.position, Quaternion.identity);
        ShootTimer.Reset(true);
    }
}