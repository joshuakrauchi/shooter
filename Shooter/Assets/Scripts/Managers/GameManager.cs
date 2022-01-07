using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    [SerializeField] private ValueSlider rewindSlider;
    [SerializeField] private GameData gameData;
    public static UIManager UIManager;

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

    public static float RewindCharge
    {
        get => rewindCharge;
        set
        {
            rewindCharge = value;

            if (rewindCharge < 0f)
            {
                rewindCharge = 0f;
            }
            else if (rewindCharge > _maxRewindCharge)
            {
                rewindCharge = _maxRewindCharge;
            }
        }
    }

    public static float MaxRewindCharge
    {
        get => _maxRewindCharge;
        private set => _maxRewindCharge = value;
    }


    public static Player Player { get; private set; }
    public static Camera MainCamera { get; private set; }
    public static float Top { get; private set; }
    public static float Bottom { get; private set; }
    public static float Left { get; private set; }
    public static float Right { get; private set; }

    private static bool _isRewinding;

    public static bool IsRewinding
    {
        get => _isRewinding;
        set => _isRewinding = value && !UIManager.IsDisplayingDialogue;// && RewindCharge > 0f;
    }

    public static bool BossIsActive { get; set; }

    public static float ProjectileDamage { get; set; } = 1f;
    public static float Currency { get; set; }
    public static LevelManager CurrentLevelManager;
    private static bool _isPaused;

    public static bool IsPaused
    {
        get => _isPaused;
        set
        {
            _isPaused = value;
            EnemyManager.Instance.SetMinionAnimatorSpeed(IsPaused ? 0f : 1f);
        }
    }

    private static float _maxRewindCharge;
    private static float _levelTime;
    private static float rewindCharge = 10f;

    private void Awake()
    {
        Player = GameObject.FindWithTag("Player").GetComponent<Player>();
        UIManager = FindObjectOfType<UIManager>();

        MainCamera = Camera.main;

        if (MainCamera != null)
        {
            var topRight = MainCamera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
            Top = topRight.y;
            Right = topRight.x;
            var bottomLeft = MainCamera.ScreenToWorldPoint(Vector2.zero);
            Bottom = bottomLeft.y;
            Left = bottomLeft.x;
        }

        CurrentLevelManager = FindObjectOfType<LevelManager>();
        _maxRewindCharge = rewindCharge;
    }

    private void Start()
    {
        rewindSlider = FindObjectOfType<ValueSlider>();
        rewindSlider.SetMaxValue(RewindCharge);
        rewindSlider.SetValue(RewindCharge);
    }

    private void Update()
    {
        Player.UpdatePlayerInput();
    }

    private void FixedUpdate()
    {
        if (IsRewinding && !UIManager.IsDisplayingDialogue)
        {
            RewindCharge -= Time.deltaTime;
        }

        rewindSlider.SetValue(RewindCharge);

        if (IsPaused)
        {
            if (IsRewinding)
            {
                IsPaused = false;
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
        EnemyManager.Instance.UpdateEnemies();
        ProjectileManager.Instance.UpdateProjectiles();
        CollectibleManager.Instance.UpdateCollectibles();
        Player.UpdatePlayer();
    }

    public static void OnPlayerHit()
    {
        if (!IsRewinding && !IsPaused)
        {
            RewindCharge -= MaxRewindCharge / 10f;
            //IsPaused = true;
        }
    }
}