using UnityEngine;

public class PlayerMovement : Movement
{
    public override void UpdateMovement()
    {
        if (!Camera.main) return;
        transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}