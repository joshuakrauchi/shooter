using UnityEngine;

public class PlayerCollision : Collision
{
    [SerializeField] private string enemyTag;
    [SerializeField] private string collectibleTag;

    public Player Player { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        Player = GetComponent<Player>();
    }

    protected override void HandleOverlapCollision(Collider2D hit)
    {
        if (hit.CompareTag(enemyTag))
        {
            Player.OnHit();
        }
        else if (hit.CompareTag(collectibleTag))
        {
            Collectible collectible = hit.GetComponent<Collectible>();
            Player.OnCollectibleHit(collectible);
        }
    }
}