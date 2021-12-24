using UnityEngine;

public abstract class Collision : MonoBehaviour
{
    [SerializeField] protected ContactFilter2D overlapFilter;

    public BoxCollider2D Collider { get; private set; }

    protected Collider2D[] OverlapHits;
    protected const int MaxOverlapHits = 5;

    protected virtual void Awake()
    {
        Collider = GetComponent<BoxCollider2D>();
        OverlapHits = new Collider2D[MaxOverlapHits];
    }

    protected abstract void HandleOverlapCollision(Collider2D hit);

    public void UpdateCollision()
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