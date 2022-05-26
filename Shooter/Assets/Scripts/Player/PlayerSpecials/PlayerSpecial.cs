using UnityEngine;

public abstract class PlayerSpecial : MonoBehaviour
{
    [field: SerializeField] protected GameData GameData { get; set; }
    
    [field: SerializeField] public uint CurrencyCost { get; private set; } = 100;
    [field: SerializeField] public float ChargeCost { get; private set; } = 1.0f;

    public abstract void UpdateShoot(bool isShooting);

    protected bool CanShoot() => ChargeCost <= GameData.SpecialCharge;

    protected void DecreaseSpecialCharge()
    {
        GameData.SpecialCharge -= ChargeCost;
    }
}