using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public DialogueManager dManager;
    public Dialogue dialogue;
    public GameObject DialogueBox;
    private bool isDialogueTriggered = false;

    private GameObject persistentDialogueBoxTrigger; // Reference to the persistent DialogueBoxTrigger

    public void TriggerDialogue()
    {
        dManager.StartDialogue(dialogue);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") == true)
        {
            Debug.Log("Trigger test true");

            if (persistentDialogueBoxTrigger == null)
            {
                // Create a persistent DialogueBoxTrigger GameObject and make it persistent
                persistentDialogueBoxTrigger = new GameObject("PersistentDialogueBoxTrigger");
                DontDestroyOnLoad(persistentDialogueBoxTrigger);
            }

            persistentDialogueBoxTrigger.SetActive(true);
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Trigger test false");
        if (persistentDialogueBoxTrigger != null)
        {
            persistentDialogueBoxTrigger.SetActive(false);
        }

        DialogueBox.SetActive(false);
        isDialogueTriggered = false;
    }

    public void Update()
    {
        if (persistentDialogueBoxTrigger != null && persistentDialogueBoxTrigger.activeSelf && Input.GetKeyDown(KeyCode.E))
        {
            if (!isDialogueTriggered)
            {
                DialogueBox.SetActive(true);
                TriggerDialogue();
                isDialogueTriggered = true;
            }
            else
            {
                dManager.DisplayNextSentence();
            }
        }
    }
}
