using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "ScriptableObjects/GameData")]
public class GameData : ScriptableObject
{
    [SerializeField] private uint initialCurrency = 100;
    [SerializeField] private uint initialProjectileDamage = 1;
    [SerializeField] private float initialRewindCharge = 10.0f;
    [SerializeField] private uint initialShots = 1;

    public LevelManager CurrentLevelManager { get; set; }
    public Player Player { get; set; }
    public Camera MainCamera { get; private set; }
    public Rect ScreenRect { get; private set; }
    public float MaxRewindCharge { get; set; }
    public uint Currency { get; set; }
    public uint ProjectileDamage { get; set; }
    public uint Shots { get; set; }

    public float LevelTime
    {
        get => _levelTime;
        set
        {
            _levelTime = value;
            if (_levelTime < 0.0f)
            {
                _levelTime = 0.0f;
            }
        }
    }

    public float RewindCharge
    {
        get => _rewindCharge;
        set
        {
            _rewindCharge = value;

            if (_rewindCharge < 0.0f)
            {
                _rewindCharge = 0.0f;
            }
            else if (_rewindCharge > MaxRewindCharge)
            {
                _rewindCharge = MaxRewindCharge;
            }
        }
    }

    private float _levelTime;
    private float _rewindCharge;

    public void Initialize()
    {
        MainCamera = Camera.main;

        UpdateScreenRect();
    }

    public void UpdateScreenRect()
    {
        if (!MainCamera) return;
        
        Vector3 bottomLeft = MainCamera.ScreenToWorldPoint(Vector2.zero);
        Vector3 topRight = MainCamera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        
        ScreenRect = Rect.MinMaxRect(bottomLeft.x < 0 ? bottomLeft.x : -bottomLeft.x, bottomLeft.y < 0 ? bottomLeft.y : -bottomLeft.y, topRight.x > 0 ? topRight.x : -topRight.x, topRight.y > 0 ? topRight.y : -topRight.y);
    }

    private void OnEnable()
    {
        Currency = initialCurrency;
        LevelTime = 0.0f;
        ProjectileDamage = initialProjectileDamage;
        MaxRewindCharge = initialRewindCharge;
        RewindCharge = MaxRewindCharge;
        Shots = initialShots;
    }
}