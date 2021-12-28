using System;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Canvas dialogueBox;
    [SerializeField] private Text header;
    [SerializeField] private Text text;
    [SerializeField] private ValueBar rewindCharge;

    private static float _levelTime;

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

    public static Player Player { get; private set; }
    public static Camera MainCamera { get; private set; }
    public static float Top { get; private set; }
    public static float Bottom { get; private set; }
    public static float Left { get; private set; }
    public static float Right { get; private set; }
    public static bool IsRewinding { get; set; }
    public static float ProjectileDamage { get; set; } = 1f;

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

    public static LevelManager CurrentLevelManager;

    private void Awake()
    {
        Player = GameObject.FindWithTag("Player").GetComponent<Player>();

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

        UIManager.Instance.Header = header;
        UIManager.Instance.Text = text;
        UIManager.Instance.DialogueBox = dialogueBox;

        CurrentLevelManager = FindObjectOfType<LevelManager>();
    }

    private void Start()
    {
        rewindCharge.SetMaxValue(Player.RewindCharge);
        rewindCharge.SetValue(Player.RewindCharge);
    }

    private void Update()
    {
        Player.UpdatePlayerInput();
    }

    private void FixedUpdate()
    {
        Player.UpdatePlayerCollision();
        rewindCharge.SetValue(Player.RewindCharge);

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

        LevelTime += IsRewinding ? -Time.deltaTime : Time.deltaTime;

        CurrentLevelManager.UpdateEnemyCreation();
        EnemyManager.Instance.UpdateEnemies();
        ProjectileManager.Instance.UpdateProjectiles();
        CollectibleManager.Instance.UpdateCollectibles();
        Player.UpdatePlayerMovementAndShoot();
    }

    public static void OnPlayerHit()
    {
        if (!IsRewinding && !IsPaused)
        {
            Player.RewindCharge -= Player.MaxRewindCharge / 10f;
            IsPaused = true;
        }
    }

    public static void OnCollectibleHit()
    {
        ++Player.Currency;
        Debug.Log(Player.Currency);
    }
}