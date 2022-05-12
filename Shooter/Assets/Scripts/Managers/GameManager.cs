using UnityEngine;

/**
 * The GameManager handles updating every IUpdateable object based on the
 * game's state, such as if the game is paused or the player is rewinding.
 * It also initializes some values in ScriptableObjects.
 */
public class GameManager : MonoBehaviour
{
    [SerializeField] private GameData gameData;
    [SerializeField] private GameState gameState;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private EnemyManager enemyManager;
    [SerializeField] private ProjectileManager projectileManager;
    [SerializeField] private UpdateableManager collectibleManager;

    private bool _hasPausedAnimators;

    private void Awake()
    {
        gameData.Initialize();
        uiManager.Initialize();
    }

    private void Update()
    {
        gameData.Player.UpdatePlayerInput();
    }

    private void FixedUpdate()
    {
        if ((gameState.IsPaused || gameState.IsRewinding) && !_hasPausedAnimators)
        {
            enemyManager.SetMinionAnimatorSpeed(0.0f);
            _hasPausedAnimators = true;
        }
        
        if (gameState.IsPaused)
        {
            if (gameState.IsRewinding)
            {
                gameState.IsPaused = false;
            }
            else return;
        }

        if (gameState.IsRewinding)
        {
            gameData.RewindCharge -= Time.deltaTime;
        }
        
        if (!gameState.IsPaused && !gameState.IsRewinding && _hasPausedAnimators)
        {
            enemyManager.SetMinionAnimatorSpeed(1.0f);
            _hasPausedAnimators = false;
        }

        if (!gameState.IsBossActive)
        {
            gameData.LevelTime += gameState.IsRewinding ? -Time.deltaTime : Time.deltaTime;
        }

        gameData.CurrentLevelManager.UpdateEnemyCreation();
        projectileManager.UpdateProjectiles();
        enemyManager.UpdateEnemies();
        collectibleManager.UpdateUpdateables();
        gameData.Player.UpdateUpdateable();
    }
}