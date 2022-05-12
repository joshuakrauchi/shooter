using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private GameData gameData;
    [field: SerializeField] public float MoveSpeed { get; private set; } = 10f;

    public void UpdateMovement()
    {
        Vector3 position = transform.position;
        Vector3 newPosition = Vector3.MoveTowards(position, gameData.MainCamera.ScreenToWorldPoint(Input.mousePosition), MoveSpeed);

        // Lock new position to within screen bounds.
        if (newPosition.x < gameData.ScreenRect.xMin)
        {
            newPosition.x = gameData.ScreenRect.xMin;
        }
        else if (newPosition.x > gameData.ScreenRect.xMax)
        {
            newPosition.x = gameData.ScreenRect.xMax;
        }

        if (newPosition.y < gameData.ScreenRect.yMin)
        {
            newPosition.y = gameData.ScreenRect.yMin;
        }
        else if (newPosition.y > gameData.ScreenRect.yMax)
        {
            newPosition.y = gameData.ScreenRect.yMax;
        }

        newPosition.z = position.z;

        transform.position = newPosition;
    }
}