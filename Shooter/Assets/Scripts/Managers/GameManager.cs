using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameData gameData;
    [SerializeField] private GameState gameState;
    [SerializeField] private EnemyManager enemyManager;
    [SerializeField] private ProjectileManager projectileManager;
    [SerializeField] private UpdatableManager collectibleManager;

    public static Camera MainCamera { get; private set; }
    public static LevelManager CurrentLevelManager { get; set; }
    public static Player Player { get; set; }
    public static Rect ScreenRect { get; private set; }
    public static UIManager UIManager { get; set; }

    public static float LevelTime
    {
        get => _levelTime;
        private set
        {
            _levelTime = value;
            if (_levelTime < 0f)
            {
                _levelTime = 0f;
            }
        }
    }

    private static float _levelTime;
    private bool enemiesArePaused;
    private ValueSlider rewindSlider;

    private void Awake()
    {
        MainCamera = Camera.main;

        if (MainCamera != null)
        {
            var bottomLeft = MainCamera.ScreenToWorldPoint(Vector2.zero);
            var topRight = MainCamera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
            ScreenRect = Rect.MinMaxRect(bottomLeft.x, bottomLeft.y, topRight.x, topRight.y);
        }
    }

    private void Start()
    {
        rewindSlider = UIManager.RewindBar.GetComponent<ValueSlider>();
        rewindSlider.SetMaxValue(gameData.RewindCharge);
        rewindSlider.SetValue(gameData.RewindCharge);
    }

    private void Update()
    {
        Player.UpdatePlayerInput();
    }

    private void FixedUpdate()
    {
        if (gameState.IsRewinding && !gameState.IsDisplayingDialogue)
        {
            gameData.RewindCharge -= Time.deltaTime;
        }

        rewindSlider.SetValue(gameData.RewindCharge);

        enemyManager.SetMinionAnimatorSpeed(gameState.IsPaused ? 0f : 1f);

        if (gameState.IsPaused)
        {
            if (gameState.IsRewinding)
            {
                gameState.IsPaused = false;
            }
            else
            {
                return;
            }
        }

        if (!gameState.BossIsActive)
        {
            LevelTime += gameState.IsRewinding ? -Time.deltaTime : Time.deltaTime;
        }

        CurrentLevelManager.UpdateEnemyCreation();
        enemyManager.UpdateEnemies();
        projectileManager.UpdateProjectiles();
        collectibleManager.UpdateUpdatables();
        Player.UpdateUpdatable();
    }
}