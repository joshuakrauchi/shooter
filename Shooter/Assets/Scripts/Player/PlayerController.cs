using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [field: SerializeField] private ProjectileManager ProjectileManager { get; set; }

    public bool IsShooting { get; private set; }
    public bool IsRewinding { get; private set; }
    public bool IsConfirmDown { get; private set; }

    public void UpdateInput()
    {
        IsShooting = Input.GetMouseButton(0);
        IsRewinding = Input.GetMouseButton(1);
        IsConfirmDown = Input.GetMouseButtonDown(0);

        if (Input.GetKeyDown(KeyCode.R))
        {
            ProjectileManager.ClearProjectiles();
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            Application.targetFrameRate = Application.targetFrameRate == 60 ? 10 : 60;
        }
    }
}