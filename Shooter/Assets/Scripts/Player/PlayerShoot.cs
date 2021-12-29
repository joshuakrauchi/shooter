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
            _shootTimer = new LockedTimer(ShootDelay);
        }
    }

    private Timer _shootTimer;

    public void Awake()
    {
        _shootTimer = new LockedTimer(ShootDelay);
    }

    public void UpdateShoot(bool isShooting)
    {
        _shootTimer.UpdateTime();

        if (!_shootTimer.IsFinished(false) || !isShooting) return;

        NPCCreator.CreateProjectile(new ProjectileDefinition(projectilePrefab, Pattern.MoveStraight), transform.position, Quaternion.identity);
        _shootTimer.Reset(true);
    }
}