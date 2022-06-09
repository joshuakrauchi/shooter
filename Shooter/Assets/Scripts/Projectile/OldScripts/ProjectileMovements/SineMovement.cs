using UnityEngine;

public class SineMovement : ProjectileMovement
{
    [field: SerializeField] private float Amplitude { get; set; } = 1.0f;
    [field: SerializeField] private float PhaseShift { get; set; } = Mathf.PI / 2.0f;
    [field: SerializeField] private float Period { get; set; } = 1.0f;
    
    public override void UpdateTransform(bool isRewinding)
    {
        if (isRewinding) return;
        
        Vector2 straightMovement = new Vector2(Speed, 0.0f) * Time.deltaTime;
        straightMovement.y = Amplitude * Mathf.Sin( TimeAlive * Period + PhaseShift);
        
        Rigidbody.MovePosition(Rigidbody.position + (Vector2) transform.TransformDirection(straightMovement));
    }
}