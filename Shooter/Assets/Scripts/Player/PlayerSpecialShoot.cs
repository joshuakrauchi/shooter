using UnityEngine;

public class PlayerSpecialShoot : MonoBehaviour
{
    private PlayerSpecial[] PlayerSpecials { get; set; }
    private int CurrentPlayerSpecial { get; set; }

    private void Awake()
    {
        PlayerSpecials = GetComponents<PlayerSpecial>();
    }

    public void CyclePlayerSpecial(bool forwards)
    {
        if (PlayerSpecials.Length <= 0) return;

        CurrentPlayerSpecial += forwards ? 1 : -1;

        if (CurrentPlayerSpecial < 0)
        {
            CurrentPlayerSpecial = PlayerSpecials.Length - 1;
        }
        else if (CurrentPlayerSpecial >= PlayerSpecials.Length)
        {
            CurrentPlayerSpecial = 0;
        }
    }

    public void UpdateSpecialShoot(bool isShooting)
    {
        if (PlayerSpecials.Length <= 0) return;
        
        PlayerSpecials[CurrentPlayerSpecial].UpdateShoot(isShooting);
    }
}