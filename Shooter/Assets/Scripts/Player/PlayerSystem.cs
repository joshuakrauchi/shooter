using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public partial class PlayerSystem : SystemBase
{
    private static Timer ShootTimer { get; set; }

    protected override void OnCreate()
    {
        base.OnCreate();

        ShootTimer = new Timer(GameInfo.Instance.ShootDelay);
    }

    protected override void OnUpdate()
    {
        var deltaTime = Time.DeltaTime;
        var isRewinding = GameManager.isRewinding;
        var numberOfShots = GameInfo.Instance.NumberOfShots;
        var movementSpeed = GameInfo.Instance.MovementSpeed;
        
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        ShootTimer.UpdateTime(isRewinding);
        var isShootTimerFinished = ShootTimer.IsFinished(false);

        Entities.ForEach((ref PlayerComponent playerComponent, ref PlayerControllerComponent playerControllerComponent, ref Translation translation, ref EntitySpawnComponent entitySpawnComponent, ref DynamicBuffer<EntitySpawnBufferElement> entitySpawnBufferElements) =>
        {
            UpdateInput(ref playerControllerComponent);
            UpdateMovement(movementSpeed, mousePosition, ref translation);
            UpdateShoot(isShootTimerFinished, numberOfShots, ref translation, ref playerControllerComponent, ref entitySpawnComponent, ref entitySpawnBufferElements);
        }).Run();

        if (isShootTimerFinished)
        {
            ShootTimer.Reset();
        }
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

    private static void UpdateShoot(bool isShootTimerFinished, float numberOfShots, ref Translation translation, ref PlayerControllerComponent playerControllerComponent, ref EntitySpawnComponent entitySpawnComponent, ref DynamicBuffer<EntitySpawnBufferElement> entitySpawnBufferElements)
    {
        if (!isShootTimerFinished || !playerControllerComponent.isShootHeld) return;
        
        for (var i = 0; i < numberOfShots; ++i)
        {
            entitySpawnBufferElements.Add(new EntitySpawnBufferElement
            {
                Entity = entitySpawnComponent.entity,
                Translation = translation,
                Rotation = new Rotation
                {
                    Value = quaternion.Euler(0.0f, 0.0f, math.PI / 2.0f)
                }
            });
        }
    }
}