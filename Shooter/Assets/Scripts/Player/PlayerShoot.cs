using UnityEngine;

public class PlayerShoot : Shoot
{
    [SerializeField] private GameObject projectilePrefab;

    public bool IsShooting { get; set; }
    
    private float _currentDelay;

    public override void UpdateShoot()
    {
        if (_currentDelay >= ShootDelay)
        {
            if (IsShooting)
            {
                NPCCreator.CreateProjectile(new ProjectileDefinition(projectilePrefab, Pattern.MoveStraight), transform.position, Quaternion.identity);
                _currentDelay -= ShootDelay;
            }
        }
        else
        {
            _currentDelay += Time.deltaTime;
        }
    }
}