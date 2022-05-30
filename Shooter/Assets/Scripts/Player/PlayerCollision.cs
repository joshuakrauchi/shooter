using UnityEngine;

public class PlayerCollision : Collision
{
    [SerializeField] private string enemyTag;
    [SerializeField] private string collectibleTag;

    private Player Player { get; set; }

    protected override void Awake()
    {
        base.Awake();

        Player = GetComponent<Player>();
    }

    protected override void HandleOverlapCollision(Collider2D hit)
    {
        if (hit.GetComponent<Enemy>() is { })
        {
            Player.OnHit();
        }
        else if (hit.GetComponent<Collectible>() is { })
        {
            Collectible collectible = hit.GetComponent<Collectible>();
            Player.OnCollectibleHit(collectible);
        }
    }
}