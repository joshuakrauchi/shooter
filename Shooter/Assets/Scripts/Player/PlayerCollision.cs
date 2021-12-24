using UnityEngine;

public class PlayerCollision : Collision
{
    protected override void HandleOverlapCollision(Collider2D hit)
    {
        if (hit.CompareTag("Enemy"))
        {
            GameManager.Die();
        }
    }
}