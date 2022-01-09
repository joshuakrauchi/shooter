using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [field: SerializeField] public float MoveSpeed { get; private set; } = 10f;

    public void UpdateMovement()
    {
        var position = transform.position;
        var newPosition = Vector3.MoveTowards(position, GameManager.MainCamera.ScreenToWorldPoint(Input.mousePosition), MoveSpeed);

        // Lock new position to within screen bounds.
        if (newPosition.x < GameManager.ScreenRect.xMin)
        {
            newPosition.x = GameManager.ScreenRect.xMin;
        }
        else if (newPosition.x > GameManager.ScreenRect.xMax)
        {
            newPosition.x = GameManager.ScreenRect.xMax;
        }

        if (newPosition.y < GameManager.ScreenRect.yMin)
        {
            newPosition.y = GameManager.ScreenRect.yMin;
        }
        else if (newPosition.y > GameManager.ScreenRect.yMax)
        {
            newPosition.y = GameManager.ScreenRect.yMax;
        }

        newPosition.z = position.z;

        transform.position = newPosition;
    }
}