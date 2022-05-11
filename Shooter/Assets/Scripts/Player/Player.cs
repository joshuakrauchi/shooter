using UnityEngine;

public class Player : MonoBehaviour, IUpdatable
{
    [SerializeField] private GameData gameData;
    [SerializeField] private GameState gameState;

    private PlayerCollision _playerCollision;
    private PlayerController _playerController;
    private PlayerMovement _playerMovement;
    private PlayerShoot _playerShoot;

    private void Awake()
    {
        _playerCollision = GetComponent<PlayerCollision>();
        _playerController = GetComponent<PlayerController>();
        _playerMovement = GetComponent<PlayerMovement>();
        _playerShoot = GetComponent<PlayerShoot>();

        _playerShoot.NumberOfShots = gameData.Shots;

        gameData.Player = this;
    }

    public void UpdatePlayerInput()
    {
        _playerController.UpdateInput();

        if (_playerController.IsConfirmDown)
        {
            GameManager.OnPlayerConfirmDown();
        }

        gameState.IsRewinding = _playerController.IsRewinding;
    }

    public void UpdateUpdatable()
    {
        _playerCollision.UpdateCollision();
        _playerMovement.UpdateMovement();
        _playerShoot.UpdateShoot(_playerController.IsShooting, gameState.IsRewinding);
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