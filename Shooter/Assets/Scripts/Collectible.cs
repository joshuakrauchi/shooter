using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] private float initialVelocity;
    [SerializeField] private float gravityPower;
    [SerializeField] private uint value;

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

        CollectibleManager.Instance.AddCollectible(this);
    }

    public void UpdateMovement()
    {
        _velocity.y += GravityPower;

        transform.Translate(_velocity);
    }

    public void DestroySelf()
    {
        CollectibleManager.Instance.RemoveCollectible(this);
        Destroy(gameObject);
    }
}