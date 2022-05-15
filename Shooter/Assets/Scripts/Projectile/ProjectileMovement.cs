using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    [field: SerializeField] public float Speed { get; set; } = 0.25f;

    public MoveBehaviour[] MoveBehaviours { get; set; }
    public ref Vector2 Velocity => ref _velocity;
    public ref Quaternion Rotation => ref _rotation;

    private Vector2 _velocity;
    private Quaternion _rotation;
    private float _timeAlive;
    private uint _moveBehaviourIndex;

    public void Awake()
    {
        Rotation = transform.rotation;
    }

    public void UpdateMovement(bool isRewinding)
    {
        if (!isRewinding)
        {
            _timeAlive += Time.deltaTime;

            if (MoveBehaviours.Length == 0) return;

            while (_moveBehaviourIndex > 0 && MoveBehaviours[_moveBehaviourIndex - 1].StartTime > _timeAlive)
            {
                --_moveBehaviourIndex;
            }

            while (_moveBehaviourIndex + 1 < MoveBehaviours.Length && MoveBehaviours[_moveBehaviourIndex + 1].StartTime <= _timeAlive)
            {
                ++_moveBehaviourIndex;
            }

            MoveBehaviours[_moveBehaviourIndex].UpdateMove(ref Velocity, ref Rotation, Speed, _timeAlive);
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