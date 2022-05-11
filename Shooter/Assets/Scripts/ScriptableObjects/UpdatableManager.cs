using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Updatable Manager")]
public class UpdatableManager : ScriptableObject
{
    private readonly List<IUpdatable> _updatables;

    private UpdatableManager()
    {
        _updatables = new List<IUpdatable>();
    }

    public void UpdateUpdatables()
    {
        foreach (IUpdatable updatable in _updatables)
        {
            updatable.UpdateUpdatable();
        }
    }

    public void AddCollectible(IUpdatable updatable)
    {
        _updatables.Add(updatable);
    }

    public void RemoveCollectible(IUpdatable updatable)
    {
        _updatables.Remove(updatable);
    }
}