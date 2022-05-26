using UnityEngine;

public class SpecialBigShot : PlayerSpecial
{
    [field: SerializeField] private ProjectileManager ProjectileManager { get; set; }
    [field: SerializeField] private GameObject ProjectilePrefab { get; set; }

    private bool HasShot { get; set; }
    
    public override void UpdateShoot(bool isShooting)
    {
        if (!HasShot && isShooting && CanShoot())
        {
            Shoot();
            
            HasShot = true;
        }

        if (HasShot && !isShooting)
        {
            HasShot = false;
        }
    }

    private void Shoot()
    {
        ProjectileManager.CreateProjectile(ProjectilePrefab, transform.position, Quaternion.Euler(0f, 0f, 90.0f));
        
        DecreaseSpecialCharge();
    }
}