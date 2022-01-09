using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameData gameData;
    [SerializeField] private UpdatableManager collectibleManager;
    [SerializeField] private EnemyManager enemyManager;
    [SerializeField] private ProjectileManager projectileManager;
    [SerializeField] private GameState gameState;

    public static Player Player { get; private set; }
    public static Camera MainCamera { get; private set; }
    public static Rect ScreenRect { get; private set; }
    public static UIManager UIManager { get; private set; }

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

    private static bool _isRewinding;

    public static bool IsRewinding
    {
        get => _isRewinding;
        set => _isRewinding = value && !UIManager.IsDisplayingDialogue;// && RewindCharge > 0f;
    }

    public static bool BossIsActive { get; set; }
    public static float ProjectileDamage { get; set; } = 1f;
    public static LevelManager CurrentLevelManager;

    private bool enemiesArePaused;

    private static float _levelTime;
    private ValueSlider rewindSlider;

    private void Awake()
    {
        Player = GameObject.FindWithTag("Player").GetComponent<Player>();
        UIManager = FindObjectOfType<UIManager>();

        MainCamera = Camera.main;

        if (MainCamera != null)
        {
            var bottomLeft = MainCamera.ScreenToWorldPoint(Vector2.zero);
            var topRight = MainCamera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
            ScreenRect = new Rect(bottomLeft.x, bottomLeft.y, topRight.x * 2, topRight.y * 2);
        }

        CurrentLevelManager = FindObjectOfType<LevelManager>();
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
        if (IsRewinding && !UIManager.IsDisplayingDialogue)
        {
            gameData.RewindCharge -= Time.deltaTime;
        }

        rewindSlider.SetValue(gameData.RewindCharge);

        enemyManager.SetMinionAnimatorSpeed(gameState.IsPaused ? 0f : 1f);

        if (gameState.IsPaused)
        {
            if (IsRewinding)
            {
                gameState.IsPaused = false;
            }
            else
            {
                return;
            }
        }

        if (!BossIsActive)
        {
            LevelTime += IsRewinding ? -Time.deltaTime : Time.deltaTime;
        }

        CurrentLevelManager.UpdateEnemyCreation();
        enemyManager.UpdateEnemies();
        projectileManager.UpdateProjectiles();
        collectibleManager.UpdateUpdatables();
        Player.UpdateUpdatable();
    }

    public static void OnPlayerHit()
    {
        //if (!IsRewinding && !GameState.IsPaused)
        {
            //RewindCharge -= MaxRewindCharge / 10f;
            //IsPaused = true;
        }
    }
}