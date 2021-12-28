using UnityEngine;

public class PlayerCollision : Collision
{
    [SerializeField] private string enemyTag;

    private bool _hasBeenHitThisFrame;

    public override void UpdateCollision()
    {

        base.UpdateCollision();


    }

    protected override void HandleOverlapCollision(Collider2D hit)
    {
        if (hit.CompareTag(enemyTag))
        {
            _hasBeenHitThisFrame = true;
            GameManager.OnPlayerHit();
        }
    }
}