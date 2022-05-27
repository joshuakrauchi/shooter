using System.Collections.Generic;
using UnityEngine;

public class PlayerRayCollision : Collision
{
    [field: SerializeField] public float Damage { get; set; }
    
    [field: SerializeField] private uint NumberOfSweepLines { get; set; } = 10;

    // Used for sweeping hit results. Must be set when the projectile is unpooled,
    // or else it would theoretically sweep across from the offscreen pool position.
    public Vector2 PreviousFramePosition { get; set; }
    
    private Transform CachedTransform { get; set; }
    private RaycastHit2D[] RaycastHits { get; set; }
    private HashSet<Collider2D> HitColliders { get; set; }
    
    protected override void Awake()
    {
        base.Awake();

        CachedTransform = transform;
        RaycastHits = new RaycastHit2D[MaxOverlapHits];
        HitColliders = new HashSet<Collider2D>();
    }

    public override void UpdateCollision()
    {
        HitColliders.Clear();
        
        Collider.OverlapCollider(OverlapFilter, OverlapHits);

        for (var i = 0; i < OverlapHits.Length; ++i)
        {
            if (OverlapHits[i] == default) break;

            HitColliders.Add(OverlapHits[i]);

            OverlapHits[i] = default;
        }
        
        Vector2 size = Collider.size * CachedTransform.localScale;
        var lineGap = size.y / NumberOfSweepLines;
        var colliderHalfHeight = size.y / 2;

        for (var i = 0; i < NumberOfSweepLines; ++i)
        {
            var yPositionOffset = -colliderHalfHeight + i * lineGap;
            Physics2D.LinecastNonAlloc(new Vector2(PreviousFramePosition.x, PreviousFramePosition.y + yPositionOffset), new Vector2(CachedTransform.position.x, CachedTransform.position.y + yPositionOffset), RaycastHits);
            
            Debug.DrawLine(new Vector2(PreviousFramePosition.x, PreviousFramePosition.y + yPositionOffset), new Vector2(CachedTransform.position.x, CachedTransform.position.y + yPositionOffset), Color.green);
            
            for (var j = 0; j < RaycastHits.Length; ++j)
            {
                if (RaycastHits[j] == default) break;

                HitColliders.Add(RaycastHits[j].collider);
                
                RaycastHits[j] = default;
            }
        }

        foreach (Collider2D hitCollider in HitColliders)
        {
            HandleOverlapCollision(hitCollider);
        }

        PreviousFramePosition = CachedTransform.position;
    }

    protected override void HandleOverlapCollision(Collider2D hit)
    {
        if (hit.GetComponent<EnemyProjectile>() is { } enemyProjectile)
        {
            enemyProjectile.OnHit();
        }
        else if (hit.GetComponent<Enemy>() is { } enemy)
        {
            enemy.OnHit(Damage);
        }
    }
}