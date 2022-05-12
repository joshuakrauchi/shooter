using UnityEngine;

public class Collectible : MonoBehaviour, IUpdateable
{
    [field: SerializeField] public float InitialVelocity { get; private set; }
    [field: SerializeField] public float GravityPower { get; private set; }
    [field: SerializeField] public uint Value { get; private set; }
    [SerializeField] private UpdateableManager updateableManager;

    private Vector2 _velocity;

    private void Awake()
    {
        _velocity = new Vector2(0.0f, InitialVelocity);

        updateableManager.AddUpdateable(this);
    }

    public void UpdateUpdateable()
    {
        _velocity.y += GravityPower;

        transform.Translate(_velocity);
    }

    public void DestroySelf()
    {
        updateableManager.RemoveUpdateable(this);
        Destroy(gameObject);
    }
}