using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndingSceneManager : MonoBehaviour
{
    public GameTimer gameTimer;
    public EndScreenCondition endScreenCondition;
    public GameObject endingPanel;
    public GameObject shine;
    public GameObject ResetButton;
    public AudioSource audioSource;

    public GameObject beefSprite;
    public GameObject onionSprite;
    public GameObject springOnionSprite;
    public GameObject eggSprite;

    [SerializeField] public TextMeshProUGUI resultText;

    public bool isGameEnded = false;

    private CrossOutText beefCrossOutText;
    private CrossOutText onionCrossOutText;
    private CrossOutText springOnionCrossOutText;
    private CrossOutText eggCrossOutText;

    private void Start()
    {
        gameTimer = FindObjectOfType<GameTimer>();
        endScreenCondition = FindObjectOfType<EndScreenCondition>();

        // Find the CrossOutText components on the respective game objects
        beefCrossOutText = GameObject.Find("BeefText").GetComponent<CrossOutText>();
        onionCrossOutText = GameObject.Find("OnionText").GetComponent<CrossOutText>();
        springOnionCrossOutText = GameObject.Find("SpringOnionText").GetComponent<CrossOutText>();
        eggCrossOutText = GameObject.Find("EggText").GetComponent<CrossOutText>();

        // Subscribe to the completion status change event in EndScreenCondition
        endScreenCondition.OnCompletionStatusChanged += CheckEndScreenConditions;

        audioSource.enabled = false;
    }

    private void OnDestroy()
    {
        // Unsubscribe from the completion status change event
        endScreenCondition.OnCompletionStatusChanged -= CheckEndScreenConditions;
    }

    private void Update()
    {
        if (!isGameEnded && gameTimer.TimerCountdown <= 0f)
        {
            EndGame();
        }
    }

    private void CheckEndScreenConditions(string foodItemName, bool isComplete)
    {
        // Check if the ingredient is crossed out on the grocery list
        bool isCrossedOut = false;

        switch (foodItemName)
        {
            case "beef":
                isCrossedOut = beefCrossOutText.IsActionComplete();
                break;
            case "onion":
                isCrossedOut = onionCrossOutText.IsActionComplete();
                break;
            case "springOnion":
                isCrossedOut = springOnionCrossOutText.IsActionComplete();
                break;
            case "egg":
                isCrossedOut = eggCrossOutText.IsActionComplete();
                break;
            default:
                break;
        }

        // Enable/disable the sprite based on ingredient completion and crossed out status
        switch (foodItemName)
        {
            case "beef":
                beefSprite.SetActive(isComplete && isCrossedOut);
                break;
            case "onion":
                onionSprite.SetActive(isComplete && isCrossedOut);
                break;
            case "springOnion":
                springOnionSprite.SetActive(isComplete && isCrossedOut);
                break;
            case "egg":
                eggSprite.SetActive(isComplete && isCrossedOut);
                break;
            default:
                break;
        }

        // Check if all ingredients are complete
        if (endScreenCondition.AreAllIngredientsComplete())
        {
            resultText.text = "A YUMMY MEAL AWAITS YOU";
        }
        else
        {
            resultText.text = "YOU MADE DUE WITH WHAT YOU GRABBED, IT'S STILL TASTY THOUGH";
        }
    }

    public void EndGame()
    {
        isGameEnded = true;
        gameTimer.characterMovement.enabled = false;
        gameTimer.characterMovement.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        gameTimer.characterAnimator.enabled = false;

        // Show the ending canvas
        endingPanel.SetActive(true);
        shine.SetActive(true);
        ResetButton.SetActive(true);
        audioSource.enabled = true;

    }

}
