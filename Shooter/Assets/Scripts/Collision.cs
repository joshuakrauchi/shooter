using UnityEngine;

public abstract class Collision : MonoBehaviour
{
    [SerializeField] protected ContactFilter2D overlapFilter;
    [SerializeField] protected uint maxOverlapHits = 5;

    public BoxCollider2D Collider { get; private set; }

    public uint MaxOverlapHits
    {
        get => maxOverlapHits;
        private set => maxOverlapHits = value;
    }

    protected Collider2D[] OverlapHits;

    protected virtual void Awake()
    {
        Collider = GetComponent<BoxCollider2D>();
        OverlapHits = new Collider2D[MaxOverlapHits];
    }

    protected abstract void HandleOverlapCollision(Collider2D hit);

    public virtual void UpdateCollision()
    {
        Collider.OverlapCollider(overlapFilter, OverlapHits);

        for (var i = 0; i < OverlapHits.Length; ++i)
        {
            if (OverlapHits[i] == default) break;

            HandleOverlapCollision(OverlapHits[i]);

            OverlapHits[i] = default;
        }
    }
}