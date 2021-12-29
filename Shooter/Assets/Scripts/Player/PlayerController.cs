using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool IsShooting { get; private set; }
    public bool IsRewinding { get; private set; }
    public bool IsLeftMouseDown { get; private set; }

    public void UpdateInput()
    {
        IsShooting = Input.GetMouseButton(0);
        IsRewinding = Input.GetMouseButton(1);
        IsLeftMouseDown = Input.GetMouseButtonDown(0);

        if (Input.GetKeyDown(KeyCode.R))
        {
            ProjectileManager.Instance.ClearProjectiles();
        }
    }
}