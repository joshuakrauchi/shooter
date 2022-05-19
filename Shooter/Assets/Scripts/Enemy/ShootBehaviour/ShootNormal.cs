using System;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class ShootNormal : ShootBehaviour
{
    [SerializeField] private uint numberOfProjectiles;
    [SerializeField] private float initialDirection;
    [SerializeField] private float angleBetweenProjectiles;
    [SerializeField] private float angleVariation;

    public override ShootTimeData GetRecordData()
    {
        return new ShootTimeData(CycleTimer, CurrentCycles);
    }

    public override void SetRewindData(ShootTimeData shootTimeData)
    {
        base.SetRewindData(shootTimeData);
        
        
    }
    
    protected override bool UpdateCycle(bool isRewinding)
    {
        var currentAngle = initialDirection - angleBetweenProjectiles * Mathf.Floor(numberOfProjectiles / 2.0f) + Random.Range(-angleVariation, angleVariation);

        for (var i = 0; i < numberOfProjectiles; ++i)
        {
            NPCCreator.CreateProjectile(ProjectilePrefab, transform.position, Quaternion.Euler(0.0f, 0.0f, currentAngle));
            currentAngle += angleBetweenProjectiles;
        }
        
        return true;
    }
}