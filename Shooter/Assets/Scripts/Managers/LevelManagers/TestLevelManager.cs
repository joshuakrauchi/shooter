using UnityEngine;

public class TestLevelManager : LevelManager
{
    [field: SerializeField] private GameObject Boss { get; set; }
    
    [field: SerializeField] private GameObject Projectile1 { get; set; }
    [field: SerializeField] private GameObject Projectile2 { get; set; }
    [field: SerializeField] private GameObject Projectile3 { get; set; }
    
    [field: SerializeField] private GameObject Soldier { get; set; }

    
    private const string HDangle = "HDangle";
    private const string HSlide1 = "HSlide1";
    private const string HSlide2 = "HSlide2";
    private const string Test = "Test";
    private const string VDangle = "VDangle";
    private const string VSlide1 = "VSlide1";
    private const string VSlide2 = "VSlide2";
    private const string Wave = "Wave";
    private const string VStraight = "VStraight";
    private const string DropLeave = "DropLeave";

    protected override void Awake()
    {
        base.Awake();
        
        //ProjectileManager.AddProjectilePool(Projectile1, 1000);
        //ProjectileManager.AddProjectilePool(Projectile2, 1000);
        //ProjectileManager.AddProjectilePool(Projectile3, 1000);
        
        CurrentTime += 0.5f;
        
        AddMinion(Soldier, TopTransforms[-3], VSlide1);


        return;
        
        CurrentTime += 0.5f;
        

        
        for (var i = 0; i < 10; ++i)
        {
            AddMinion(Soldier, TopTransforms[-3], VSlide1);
            CurrentTime += 0.25f;
        }

        //AddBoss(Boss, new Vector2());
    }
}