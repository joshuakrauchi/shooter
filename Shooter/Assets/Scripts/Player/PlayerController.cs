using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float HorizontalAxis { get; private set; }
    public float VerticalAxis { get; private set; }
    public bool IsShooting { get; private set; }
    public bool IsRewinding { get; private set; }
    public bool IsLeftMouseDown { get; private set; }

    public void UpdateInput()
    {
        HorizontalAxis = Input.GetAxisRaw("Horizontal");
        VerticalAxis = Input.GetAxisRaw("Vertical");
        IsShooting = Input.GetMouseButton(0);
        IsRewinding = Input.GetMouseButton(1);
        IsLeftMouseDown = Input.GetMouseButtonDown(0);
    }
}