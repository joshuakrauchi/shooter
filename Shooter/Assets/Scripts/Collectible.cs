using UnityEngine;

public class Collectible : MonoBehaviour, IUpdatable
{
    [field: SerializeField] public float InitialVelocity { get; private set; }
    [field: SerializeField] public float GravityPower { get; private set; }
    [field: SerializeField] public uint Value { get; private set; }
    [SerializeField] private UpdatableManager updatableManager;

    private Vector2 _velocity;

    private void Awake()
    {
        _velocity = new Vector2(0f, InitialVelocity);

        updatableManager.AddCollectible(this);
    }

    public void UpdateUpdatable()
    {
        _velocity.y += GravityPower;

        transform.Translate(_velocity);
    }

    public void DestroySelf()
    {
        updatableManager.RemoveCollectible(this);
        Destroy(gameObject);
    }
}