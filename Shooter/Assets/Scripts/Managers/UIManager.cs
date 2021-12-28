using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager
{
    private static UIManager _instance;

    public static UIManager Instance => _instance ??= new UIManager();

    public Queue<Tuple<string, string>> TextQueue { get; private set; }
    public Canvas DialogueBox { get; set; }
    public Text Header { get; set; }
    public Text Text { get; set; }
    public bool IsDisplayingDialogue { get; private set; }

    private UIManager()
    {
        TextQueue = new Queue<Tuple<string, string>>();
    }

    public void StartDialogue(IEnumerable<Tuple<string, string>> textStrings)
    {
        GameManager.IsPaused = true;

        foreach (var t in textStrings)
        {
            TextQueue.Enqueue(t);
        }

        DialogueBox.enabled = true;
        IsDisplayingDialogue = true;

        UpdateDialogue();
    }

    public void UpdateDialogue()
    {
        if (TextQueue.Count <= 0)
        {
            EndDialogue();
            return;
        }

        var dialogue = TextQueue.Dequeue();

        Header.text = dialogue.Item1;
        Text.text = dialogue.Item2;
    }

    public void EndDialogue()
    {
        DialogueBox.enabled = false;
        Header.text = "";
        Text.text = "";

        IsDisplayingDialogue = false;
        GameManager.IsPaused = false;
    }
}