using System;
using UnityEngine;

public abstract class ShootBehaviour : ICloneable
{
    public uint TotalCycles { get; private set; }
    public float TimeBetweenCycles { get; private set; }

    protected uint CurrentCycles;
    protected float CurrentTime;

    public ShootBehaviour(uint totalCycles, float timeBetweenCycles)
    {
        TotalCycles = totalCycles;
        TimeBetweenCycles = timeBetweenCycles;
    }

    public abstract void UpdateShoot(Vector2 position);
    public abstract object Clone();
}