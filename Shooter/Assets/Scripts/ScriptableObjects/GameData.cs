using UnityEngine;

[CreateAssetMenu]
public class GameData: ScriptableObject
{
    [field: SerializeField] public uint Shots { get; set; }
    [field: SerializeField] public uint Currency { get; set; }
}