using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UpdateableManager", menuName = "ScriptableObjects/UpdatableManager")]
public class UpdateableManager : ScriptableObject
{
    private readonly List<IUpdateable> _updateables;

    private UpdateableManager()
    {
        _updateables = new List<IUpdateable>();
    }

    public void UpdateUpdateables()
    {
        foreach (IUpdateable updateable in _updateables)
        {
            updateable.UpdateUpdateable();
        }
    }

    public void AddUpdateable(IUpdateable updateable)
    {
        _updateables.Add(updateable);
    }

    public void RemoveUpdateable(IUpdateable updateable)
    {
        _updateables.Remove(updateable);
    }
}