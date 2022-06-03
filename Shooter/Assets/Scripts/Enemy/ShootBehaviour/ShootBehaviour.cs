using UnityEngine;

public abstract class ShootBehaviour : MonoBehaviour
{
    public class ShootTimeData
    {
        public Timer CycleTimer { get; }
        public uint CurrentCycles { get; }

        public ShootTimeData(Timer cycleTimer, uint currentCycles)
        {
            CycleTimer = cycleTimer;
            CurrentCycles = currentCycles;
        }
    }
    
    [field: SerializeField] protected GameObject ProjectilePrefab { get; private set; }

    // A "Cycle" is a full iteration of a ShootBehaviour. A single cycle can hold one to many "shots".
    // "0" TotalCycles means repeat infinitely.
    [field: SerializeField] private uint TotalCycles { get; set; }
    // There can be a delay between cycles, so you can do rapid fire shots split up by short pauses.
    [field: SerializeField] private float TimeBetweenCycles { get; set; } = 1.0f;

    protected uint CurrentCycles { get; private set; }
    protected Timer CycleTimer { get; private set; }

    protected virtual void Awake()
    {
        CycleTimer = new Timer(TimeBetweenCycles);
    }

    public void UpdateShoot(bool isRewinding)
    {
        CycleTimer.UpdateTime(isRewinding);

        if (!CycleTimer.IsFinished(false) || TotalCycles != 0 && CurrentCycles == TotalCycles) return;

        if (!UpdateCycle(isRewinding)) return;

        ++CurrentCycles;
        CycleTimer = new Timer(CycleTimer.TimeToFinish);
    }

    /**
     * Called every FixedUpdate.
     * Classes deriving from ShootBehaviour have to make sure to return whatever params are necessary for rewinding.
     * If ShootTimeData doesn't hold all the necessary parameters, derive a new class from ShootTimeData.
     */
    public abstract ShootTimeData GetRecordData();

    /**
     * During rewind, sets all the parameters that were previously given by GetRecordData().
     */
    public virtual void SetRewindData(ShootTimeData shootTimeData)
    {
        CycleTimer = shootTimeData.CycleTimer;
        CurrentCycles = shootTimeData.CurrentCycles;
    }

    /**
     * Returns true if the cycle is completely finished.
     */
    protected abstract bool UpdateCycle(bool isRewinding);
}