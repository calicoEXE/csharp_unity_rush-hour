using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    public float timerSeconds = 10f;
    public CharacterMovement characterMovement;
    public DialogueManager dialogueManager;
    public Animator characterAnimator; // Reference to the character's animator component

    private bool isTimerRunning = false;
    private bool isDialogueEnded = false;

    public float timerCountdown;
    private SpriteRenderer spriteRenderer;

    public List<Sprite> countdownSprites; // List of sprites representing each second countdown

    public float TimerCountdown
    {
        get { return timerCountdown; }
        private set { timerCountdown = value; }
    }

    private void Start()
    {
        timerCountdown = timerSeconds;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (!isTimerRunning && isDialogueEnded)
        {
            isTimerRunning = true;
            StartCoroutine(CountdownTimer());
        }
    }

    public void SetDialogueEnded()
    {
        isDialogueEnded = true;
    }

    private IEnumerator CountdownTimer()
    {
        while (timerCountdown > 0f)
        {
            yield return new WaitForSeconds(1f);
            timerCountdown -= 1f;
            UpdateTimerSprite();
        }

        isTimerRunning = false;
        characterMovement.enabled = false;
        characterMovement.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        characterAnimator.enabled = false; // Freeze the character's animation
        Debug.Log("Time's up! Player movement and animation are frozen.");
    }

    private void UpdateTimerSprite()
    {
        int spriteIndex = Mathf.RoundToInt(timerCountdown);
        spriteRenderer.sprite = countdownSprites[spriteIndex];
    }

    public void ResetTimer()
    {
        timerCountdown = timerSeconds;
        isTimerRunning = false;
    }
}





/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    public float timerSeconds = 10f;
    public float nextWaveDelay = 2f; // Delay before the next wave starts
    public CharacterMovement characterMovement;
    public DialogueManager dialogueManager;

    [SerializeField]
    private float timerCountdown;

    private bool isTimerRunning = false;
    private bool isDialogueCompleted = false;

    public float TimerCountdown
    {
        get { return timerCountdown; }
        private set { timerCountdown = value; }
    }

    public List<Sprite> countdownSprites; // List of sprites representing each second countdown

    private SpriteRenderer spriteRenderer;

    public bool IsTimerRunning // Property to check if the timer is running
    {
        get { return isTimerRunning; }
    }

    private void Start()
    {
        TimerCountdown = timerSeconds;
        spriteRenderer = GetComponent<SpriteRenderer>();
        dialogueManager.DialogueEnded += SetDialogueCompleted;
    }

    private void OnDestroy()
    {
        dialogueManager.DialogueEnded -= SetDialogueCompleted;
    }

    private void Update()
    {
        if (!isDialogueCompleted || isTimerRunning)
            return;

        if (!isTimerRunning)
        {
            isTimerRunning = true;
            StartCoroutine(CountdownTimer());
        }
    }

    public void SetDialogueCompleted()
    {
        isDialogueCompleted = true;
    }

    private IEnumerator CountdownTimer()
    {
        yield return new WaitUntil(() => isDialogueCompleted);

        while (TimerCountdown > 0f)
        {
            yield return new WaitForSeconds(1f);
            TimerCountdown -= 1f;
            UpdateTimerSprite();
        }

        characterMovement.enabled = false;
        characterMovement.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        Debug.Log("Time's up! Player movement is frozen.");

        // Wait for the next wave to start
        yield return new WaitForSeconds(nextWaveDelay);

        characterMovement.enabled = true;
        characterMovement.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        characterMovement.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        Debug.Log("Next wave started! Player movement is enabled.");
    }

    private void UpdateTimerSprite()
    {
        int spriteIndex = Mathf.RoundToInt(TimerCountdown);
        spriteRenderer.sprite = countdownSprites[spriteIndex];
    }
}*/
