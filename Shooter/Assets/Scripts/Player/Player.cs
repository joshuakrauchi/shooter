using UnityEngine;

public class Player : MonoBehaviour, IUpdateable
{
    [field: SerializeField] private GameData GameData { get; set; }
    [field: SerializeField] private GameState GameState { get; set; }
    [field: SerializeField] private UIManager UIManager { get; set; }

    private PlayerCollision PlayerCollision { get; set; }
    private PlayerController PlayerController { get; set; }
    private PlayerMovement PlayerMovement { get; set; }
    private PlayerShoot PlayerShoot { get; set; }
    private PlayerSpecialShoot PlayerSpecialShoot { get; set; }

    private void Awake()
    {
        PlayerCollision = GetComponent<PlayerCollision>();
        PlayerController = GetComponent<PlayerController>();
        PlayerMovement = GetComponent<PlayerMovement>();
        PlayerShoot = GetComponent<PlayerShoot>();
        PlayerSpecialShoot = GetComponent<PlayerSpecialShoot>();

        PlayerShoot.NumberOfShots = GameData.Shots;

        GameData.Player = this;
    }

    public void UpdatePlayerInput()
    {
        PlayerController.UpdateInput();

        if (PlayerController.IsConfirmDown)
        {
            if (!GameState.IsDisplayingDialogue) return;

            UIManager.UpdateDialogue();
        }

        GameState.IsRewinding = PlayerController.IsRewinding;
    }

    public void UpdateUpdateable()
    {
        PlayerCollision.UpdateCollision();
        PlayerMovement.UpdateMovement();
        PlayerShoot.UpdateShoot(PlayerController.IsShooting, GameState.IsRewinding);
        PlayerSpecialShoot.UpdateSpecialShoot(PlayerController.IsSpecialHeld);
    }

    public void OnCollectibleHit(Collectible collectible)
    {
        GameData.Currency += collectible.Value;
        collectible.DestroySelf();
    }

    public void OnHit()
    {
        //GameState.IsPaused = true;
    }
}