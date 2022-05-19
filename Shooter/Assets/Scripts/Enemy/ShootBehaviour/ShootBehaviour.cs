using UnityEngine;

public abstract class ShootBehaviour: MonoBehaviour
{
    [field: SerializeField] protected GameObject ProjectilePrefab { get; private set; }

    // A "Cycle" is a full iteration of a ShootBehaviour. A single cycle can hold one to many "shots".
    // There can be a delay between cycles, so you can do rapid fire shots split up by short pauses.
    // "0" TotalCycles means repeat infinitely.
    [field: SerializeField] private uint TotalCycles { get; set; }
    [field: SerializeField] private Timer CycleTimer { get; set; } = new Timer(1.0f);

    private uint CurrentCycles { get; set; }

    public void UpdateShoot(bool isRewinding)
    {
        CycleTimer.UpdateTime(isRewinding);

        if (!CycleTimer.IsFinished(false) || TotalCycles != 0 && CurrentCycles == TotalCycles) return;

        if (!UpdateCycle(isRewinding)) return;

        ++CurrentCycles;
        CycleTimer.Reset();
    }

    /**
     * Returns true if the cycle is completely finished.
     */
    protected abstract bool UpdateCycle(bool isRewinding);
}