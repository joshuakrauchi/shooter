using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : Shoot
{
    public ShootBehaviour ShootBehaviour { get; set; }
    
    public float InitialDelay { get; set; }
    [SerializeField] private uint maxShots;
    public uint MaxShots {
        get => maxShots;
        set => maxShots = value;
    }
    
    public List<ProjectileDefinition> ProjectileDefinitions { get; set; }
    
    private float _currentDelay;
    private uint _currentShots;

    public override void UpdateShoot()
    {
        if (!GameManager.IsRewinding)
        {
            _currentDelay += Time.deltaTime;
            if ((MaxShots == 0 || _currentShots < MaxShots) && _currentDelay >= ShootDelay)
            {
                ShootBehaviour?.UpdateShoot(ProjectileDefinitions, transform.position);
                ++_currentShots;
                _currentDelay -= ShootDelay;
            }
        }
        else
        {
            if (_currentDelay > 0f)
            {
                _currentDelay -= Time.deltaTime;
            }
            else if (_currentShots > 0)
            {
                --_currentShots;
                _currentDelay += ShootDelay;
            }
        }
    }
}