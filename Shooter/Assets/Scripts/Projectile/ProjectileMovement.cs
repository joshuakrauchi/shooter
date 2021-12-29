using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    [SerializeField] private float speed;

    public float Speed
    {
        get => speed;
        set => speed = value;
    }

    public MoveBehaviour MoveBehaviour { get; set; }
    public ref Vector2 Velocity => ref _velocity;

    private Vector2 _velocity;
    private float _timeAlive;

    public void UpdateMovement()
    {
        if (!GameManager.IsRewinding)
        {
            _timeAlive += Time.deltaTime;
            MoveBehaviour.UpdateMove(ref Velocity, Speed, _timeAlive);
            transform.Translate(Velocity);
        }
        else
        {
            _timeAlive -= Time.deltaTime;
        }
    }
}