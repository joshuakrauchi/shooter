using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Game State")]
public class GameState : ScriptableObject
{
    public bool IsBossActive { get; set; }
    public bool IsDisplayingDialogue { get; set; }
    public bool IsPaused { get; set; }
    public bool IsRewinding { get; set; }
}