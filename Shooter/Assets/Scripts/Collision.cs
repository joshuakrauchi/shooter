using UnityEngine;

public abstract class Collision : MonoBehaviour
{
    [field: SerializeField] public uint MaxOverlapHits { get; private set; } = 5;
    [field: SerializeField] protected ContactFilter2D OverlapFilter { get; private set; }

    public BoxCollider2D Collider { get; private set; }

    protected Collider2D[] OverlapHits { get; private set; }

    protected virtual void Awake()
    {
        Collider = GetComponent<BoxCollider2D>();
        OverlapHits = new Collider2D[MaxOverlapHits];
    }

    protected abstract void HandleOverlapCollision(Collider2D hit);

    public virtual void UpdateCollision()
    {
        Collider.OverlapCollider(OverlapFilter, OverlapHits);

        for (var i = 0; i < OverlapHits.Length; ++i)
        {
            if (OverlapHits[i] == default) break;

            HandleOverlapCollision(OverlapHits[i]);

            OverlapHits[i] = default;
        }
    }
}