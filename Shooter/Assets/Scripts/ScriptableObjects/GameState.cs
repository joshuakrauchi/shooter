using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Game State")]
public class GameState : ScriptableObject
{
    public bool IsPaused { get; set; }
    public bool IsDisplayingDialogue { get; set; }

}