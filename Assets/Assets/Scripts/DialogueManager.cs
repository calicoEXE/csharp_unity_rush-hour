using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public GameObject DialogueBox;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public GameTimer gameTimer;
    public specifiedFoodSpawner foodSpawner; // Reference to the specifiedFoodSpawner script

    private Queue<string> sentences;

    public delegate void DialogueEvent();
    public event DialogueEvent DialogueEnded;

    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;
    }

    void EndDialogue()
    {
        Debug.Log("End of conversation.");
        DialogueBox.SetActive(false);
        DialogueEnded?.Invoke();

        // Start the specifiedFoodSpawner script
        foodSpawner.StartSpawningFood();
        gameTimer.SetDialogueEnded();
    }
}
