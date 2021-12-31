using System;
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

    private ProjectileDefinition _projectileDefinition;
    private Timer _shootTimer;

    public void Awake()
    {
        _shootTimer = new LockedTimer(ShootDelay);
        _projectileDefinition = new ProjectileDefinition(projectilePrefab, new[] {new MovePair(0f, new MoveStraight())});
    }

    public void UpdateShoot(bool isShooting)
    {
        _shootTimer.UpdateTime();

        if (!_shootTimer.IsFinished(false) || !isShooting) return;

        NPCCreator.CreateProjectile(_projectileDefinition, transform.position, Quaternion.Euler(0f, 0f, 90f));
        _shootTimer.Reset(true);
    }
}