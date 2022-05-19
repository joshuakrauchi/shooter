using System;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class ShootSuccessiveHoming : ShootBehaviour
{
    [SerializeField] private Timer betweenShotsTimer;
    [SerializeField] private uint numberOfProjectiles;
    [SerializeField] private float angleBetweenProjectiles;
    [SerializeField] private float angleVariation;

    private GameObject _target;
    private uint _shotsFired;
    private float _currentAngle;

    protected override bool UpdateCycle(bool isRewinding)
    {
        betweenShotsTimer.UpdateTime(isRewinding);

        if (!betweenShotsTimer.IsFinished(false)) return false;

        betweenShotsTimer.Reset();

        if (_shotsFired == 0)
        {
            Vector2 direction = _target.transform.position - transform.position;
            _currentAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - angleBetweenProjectiles * Mathf.Floor(numberOfProjectiles / 2.0f);
        }

        NPCCreator.CreateProjectile(ProjectilePrefab, transform.position, Quaternion.Euler(0.0f, 0.0f, _currentAngle + Random.Range(-angleVariation, angleVariation)));
        _currentAngle += angleBetweenProjectiles;
        ++_shotsFired;

        if (_shotsFired != numberOfProjectiles) return false;
        
        _shotsFired = 0;

        return true;
    }
}