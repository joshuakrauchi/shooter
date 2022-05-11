using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Game Data")]
public class GameData: ScriptableObject
{
    [SerializeField] private float maxRewindCharge;
    [SerializeField] private uint shots;
    [SerializeField] private uint currency;
    [SerializeField] private uint projectileDamage;
    
    public Player Player { get; set; }
    public uint Currency { get; set; }
    public uint ProjectileDamage { get; set; }
    public uint Shots { get; set; }

    public float RewindCharge
    {
        get => _rewindCharge;
        set
        {
            _rewindCharge = value;

            if (_rewindCharge < 0f)
            {
                _rewindCharge = 0f;
            }
            else if (_rewindCharge > maxRewindCharge)
            {
                _rewindCharge = maxRewindCharge;
            }
        }
    }

    private float _rewindCharge;

    private void OnEnable()
    {
        Currency = currency;
        ProjectileDamage = projectileDamage;
        Shots = shots;
        RewindCharge = maxRewindCharge;
    }
}