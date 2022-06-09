using UnityEngine;

public class AirMovement : ProjectileMovement
{
    [field: SerializeField] private ProjectileManager ProjectileManager { get; set; }
    [field: SerializeField] private GameObject ExplosionPrefab { get; set; }
    [field: SerializeField] private float AscendTime { get; set; } = 0.75f;
    [field: SerializeField] private float DescendTime { get; set; } = 0.25f;
    [field: SerializeField] private float MaxScaleMultiplier { get; set; } = 3.0f;

    private Vector3 InitialScale { get; set; }
    private Vector3 MaxScale { get; set; }
    private Timer AscendTimer { get; set; }
    private Timer DescendTimer { get; set; }
    private bool HasExploded { get; set; }

    protected override void Awake()
    {
        base.Awake();

        InitialScale = transform.localScale;
        MaxScale = Vector3.Scale(InitialScale, new Vector3(MaxScaleMultiplier, MaxScaleMultiplier, 1.0f));
        AscendTimer = new Timer(AscendTime);
        DescendTimer = new Timer(DescendTime);
    }

    public override void UpdateTransform(bool isRewinding)
    {
        if (!HasExploded && !isRewinding)
        {
            Rigidbody.MovePosition(Rigidbody.position + (Vector2) transform.TransformDirection(new Vector2(Speed, 0.0f) * Time.deltaTime));
        }
        
        AscendTimer.UpdateTime(isRewinding);

        if (!AscendTimer.IsFinished(false))
        {
            transform.localScale = Vector3.Slerp(InitialScale, MaxScale, Mathf.SmoothStep(0.0f, 1.0f, AscendTimer.ElapsedTime / AscendTimer.TimeToFinish));

            return;
        }
        
        DescendTimer.UpdateTime(isRewinding);
            
        if (!DescendTimer.IsFinished(false))
        {
            transform.localScale = Vector3.Slerp(MaxScale, InitialScale, Mathf.SmoothStep(0.0f, 1.0f, DescendTimer.ElapsedTime / DescendTimer.TimeToFinish));
            HasExploded = false;
        }
        else if (!HasExploded)
        {
            Rigidbody.MovePosition(Utilities.OffscreenPosition);

            //ProjectileManager.CreateProjectile(ExplosionPrefab, transform.position, Quaternion.identity);
            HasExploded = true;
        }
    }

    public override void ActivatePoolable()
    {
        base.ActivatePoolable();

        AscendTimer = new Timer(AscendTime);
        DescendTimer = new Timer(DescendTime);
        HasExploded = false;
    }
}