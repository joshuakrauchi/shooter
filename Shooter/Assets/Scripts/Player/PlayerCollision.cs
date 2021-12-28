using UnityEngine;

public class PlayerCollision : Collision
{
    [SerializeField] private string enemyTag;
    [SerializeField] private string collectibleTag;

    protected override void HandleOverlapCollision(Collider2D hit)
    {
        if (hit.CompareTag(enemyTag))
        {
            GameManager.OnPlayerHit();
        }
        else if (hit.CompareTag(collectibleTag))
        {
            GameManager.OnCollectibleHit();
            Destroy(hit.gameObject);
        }
    }
}