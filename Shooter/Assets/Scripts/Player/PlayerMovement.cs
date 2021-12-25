using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public void UpdateMovement()
    {
        transform.position = (Vector2)GameManager.MainCamera.ScreenToWorldPoint(Input.mousePosition);
    }
}