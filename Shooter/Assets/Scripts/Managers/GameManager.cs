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

    private bool HasPausedAnimators { get; set; }

    private void Awake()
    {
        GameData.Initialize();
        UIManager.Initialize();
    }

    private void Update()
    {
        GameData.Player.UpdatePlayerInput();
    }

    private void FixedUpdate()
    {
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
        ProjectileManager.UpdateProjectiles();
        EnemyManager.UpdateEnemies();
        CollectibleManager.UpdateUpdateables();
        GameData.Player.UpdateUpdateable();
    }
}