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

    private void Update()
    {
        PlayerController.UpdateInput();

        if (PlayerController.IsLeftMouseDown)
        {
            UIManager.Instance.UpdateDialogue();
        }

        GameManager.IsRewinding = PlayerController.IsRewinding;
    }

    private void FixedUpdate()
    {
        PlayerCollision.UpdateCollision();
        PlayerMovement.UpdateMovement();
        PlayerShoot.UpdateShoot(PlayerController.IsShooting);
    }
}