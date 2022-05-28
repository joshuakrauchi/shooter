using UnityEngine;

/**
 * Takes in a container GameObject holding two SpecialRays,
 * one representing the left side ray, and one representing the right side ray.
 */
public class SpecialSideRays : PlayerSpecial
{
    [field: SerializeField] private GameObject SpecialSideRaysContainer { get; set; }
    
    private SpecialRay LeftSpecialRay { get; set; }
    private SpecialRay RightSpecialRay { get; set; }
    
    private void Awake()
    {
        GameObject specialSideRaysContainer = Instantiate(SpecialSideRaysContainer, transform);
        var specialRays = specialSideRaysContainer.GetComponents<SpecialRay>();
        LeftSpecialRay = specialRays[0];
        RightSpecialRay = specialRays[1];
    }

    public override void UpdateShoot(bool isShooting)
    {
        LeftSpecialRay.UpdateShoot(isShooting);
        RightSpecialRay.UpdateShoot(isShooting);
    }
}