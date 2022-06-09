using System;
using System.Linq;
using UnityEngine;

/**
 * SequentialMovement combines different ProjectileMovements by running them sequentially,
 * one after the other, at times given by the StartTimes array.
 * ProjectileMovements are attached via a child GameObject and run in the order of attachment to that object.
 */
public class SequentialMovement : ProjectileMovement
{
    // Every entry in this array is a time to start the next ProjectileMovement.
    // So if the first entry is 1.0f, the second ProjectileMovement will start running after 1 second.
    // This means that there must be n - 1 StartTimes, where n is the number of ProjectileMovements.
    [field: SerializeField] private float[] StartTimes { get; set; }

    // An array of ProjectileMovements to run sequentially.
    private ProjectileMovement[] ProjectileMovements { get; set; }
    private int CurrentMovementIndex { get; set; }

    protected override void Awake()
    {
        base.Awake();

        ProjectileMovements = GetComponents<ProjectileMovement>().Skip(1).ToArray();

        if (ProjectileMovements.Length <= 0)
        {
            throw new Exception("A projectile with the SequentialMovement component must have other ProjectileMovements to run.");
        }

        if (StartTimes.Length != ProjectileMovements.Length - 1)
        {
            throw new Exception("Number of StartTimes must be 1 less than the number of ProjectileMovements.");
        }
    }

    public override void UpdateTransform(bool isRewinding)
    {
        UpdateCurrentMovementIndex();
        
        ProjectileMovements[CurrentMovementIndex].UpdateMovement(isRewinding);
    }

    public override void ActivatePoolable()
    {
        base.ActivatePoolable();

        foreach(ProjectileMovement movement in ProjectileMovements)
        {
            movement.ActivatePoolable();
        }
    }

    private void UpdateCurrentMovementIndex()
    {
        while (CurrentMovementIndex > 0 && TimeAlive < StartTimes[CurrentMovementIndex - 1])
        {
            --CurrentMovementIndex;
        }

        while (CurrentMovementIndex < StartTimes.Length && TimeAlive >= StartTimes[CurrentMovementIndex])
        {
            ++CurrentMovementIndex;
        }
    }
}