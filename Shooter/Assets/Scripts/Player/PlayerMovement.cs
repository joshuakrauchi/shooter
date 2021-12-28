using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    public float MoveSpeed
    {
        get => moveSpeed;
        private set => moveSpeed = value;
    }

    public void UpdateMovement()
    {
        var newPosition = Vector2.MoveTowards(transform.position, GameManager.MainCamera.ScreenToWorldPoint(Input.mousePosition), moveSpeed);

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

        transform.position = newPosition;
    }
}