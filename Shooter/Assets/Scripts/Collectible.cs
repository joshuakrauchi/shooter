using UnityEngine;

public class Collectible : MonoBehaviour, IUpdatable
{
    [SerializeField] private float initialVelocity;
    [SerializeField] private float gravityPower;
    [SerializeField] private uint value;
    [SerializeField] private UpdatableManager updatableManager;

    public float InitialVelocity
    {
        get => initialVelocity;
        private set => initialVelocity = value;
    }

    public float GravityPower
    {
        get => gravityPower;
        private set => gravityPower = value;
    }

    public uint Value
    {
        get => value;
        private set => this.value = value;
    }

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