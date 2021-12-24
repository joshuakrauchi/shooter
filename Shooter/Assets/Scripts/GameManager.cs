using UnityEngine;

public class GameManager : MonoBehaviour
{
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
        
        if (Camera.main != null)
        {
            var topRight = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
            Top = topRight.y;
            Right = topRight.x;
            var bottomLeft = Camera.main.ScreenToWorldPoint(Vector2.zero);
            Bottom = bottomLeft.y;
            Left = bottomLeft.x;
        }
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

    public static void Die()
    {
        Debug.Log("Hit!");
    }

    public static void InitiateDialogue()
    {
        IsPaused = true;
        
    }
}