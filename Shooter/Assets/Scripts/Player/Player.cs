using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float rewindCharge = 10f;

    public PlayerCollision PlayerCollision { get; private set; }
    public PlayerController PlayerController { get; private set; }
    public PlayerMovement PlayerMovement { get; private set; }
    public PlayerShoot PlayerShoot { get; private set; }
    public float Currency { get; set; }

    public float RewindCharge
    {
        get => rewindCharge;
        set
        {
            rewindCharge = value;

            if (rewindCharge < 0f)
            {
                rewindCharge = 0f;
            }
            else if (rewindCharge > _maxRewindCharge)
            {
                rewindCharge = _maxRewindCharge;
            }
        }
    }

    public float MaxRewindCharge
    {
        get => _maxRewindCharge;
        private set => _maxRewindCharge = value;
    }

    private float _maxRewindCharge;

    private void Awake()
    {
        PlayerCollision = GetComponent<PlayerCollision>();
        PlayerController = GetComponent<PlayerController>();
        PlayerMovement = GetComponent<PlayerMovement>();
        PlayerShoot = GetComponent<PlayerShoot>();

        _maxRewindCharge = rewindCharge;
    }

    public void UpdatePlayerInput()
    {
        PlayerController.UpdateInput();

        if (PlayerController.IsLeftMouseDown && UIManager.Instance.IsDisplayingDialogue)
        {
            UIManager.Instance.UpdateDialogue();
        }

        if (PlayerController.IsRewinding && !UIManager.Instance.IsDisplayingDialogue)
        {
            RewindCharge -= Time.deltaTime;
        }

        GameManager.IsRewinding = PlayerController.IsRewinding && RewindCharge > 0f && !UIManager.Instance.IsDisplayingDialogue;
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