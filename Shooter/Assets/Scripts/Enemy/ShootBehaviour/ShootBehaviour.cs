using System;
using UnityEngine;

[Serializable]
public abstract class ShootBehaviour
{
    [field: SerializeField] public uint TotalCycles { get; private set; }
    [field: SerializeField] public Timer CycleTimer { get; private set; }

    protected uint CurrentCycles;

    public ShootBehaviour(uint totalCycles, Timer cycleTimer)
    {
        TotalCycles = totalCycles;
        CycleTimer = cycleTimer;
    }

    public abstract void UpdateShoot(Vector2 position);
    public abstract ShootBehaviour Clone();
}