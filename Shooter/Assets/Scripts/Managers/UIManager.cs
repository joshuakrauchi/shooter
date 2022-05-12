using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "ScriptableObjects/UIManager")]
public class UIManager : ScriptableObject, IUpdateable
{
    [field: SerializeField] public GameObject RewindBar { get; private set; }

    [SerializeField] private GameData gameData;
    [SerializeField] private GameState gameState;
    [SerializeField] private GameObject dialogueBox;

    public bool IsDisplayingDialogue { get; private set; }

    private Queue<Tuple<string, string>> _textQueue;
    private Canvas _dialogueCanvas;
    private Text _header;
    private Text _text;
    private ValueSlider _rewindSlider;
    private bool _initialized;

    public void Initialize()
    {
        if (_initialized) return;
        _initialized = true;

        _rewindSlider = Instantiate(RewindBar).GetComponent<ValueSlider>();
        

        _rewindSlider.SetMaxValue(gameData.RewindCharge);
        _rewindSlider.SetValue(gameData.RewindCharge);

        _textQueue = new Queue<Tuple<string, string>>();

        GameObject instantiatedDialogueBox = Instantiate(dialogueBox);
        _header = instantiatedDialogueBox.transform.GetChild(0).GetComponent<Text>();
        _text = instantiatedDialogueBox.transform.GetChild(1).GetComponent<Text>();
    }

    public void UpdateUpdateable()
    {
        
    }

    public void StartDialogue(IEnumerable<Tuple<string, string>> dialogue)
    {
        gameState.IsPaused = true;

        foreach (var d in dialogue)
        {
            _textQueue.Enqueue(d);
        }

        _dialogueCanvas.enabled = true;
        IsDisplayingDialogue = true;

        UpdateDialogue();
    }

    public void UpdateDialogue()
    {
        if (_textQueue.Count <= 0)
        {
            EndDialogue();
            return;
        }

        var (headerText, dialogueText) = _textQueue.Dequeue();

        _header.text = headerText;
        _text.text = dialogueText;
    }

    public void EndDialogue()
    {
        _dialogueCanvas.enabled = false;
        _header.text = "";
        _text.text = "";

        IsDisplayingDialogue = false;
        gameState.IsPaused = false;
    }
}