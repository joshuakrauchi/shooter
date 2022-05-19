using UnityEngine;
using Random = UnityEngine.Random;

public class ShootSpiral : ShootBehaviour
{
    [SerializeField] private uint directions;
    [SerializeField] private float angleIncreasePerCycle;
    [SerializeField] private float angleVariation;

    private float _currentAngle;
    
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
        var angleBetweenProjectiles = 360.0f / directions;
        for (var i = 0; i < directions; ++i)
        {
            NPCCreator.CreateProjectile(ProjectilePrefab, transform.position, Quaternion.Euler(0.0f, 0.0f, _currentAngle + angleBetweenProjectiles * i + Random.Range(-angleVariation, angleVariation)));
        }

        _currentAngle += angleIncreasePerCycle;

        return true;
    }
}