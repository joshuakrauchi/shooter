using UnityEngine;

public class Player : MonoBehaviour
{
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
    }

    public void UpdatePlayerInput()
    {
        PlayerController.UpdateInput();

        if (PlayerController.IsLeftMouseDown && UIManager.Instance.IsDisplayingDialogue)
        {
            UIManager.Instance.UpdateDialogue();
        }

        GameManager.IsRewinding = PlayerController.IsRewinding && !UIManager.Instance.IsDisplayingDialogue;
    }

    public void UpdatePlayerMovementAndShoot()
    {
        PlayerMovement.UpdateMovement();
        PlayerShoot.UpdateShoot(PlayerController.IsShooting);
    }

    public void UpdatePlayerCollision()
    {
        PlayerCollision.UpdateCollision();
    }
}