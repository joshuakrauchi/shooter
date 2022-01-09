using UnityEngine;

public class Player : MonoBehaviour, IUpdatable
{
    [SerializeField] private GameData gameData;

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
    }

    public void UpdatePlayerInput()
    {
        PlayerController.UpdateInput();

        if (PlayerController.IsLeftMouseDown && GameManager.UIManager.IsDisplayingDialogue)
        {
            GameManager.UIManager.UpdateDialogue();
        }

        GameManager.IsRewinding = PlayerController.IsRewinding;
    }

    public void UpdateUpdatable()
    {
        PlayerCollision.UpdateCollision();
        PlayerMovement.UpdateMovement();
        PlayerShoot.UpdateShoot(PlayerController.IsShooting);
    }

    public void OnCollectibleHit(Collectible collectible)
    {
        gameData.Currency += collectible.Value;
        collectible.DestroySelf();
    }

    public void OnHit()
    {
        GameManager.OnPlayerHit();
    }
}