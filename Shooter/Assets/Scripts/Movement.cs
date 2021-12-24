using UnityEngine;

public abstract class Movement : MonoBehaviour
{
    [SerializeField] private float xSpeed;
    [SerializeField] private float ySpeed;

    public float XSpeed
    {
        get => xSpeed;
        set => xSpeed = value;
    }

    public float YSpeed
    {
        get => ySpeed;
        set => ySpeed = value;
    }

    public ref Vector2 Velocity => ref _velocity;

    private Vector2 _velocity;

    public abstract void UpdateMovement();
}