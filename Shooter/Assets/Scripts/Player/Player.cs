using UnityEngine;

public class Player : MonoBehaviour, IUpdatable
{
    [SerializeField] private GameData gameData;
    [SerializeField] private GameState gameState;

    public PlayerCollision PlayerCollision { get; private set; }
    public PlayerController PlayerController { get; private set; }
    public PlayerMovement PlayerMovement { get; private set; }
    public PlayerShoot PlayerShoot { get; private set; }

    private void Awake()
    {
        PlayerCollision = GetComponent<PlayerCollision>();
        PlayerController = GetComponent<PlayerController>();
        PlayerMovement = GetComponent<PlayerMovement>();
        PlayerShoot = GetComponent<PlayerShoot>();

        PlayerShoot.NumberOfShots = gameData.Shots;
        GameManager.Player = this;
    }

    public void UpdatePlayerInput()
    {
        PlayerController.UpdateInput();

        if (PlayerController.IsConfirmDown)
        {
            GameManager.OnPlayerConfirmDown();
        }

        gameState.IsRewinding = PlayerController.IsRewinding;
    }

    public void UpdateUpdatable()
    {
        PlayerCollision.UpdateCollision();
        PlayerMovement.UpdateMovement();
        PlayerShoot.UpdateShoot(PlayerController.IsShooting, gameState.IsRewinding);
    }

    public void OnCollectibleHit(Collectible collectible)
    {
        gameData.Currency += collectible.Value;
        collectible.DestroySelf();
    }

    public void OnHit()
    {
        gameState.IsPaused = true;
    }
}