using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "ScriptableObjects/GameData")]
public class GameData : ScriptableObject
{
    [field: SerializeField] private bool ResetValues { get; set; } = true;
    [field: SerializeField] private uint InitialCurrency { get; set; } = 100;
    [field: SerializeField] private uint InitialProjectileDamage { get; set; } = 1;
    [field: SerializeField] private float InitialRewindCharge { get; set; } = 10.0f;
    [field: SerializeField] private float InitialSpecialCharge { get; set; } = 10.0f;
    [field: SerializeField] private uint InitialShots { get; set; } = 1;
    [field: SerializeField] private float InitialShootDelay { get; set; } = 0.1f;

    public LevelManager CurrentLevelManager { get; set; }
    public Player Player { get; set; }
    public Camera MainCamera { get; private set; }
    public Rect ScreenRect { get; private set; }
    public uint Currency { get; set; }
    public uint ProjectileDamage { get; set; }
    public uint NumberOfShots { get; set; }
    public float ShootDelay { get; set; }
    public uint NextLevelExperience { get; set; } = 100;

    public uint Experience
    {
        get => _experience;
        set
        {
            _experience = value;

            while (_experience >= NextLevelExperience)
            {
                LevelUp();
            }
        }
    }

    public float MaxRewindCharge
    {
        get => _maxRewindCharge;
        set
        {
            _maxRewindCharge = value;
            _rewindCharge = new LockedFloat(_rewindCharge.Value, 0.0f, _maxRewindCharge);
        }
    }

    public float MaxSpecialCharge
    {
        get => _maxSpecialCharge;
        set
        {
            _maxSpecialCharge = value;
            _specialCharge = new LockedFloat(_specialCharge.Value, 0.0f, _maxSpecialCharge);
        }
    }

    public float RewindCharge
    {
        get => _rewindCharge.Value;
        set => _rewindCharge.Value = value;
    }

    public float SpecialCharge
    {
        get => _specialCharge.Value;
        set => _specialCharge.Value = value;
    }

    public float LevelTime
    {
        get => _levelTime.Value;
        set => _levelTime.Value = value;
    }

    private uint _experience;
    private float _maxRewindCharge;
    private float _maxSpecialCharge;
    private LockedFloat _rewindCharge;
    private LockedFloat _specialCharge;
    private LockedFloat _levelTime;

    public void Initialize()
    {
        MainCamera = Camera.main;

        UpdateScreenRect();

        InitializeValues();
        Player.UpdateStats();
    }

    public void UpdateScreenRect()
    {
        if (!MainCamera) return;

        Vector3 bottomLeft = MainCamera.ScreenToWorldPoint(Vector2.zero);
        Vector3 topRight = MainCamera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        ScreenRect = Rect.MinMaxRect(
            bottomLeft.x < 0 ? bottomLeft.x : -bottomLeft.x,
            bottomLeft.y < 0 ? bottomLeft.y : -bottomLeft.y,
            topRight.x > 0 ? topRight.x : -topRight.x,
            topRight.y > 0 ? topRight.y : -topRight.y);
    }

    private void OnEnable()
    {
        if (ResetValues)
        {
            InitializeValues();
        }
    }

    private void InitializeValues()
    {
        Experience = 0;
        NextLevelExperience = 100;
        Currency = InitialCurrency;
        ProjectileDamage = InitialProjectileDamage;
        MaxRewindCharge = InitialRewindCharge;
        MaxSpecialCharge = InitialSpecialCharge;
        NumberOfShots = InitialShots;
        ShootDelay = InitialShootDelay;

        RewindCharge = MaxRewindCharge;
        SpecialCharge = MaxSpecialCharge;
        _levelTime = new LockedFloat(0.0f, 0.0f, float.MaxValue);
    }

    private void LevelUp()
    {
        Debug.Log("LEVEL UP!");
        ShootDelay -= 0.05f;
        NumberOfShots += 1;
        ProjectileDamage += 1;
        
        Player.UpdateStats();
        
        NextLevelExperience *= 2;
    }
}