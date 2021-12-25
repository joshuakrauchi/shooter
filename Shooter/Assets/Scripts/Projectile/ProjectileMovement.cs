using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    public delegate void MovementPattern(ref Vector2 velocity, float xSpeed, float ySpeed, float timeAlive);

    [SerializeField] private float xSpeed;
    [SerializeField] private float ySpeed;

    public float XSpeed
    {
        get => xSpeed;
        private set => xSpeed = value;
    }

    public float YSpeed
    {
        get => ySpeed;
        private set => ySpeed = value;
    }

    public MovementPattern Pattern { get; set; }
    public ref Vector2 Velocity => ref _velocity;

    private Vector2 _velocity;
    private float _timeAlive;

    public void UpdateMovement()
    {
        if (!GameManager.IsRewinding)
        {
            _timeAlive += Time.deltaTime;
            Pattern?.Invoke(ref Velocity, XSpeed, YSpeed, _timeAlive);
            transform.Translate(Velocity);
        }
        else
        {
            _timeAlive -= Time.deltaTime;
        }
    }
}