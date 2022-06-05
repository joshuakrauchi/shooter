using UnityEngine;

public class TestLevelManager : LevelManager
{
    [field: SerializeField] private GameObject Boss { get; set; }
    
    [field: SerializeField] private GameObject Projectile1 { get; set; }
    [field: SerializeField] private GameObject Projectile2 { get; set; }
    [field: SerializeField] private GameObject Projectile3 { get; set; }

    
    protected override void Awake()
    {
        base.Awake();
        
        //ProjectileManager.AddProjectilePool(Projectile1, 1000);
        //ProjectileManager.AddProjectilePool(Projectile2, 1000);
        //ProjectileManager.AddProjectilePool(Projectile3, 1000);

        CurrentTime += 0.5f;

        AddBoss(Boss, new Vector2());
    }
}