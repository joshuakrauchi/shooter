using UnityEngine;

public class PlayerCollision : Collision
{
    [SerializeField] private string enemyTag;

    protected override void HandleOverlapCollision(Collider2D hit)
    {
        if (hit.CompareTag(enemyTag))
        {
            GameManager.OnPlayerHit();
        }
    }
}