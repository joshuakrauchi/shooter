using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private GameObject background;
    [SerializeField] private GameObject header;
    [SerializeField] private GameObject text;
    [SerializeField] private GameObject rewindBar;

    public bool IsDisplayingDialogue { get; private set; }

    private Queue<Tuple<string, string>> _textQueue;
    private Canvas _dialogueCanvas;
    private Text _header;
    private Text _text;

    private void Awake() {
        _textQueue = new Queue<Tuple<string, string>>();

        _dialogueCanvas = Instantiate(dialogueBox, transform).GetComponent<Canvas>();
        var backgroundObject = Instantiate(background, _dialogueCanvas.transform);
        _header = Instantiate(header, backgroundObject.transform).GetComponent<Text>();
        _text = Instantiate(text, backgroundObject.transform).GetComponent<Text>();

        Instantiate(rewindBar, transform);
    }

    public void StartDialogue(IEnumerable<Tuple<string, string>> textStrings)
    {
        GameManager.IsPaused = true;

        foreach (var t in textStrings)
        {
            _textQueue.Enqueue(t);
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

        var dialogue = _textQueue.Dequeue();

        _header.text = dialogue.Item1;
        _text.text = dialogue.Item2;
    }

    public void EndDialogue()
    {
        _dialogueCanvas.enabled = false;
        _header.text = "";
        _text.text = "";

        IsDisplayingDialogue = false;
        GameManager.IsPaused = false;
    }
}