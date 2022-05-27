using UnityEngine;

public class SpecialAOE : PlayerSpecial
{
    [field: SerializeField] private ProjectileManager ProjectileManager { get; set; }
    [field: SerializeField] private GameObject AOEPrefab { get; set; }
    
    private bool HasShot { get; set; }
    
    public override void UpdateShoot(bool isShooting)
    {
        if (HasShot && !isShooting)
        {
            HasShot = false;
        }

        if (!isShooting || HasShot || !CanShoot()) return;

        Shoot();
        HasShot = true;
    }

    private void Shoot()
    {
        ProjectileManager.CreateProjectile(AOEPrefab, transform.position, Quaternion.identity);
    }
}