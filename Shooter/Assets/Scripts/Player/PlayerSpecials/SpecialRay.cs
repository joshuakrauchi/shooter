using UnityEngine;

public class SpecialRay : PlayerSpecial
{
    [field: SerializeField] private GameState GameState { get; set; }
    [field: SerializeField] private GameObject RayPrefab { get; set; }
    [field: SerializeField] private Vector3 RayOffset { get; set; }
    
    private PlayerRayCollision PlayerRayCollision { get; set; }
    private Transform CachedTransform { get; set; }
    private Transform RayTransform { get; set; }
    private bool IsActive { get; set; }

    private void Awake()
    {
        CachedTransform = transform;

        GameObject rayObject = Instantiate(RayPrefab, Utilities.OffscreenPosition, Quaternion.identity);
        RayTransform = rayObject.transform;
        PlayerRayCollision = rayObject.GetComponent<PlayerRayCollision>();
    }

    public override void UpdateShoot(bool isShooting)
    {
        if (!isShooting || !CanShoot())
        {
            if (!IsActive) return;
            
            RayTransform.position = Utilities.OffscreenPosition;
            IsActive = false;

            return;
        }

        if (!IsActive)
        {
            IsActive = true;
            PlayerRayCollision.PreviousFramePosition = CachedTransform.position + RayOffset;
        }

        UpdateRay();

        DecreaseSpecialCharge();
    }
    
    private void UpdateRay()
    {
        RayTransform.position = CachedTransform.position + RayOffset;

        if (!GameState.IsRewinding)
        {
            PlayerRayCollision.UpdateCollision();
        }
    }
}