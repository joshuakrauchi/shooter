using System.Collections.Generic;
using UnityEngine;

public class CollectibleManager
{
    private static CollectibleManager _instance;
    public static CollectibleManager Instance => _instance ??= new CollectibleManager();

    private List<Collectible> _collectibles;

    private CollectibleManager()
    {
        _collectibles = new List<Collectible>();
    }

    public void UpdateCollectibles()
    {
        foreach (var collectible in _collectibles)
        {
            collectible.UpdateMovement();
        }
    }

    public void AddCollectible(Collectible collectible)
    {
        _collectibles.Add(collectible);
    }

    public void RemoveCollectible(Collectible collectible)
    {
        _collectibles.Remove(collectible);
    }
}