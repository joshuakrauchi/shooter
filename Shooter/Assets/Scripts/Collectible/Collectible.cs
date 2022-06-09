using UnityEngine;

public class Collectible : MonoBehaviour, IUpdateable
{
    [field: SerializeField] public uint Value { get; private set; }

    [field: SerializeField] private UpdateableManager UpdateableManager { get; set; }
    [field: SerializeField] private float InitialVelocity { get; set; } = 0.4f;
    [field: SerializeField] private float TerminalVelocity { get; set; } = 0.1f;
    [field: SerializeField] private float GravityPower { get; set; } = -0.01f;

    private Vector2 _velocity;

    private void Awake()
    {
        _velocity = new Vector2(0.0f, InitialVelocity);

        UpdateableManager.AddUpdateable(this);
    }

    public void UpdateUpdateable()
    {
        return;
        
        _velocity.y += GravityPower * Time.deltaTime;

        if (_velocity.y < TerminalVelocity)
        {
            _velocity.y = TerminalVelocity;
        }

        transform.Translate(_velocity * Time.deltaTime);
    }

    public void DestroySelf()
    {
        //UpdateableManager.RemoveUpdateable(this);
        //Destroy(gameObject);
    }
}