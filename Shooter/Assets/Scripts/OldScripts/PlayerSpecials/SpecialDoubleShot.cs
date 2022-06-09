using UnityEngine;

/**
 * Takes in a container GameObject holding two SpecialRays,
 * one representing the left side ray, and one representing the right side ray.
 */
public class SpecialDoubleShot : PlayerSpecial
{
    [field: SerializeField] private GameObject SpecialContainer { get; set; }
    
    private PlayerSpecial LeftSpecial { get; set; }
    private PlayerSpecial RightSpecial { get; set; }
    
    private void Awake()
    {
        GameObject specialContainer = Instantiate(SpecialContainer, transform);
        var specials = specialContainer.GetComponents<PlayerSpecial>();
        LeftSpecial = specials[0];
        RightSpecial = specials[1];
    }

    public override void UpdateShoot(bool isShooting)
    {
        LeftSpecial.UpdateShoot(isShooting);
        RightSpecial.UpdateShoot(isShooting);
    }
}