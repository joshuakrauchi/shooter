using UnityEngine;
using Random = UnityEngine.Random;

public class ShootHoming : ShootBehaviour
{
    [field: SerializeField] private GameData GameData { get; set; }
    [SerializeField] private Timer shotTimer;
    [SerializeField] private uint shotsPerCycle = 1;
    [SerializeField] private uint projectilesPerShot = 3;
    [SerializeField] private float angleBetweenProjectiles = 20;
    [SerializeField] private float angleVariation = 2;
    [SerializeField] private float speedChangeBetweenShots = 0;

    private Transform _target;
    private uint _currentShots;

    private void Awake()
    {
        _target = GameData.Player.transform;
        shotTimer = new Timer(0.0f);
        
        
    }

    protected override bool UpdateCycle(bool isRewinding)
    {
        shotTimer.UpdateTime(isRewinding);

        if (!shotTimer.IsFinished(false)) return false;

        shotTimer.Reset();

        Vector2 direction = _target.position - transform.position;
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        angle -= angleBetweenProjectiles * Mathf.Floor(projectilesPerShot / 2.0f) + Random.Range(-angleVariation, angleVariation);

        for (var i = 0; i < projectilesPerShot; ++i)
        {
            ProjectileMovement projectileMovement = NPCCreator.CreateProjectile(ProjectilePrefab, transform.position, Quaternion.Euler(0.0f, 0.0f, angle));
            projectileMovement.Speed += speedChangeBetweenShots * _currentShots;
            angle += angleBetweenProjectiles;
        }

        ++_currentShots;

        if (_currentShots != shotsPerCycle) return false;
        
        _currentShots = 0;
        return true;
    }
}