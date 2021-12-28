using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] private float initialVelocity = 1f;
    [SerializeField] private float gravityPower = -0.1f;

    private Vector2 _velocity;

    private void Awake()
    {
        _velocity = new Vector2(0f, initialVelocity);

        CollectibleManager.Instance.AddCollectible(this);
    }

    public void UpdateMovement()
    {
        _velocity.y += gravityPower;

        transform.Translate(_velocity);
    }

    public void DestroySelf()
    {
        CollectibleManager.Instance.RemoveCollectible(this);
        Destroy(gameObject);
    }
}