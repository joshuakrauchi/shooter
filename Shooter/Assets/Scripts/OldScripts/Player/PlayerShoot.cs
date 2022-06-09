using System;
using Unity.Entities;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [field: SerializeField] private GameData GameData { get; set; }
    [field: SerializeField] private ProjectileManager ProjectileManager { get; set; }
    [field: SerializeField] private GameObject ProjectilePrefab { get; set; }
    [field: SerializeField] private float NumberOfShots { get; set; }
    [field: SerializeField] private float ArmSpan { get; set; }

    private Timer ShootTimer { get; set; }

    private Entity e;
    private BlobAssetStore blob;

    private uint projectiles;

    public void Start()
    {
        //ProjectileManager.AddProjectilePool(ProjectilePrefab, 100);

        blob = new BlobAssetStore();

        e = GameObjectConversionUtility.ConvertGameObjectHierarchy(ProjectilePrefab, GameObjectConversionSettings.FromWorld(World.DefaultGameObjectInjectionWorld, blob));
    }

    public void OnDestroy()
    {
        blob.Dispose();
    }

    public void UpdateShoot(bool isShooting, bool isRewinding)
    {
        ShootTimer.UpdateTime(isRewinding);

        if (!ShootTimer.IsFinished(false) || !isShooting) return;

        Vector3 position = transform.position;


        projectiles += (uint) NumberOfShots;

        Debug.Log(projectiles);

        for (var i = 0; i < NumberOfShots; ++i)
        {
            //ProjectileManager.CreateProjectile(ProjectilePrefab, new Vector2(position.x + ArmSpan * i, position.y), Quaternion.Euler(0f, 0f, 90.0f));
            ProjectileManager.CreateProjectile(e, new Vector2(position.x + ArmSpan * i, position.y), Quaternion.Euler(0f, 0f, 90.0f));
        }

        ShootTimer.Reset();
    }

    public void UpdateStats()
    {
        NumberOfShots = GameData.NumberOfShots;
        ShootTimer = new Timer(GameData.ShootDelay);
    }
}