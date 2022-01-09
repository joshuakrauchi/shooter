using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [field: SerializeField] public GameObject RewindBar { get; private set; }

    [SerializeField] private GameState gameState;
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private GameObject background;
    [SerializeField] private GameObject header;
    [SerializeField] private GameObject text;

    public bool IsDisplayingDialogue { get; private set; }

    private Queue<Tuple<string, string>> _textQueue;
    private Canvas _dialogueCanvas;
    private Text _header;
    private Text _text;

    private void Awake() {
        RewindBar = Instantiate(RewindBar, transform);

        _textQueue = new Queue<Tuple<string, string>>();

        _dialogueCanvas = Instantiate(dialogueBox, transform).GetComponent<Canvas>();
        var backgroundObject = Instantiate(background, _dialogueCanvas.transform);
        _header = Instantiate(header, backgroundObject.transform).GetComponent<Text>();
        _text = Instantiate(text, backgroundObject.transform).GetComponent<Text>();
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