using UnityEngine;

public class NPCMovement : Movement
{
    public delegate void NPCPattern(ref Vector2 velocity, float xSpeed, float ySpeed, float timeAlive);

    public NPCPattern Pattern { get; set; }
    private float _timeAlive;

    public override void UpdateMovement()
    {
        if (!GameManager.IsRewinding)
        {
            _timeAlive += Time.deltaTime;
            Pattern?.Invoke(ref Velocity, XSpeed, YSpeed, _timeAlive);
            transform.Translate(Velocity);
        }
        else
        {
            _timeAlive -= Time.deltaTime;
        }
    }
}