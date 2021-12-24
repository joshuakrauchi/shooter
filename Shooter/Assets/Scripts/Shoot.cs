using UnityEngine;

public abstract class Shoot : MonoBehaviour
{
    [SerializeField] private float shootDelay = 1f;

    public float ShootDelay
    {
        get => shootDelay;
        set => shootDelay = value;
    }

    public abstract void UpdateShoot();
}