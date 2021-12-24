using System;
using UnityEngine;

public abstract class ShootBehaviour : ICloneable
{
    public uint TotalCycles { get; private set; }
    public Timer CycleTimer { get; private set; }

    protected uint CurrentCycles;

    public ShootBehaviour(uint totalCycles, Timer cycleTimer)
    {
        TotalCycles = totalCycles;
        CycleTimer = cycleTimer;
    }

    public abstract void UpdateShoot(Vector2 position);
    public abstract object Clone();
}