using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [field: SerializeField] public float MoveSpeed { get; private set; } = 10f;

    public void UpdateMovement()
    {
        var position = transform.position;
        var newPosition = Vector3.MoveTowards(position, GameManager.MainCamera.ScreenToWorldPoint(Input.mousePosition), MoveSpeed);

        // Lock new position to within screen bounds.
        if (newPosition.x < GameManager.Left)
        {
            newPosition.x = GameManager.Left;
        }
        else if (newPosition.x > GameManager.Right)
        {
            newPosition.x = GameManager.Right;
        }

        if (newPosition.y < GameManager.Bottom)
        {
            newPosition.y = GameManager.Bottom;
        }
        else if (newPosition.y > GameManager.Top)
        {
            newPosition.y = GameManager.Top;
        }

        newPosition.z = position.z;

        transform.position = newPosition;
    }
}