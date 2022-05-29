using UnityEngine;

public class Collectible : MonoBehaviour, IUpdateable
{
    [field: SerializeField] public float InitialVelocity { get; private set; }
    [field: SerializeField] public float GravityPower { get; private set; }
    [field: SerializeField] public uint Value { get; private set; }
    
    [field: SerializeField] private UpdateableManager UpdateableManager { get; set; }

    private Vector2 _velocity;

    private void Awake()
    {
        _velocity = new Vector2(0.0f, InitialVelocity);

        UpdateableManager.AddUpdateable(this);
    }

    public void UpdateUpdateable()
    {
        _velocity.y += GravityPower;

        transform.Translate(_velocity);
    }

    public void DestroySelf()
    {
        UpdateableManager.RemoveUpdateable(this);
        Destroy(gameObject);
    }
}