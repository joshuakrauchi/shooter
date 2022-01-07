using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    [SerializeField] private float speed;

    public float Speed
    {
        get => speed;
        set => speed = value;
    }

    public MovePair[] MovePairs { get; set; }
    public ref Vector2 Velocity => ref _velocity;
    public ref Quaternion Rotation => ref _rotation;

    private Vector2 _velocity;
    private Quaternion _rotation;
    private float _timeAlive;
    private uint _moveIndex;

    public void Awake()
    {
        Rotation = transform.rotation;
    }

    public void UpdateMovement()
    {
        if (!GameManager.IsRewinding)
        {
            _timeAlive += Time.deltaTime;

            if (MovePairs.Length == 0) return;

            while (_moveIndex > 0 && MovePairs[_moveIndex - 1].StartTime > _timeAlive)
            {
                --_moveIndex;
            }

            while (_moveIndex + 1 < MovePairs.Length && MovePairs[_moveIndex + 1].StartTime <= _timeAlive)
            {
                ++_moveIndex;
            }

            MovePairs[_moveIndex].MoveBehaviour.UpdateMove(ref Velocity, ref Rotation, Speed, _timeAlive);
            transform.Translate(Velocity);
            transform.rotation = Rotation;
        }
        else
        {
            _timeAlive -= Time.deltaTime;

            if (_timeAlive < 0)
            {
                GetComponent<Projectile>().DestroyProjectile();
            }
        }
    }
}