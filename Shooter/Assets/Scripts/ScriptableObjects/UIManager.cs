using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "UIManager", menuName = "ScriptableObjects/UIManager")]
public class UIManager : ScriptableObject, IUpdateable
{
    [field: SerializeField] private GameData GameData { get; set; }
    [field: SerializeField] private GameState GameState { get; set; }
    [field: SerializeField] private GameObject MainUICanvas { get; set; }
    [field: SerializeField] private GameObject RewindBar { get; set; }
    [field: SerializeField] private GameObject DialogueBox { get; set; }

    private Queue<Tuple<string, string>> TextQueue { get; set; }
    private ValueSlider RewindSlider { get; set; }
    private Canvas DialogueBoxCanvas { get; set; }
    private TextMeshProUGUI DialogueBoxNameText { get; set; }
    private TextMeshProUGUI DialogueBoxDialogueText { get; set; }

    public void Initialize()
    {
        Transform parentTransform = Instantiate(MainUICanvas).transform;

        RewindSlider = Instantiate(RewindBar, parentTransform).GetComponent<ValueSlider>();
        RewindSlider.SetMaxValue(GameData.MaxRewindCharge);

        TextQueue = new Queue<Tuple<string, string>>();

        DialogueBoxCanvas = Instantiate(DialogueBox, parentTransform).GetComponent<Canvas>();
        var texts = DialogueBoxCanvas.GetComponentsInChildren<TextMeshProUGUI>();
        DialogueBoxNameText = texts[0];
        DialogueBoxDialogueText = texts[1];

        EndDialogue();
    }

    public void StartDialogue(IEnumerable<Tuple<string, string>> dialogue)
    {
        GameState.IsPaused = true;

        foreach (var entry in dialogue)
        {
            TextQueue.Enqueue(entry);
        }

        DialogueBoxCanvas.enabled = true;
        GameState.IsDisplayingDialogue = true;

        UpdateDialogue();
    }

    public void UpdateDialogue()
    {
        if (TextQueue.Count <= 0)
        {
            EndDialogue();
            return;
        }

        var (headerText, dialogueText) = TextQueue.Dequeue();

        DialogueBoxNameText.text = headerText;
        DialogueBoxDialogueText.text = dialogueText;
    }

    private void EndDialogue()
    {
        DialogueBoxCanvas.enabled = false;
        DialogueBoxNameText.text = "";
        DialogueBoxDialogueText.text = "";

        GameState.IsDisplayingDialogue = false;
        GameState.IsPaused = false;
    }

    public void UpdateUpdateable()
    {
        RewindSlider.SetCurrentValue(GameData.RewindCharge);
    }
}