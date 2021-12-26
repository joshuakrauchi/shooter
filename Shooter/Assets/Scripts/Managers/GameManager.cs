using System;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Canvas dialogueBox;
    [SerializeField] private Text header;
    [SerializeField] private Text text;

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

    public static GameObject Player { get; private set; }
    public static Camera MainCamera { get; private set; }
    public static float Top { get; private set; }
    public static float Bottom { get; private set; }
    public static float Left { get; private set; }
    public static float Right { get; private set; }
    public static bool IsRewinding { get; set; }
    public static float ProjectileDamage { get; set; } = 1f;
    public static bool IsPaused { get; set; }

    private void Awake()
    {
        Player = GameObject.FindWithTag("Player");

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
    }

    private void Update()
    {
        if (IsPaused) return;

        if (IsRewinding)
        {
            LevelTime -= Time.deltaTime;
        }
        else
        {
            LevelTime += Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        if (IsPaused) return;

        EnemyManager.Instance.UpdateEnemies();
        ProjectileManager.Instance.UpdateProjectiles();
    }

    public static void OnPlayerHit()
    {
        if (!IsRewinding)
        {
            //UIManager.Instance.StartDialogue(new []{Tuple.Create("God", "YOU DIED")});
        }
    }
}