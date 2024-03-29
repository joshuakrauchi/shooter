using UnityEngine;

[CreateAssetMenu(fileName = "GameState", menuName = "ScriptableObjects/GameState")]
public class GameState : ScriptableObject
{
    [field: SerializeField] public bool IsBossActive { get; set; }
    [field: SerializeField] public bool IsDisplayingDialogue { get; set; }
    [field: SerializeField] public bool IsPaused { get; set; }
    [field: SerializeField] public bool IsRewinding { get; set; }

    private void OnEnable()
    {
        IsBossActive = false;
        IsDisplayingDialogue = false;
        IsPaused = false;
        IsRewinding = false;
    }
}