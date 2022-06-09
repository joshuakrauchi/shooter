using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public partial class PlayerSystem : SystemBase
{
    protected override void OnUpdate()
    {
        var movementSpeed = GameInfo.Instance.MovementSpeed;
        
        Vector3 mousePosition = Utilities.MainCamera.ScreenToWorldPoint(Input.mousePosition);

        Entities.WithAll<PlayerComponent>().ForEach((ref PlayerControllerComponent playerControllerComponent, ref Translation translation) =>
        {
            UpdateInput(ref playerControllerComponent);
            UpdateMovement(movementSpeed, mousePosition, ref translation);
        }).Run();
    }

    private static void UpdateInput(ref PlayerControllerComponent playerControllerComponent)
    {
        playerControllerComponent.isShootHeld = Input.GetMouseButton(0);
        playerControllerComponent.isRewindHeld = Input.GetMouseButton(1);
        playerControllerComponent.isConfirmDown = Input.GetMouseButtonDown(0);
        playerControllerComponent.isSpecialHeld = Input.GetKey(KeyCode.X);
        playerControllerComponent.scrollDelta = Input.mouseScrollDelta.y;

        if (Input.GetKeyDown(KeyCode.T))
        {
            Application.targetFrameRate = Application.targetFrameRate != 10 ? 10 : 60;
        }
    }

    private static void UpdateMovement(float movementSpeed, float3 mousePosition, ref Translation translation)
    {
        float3 newPosition = Vector3.MoveTowards(translation.Value, mousePosition, movementSpeed);
        
        // Lock new position to within screen bounds.
        /*if (newPosition.x < gameData.ScreenRect.xMin)
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
        }*/

        newPosition.z = 0.0f;

        translation.Value = newPosition;
    }
}