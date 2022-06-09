using Unity.Entities;
using UnityEngine;

/**
 * The GameManager handles updating every IUpdateable object based on the
 * game's state, such as if the game is paused or the player is rewinding.
 * It also initializes some values in ScriptableObjects.
 */
public class GameManager : MonoBehaviour
{
    [field: SerializeField] private GameData GameData { get; set; }
    [field: SerializeField] private GameState GameState { get; set; }
    [field: SerializeField] private UIManager UIManager { get; set; }
    [field: SerializeField] private EnemyManager EnemyManager { get; set; }
    [field: SerializeField] private ProjectileManager ProjectileManager { get; set; }
    [field: SerializeField] private UpdateableManager CollectibleManager { get; set; }

    public static bool isRewinding;
    
    private bool HasPausedAnimators { get; set; }

    private void Start()
    {
        GameData.Initialize();
        UIManager.Initialize();
    }
    
    private void Update()
    {
        //GameData.Player.UpdatePlayerInput();
    }

    private void FixedUpdate()
    {
        // For later fun
        /*
        var rotation = GameData.MainCamera.transform.rotation;
        GameData.MainCamera.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotation.eulerAngles.z + 0.25f);
        
        GameData.UpdateScreenRect();
        */

        EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        var playerControllerComponent = entityManager.GetComponentData<PlayerControllerComponent>(GameInfo.Instance.PlayerEntity);
        isRewinding = GameState.IsRewinding || playerControllerComponent.isRewindHeld;
        
        if ((GameState.IsPaused || GameState.IsRewinding) && !HasPausedAnimators)
        {
            EnemyManager.SetMinionAnimatorSpeed(0.0f);
            HasPausedAnimators = true;
        }
        
        if (GameState.IsPaused)
        {
            if (GameState.IsRewinding && !GameState.IsDisplayingDialogue)
            {
                GameState.IsPaused = false;
            }
            else return;
        }

        if (GameState.IsRewinding)
        {
            GameData.RewindCharge -= Time.deltaTime;
        }

        if (!GameState.IsPaused && !GameState.IsRewinding && HasPausedAnimators)
        {
            EnemyManager.SetMinionAnimatorSpeed(1.0f);
            HasPausedAnimators = false;
        }

        if (!GameState.IsBossActive)
        {
            GameData.LevelTime += GameState.IsRewinding ? -Time.deltaTime : Time.deltaTime;
        }
        
        GameData.CurrentLevelManager.UpdateEnemyCreation();
        UIManager.UpdateUpdateable();
        
        EnemyManager.UpdateEnemies();
        CollectibleManager.UpdateUpdateables();
    }
}