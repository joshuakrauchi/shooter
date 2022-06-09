using UnityEngine;

public abstract class Collision : MonoBehaviour
{
    [field: SerializeField] public uint MaxOverlapHits { get; private set; } = 5;
    [field: SerializeField] protected ContactFilter2D OverlapFilter { get; private set; }

    public Collider Collider { get; private set; }

    protected Collider[] OverlapHits { get; private set; }

    protected virtual void Awake()
    {
        Collider = GetComponent<Collider>();
        OverlapHits = new Collider[MaxOverlapHits];
    }

    protected abstract void HandleOverlapCollision(Collider hit);

    public virtual void UpdateCollision()
    {
        Physics.OverlapSphereNonAlloc(transform.position, 1.0f, OverlapHits);

        for (var i = 0; i < OverlapHits.Length; ++i)
        {
            if (OverlapHits[i] == default) break;

            HandleOverlapCollision(OverlapHits[i]);

            OverlapHits[i] = default;
        }
    }
}