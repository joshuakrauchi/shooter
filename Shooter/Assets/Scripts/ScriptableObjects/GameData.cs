using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Game Data")]
public class GameData: ScriptableObject
{
    [SerializeField] private float maxRewindCharge;
    [SerializeField] private uint shots;
    [SerializeField] private uint currency;

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

    public uint Shots { get; set; }
    public uint Currency { get; set; }
    public float ProjectileDamage { get; set; } = 1f;

    private float _rewindCharge;

    private void OnEnable()
    {
        RewindCharge = maxRewindCharge;
        Shots = shots;
        Currency = currency;
    }
}