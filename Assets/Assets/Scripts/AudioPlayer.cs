using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public AudioSource audioSource;
    public DialogueManager dialogueManager;
    public EndingSceneManager endingSceneManager; // Reference to the EndingSceneManager script

    void Start()
    {
        dialogueManager.DialogueEnded += EnableAudio;
        audioSource.enabled = false;
    }

    void EnableAudio()
    {
        if (!endingSceneManager.isGameEnded)
        {
            audioSource.enabled = true;
        }
    }

    void Update()
    {
        if (endingSceneManager.isGameEnded)
        {
            audioSource.Stop();
        }
    }
}
