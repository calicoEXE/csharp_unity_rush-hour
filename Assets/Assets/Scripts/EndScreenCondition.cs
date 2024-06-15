using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndScreenCondition : MonoBehaviour
{
    public Image beefSprite;
    public Image onionSprite;
    public Image springOnionSprite;
    public Image eggSprite;
    public TextMeshProUGUI resultText;

    public delegate void CompletionStatusChangedHandler(string foodItemName, bool isComplete);
    public event CompletionStatusChangedHandler OnCompletionStatusChanged;

    private CrossOutText beefCrossOutText;
    private CrossOutText onionCrossOutText;
    private CrossOutText springOnionCrossOutText;
    private CrossOutText eggCrossOutText;

    private void Start()
    {
        // Find the CrossOutText components on the respective game objects
        beefCrossOutText = GameObject.Find("BeefText").GetComponent<CrossOutText>();
        onionCrossOutText = GameObject.Find("OnionText").GetComponent<CrossOutText>();
        springOnionCrossOutText = GameObject.Find("SpringOnionText").GetComponent<CrossOutText>();
        eggCrossOutText = GameObject.Find("EggText").GetComponent<CrossOutText>();

        // Subscribe to the completion status change events
        beefCrossOutText.OnActionComplete += () => CompletionStatusChanged("beef", beefCrossOutText.IsActionComplete());
        onionCrossOutText.OnActionComplete += () => CompletionStatusChanged("onion", onionCrossOutText.IsActionComplete());
        springOnionCrossOutText.OnActionComplete += () => CompletionStatusChanged("springOnion", springOnionCrossOutText.IsActionComplete());
        eggCrossOutText.OnActionComplete += () => CompletionStatusChanged("egg", eggCrossOutText.IsActionComplete());
    }

    private void OnDestroy()
    {
        // Unsubscribe from the completion status change events
        beefCrossOutText.OnActionComplete -= () => CompletionStatusChanged("beef", beefCrossOutText.IsActionComplete());
        onionCrossOutText.OnActionComplete -= () => CompletionStatusChanged("onion", onionCrossOutText.IsActionComplete());
        springOnionCrossOutText.OnActionComplete -= () => CompletionStatusChanged("springOnion", springOnionCrossOutText.IsActionComplete());
        eggCrossOutText.OnActionComplete -= () => CompletionStatusChanged("egg", eggCrossOutText.IsActionComplete());
    }

    private void CompletionStatusChanged(string foodItemName, bool isComplete)
    {
        // Check the completion status of each ingredient and show/hide the respective sprite
        switch (foodItemName)
        {
            case "beef":
                beefSprite.gameObject.SetActive(isComplete);
                break;
            case "onion":
                onionSprite.gameObject.SetActive(isComplete);
                break;
            case "springOnion":
                springOnionSprite.gameObject.SetActive(isComplete);
                break;
            case "egg":
                eggSprite.gameObject.SetActive(isComplete);
                break;
            default:
                break;
        }

        // Invoke the event to notify the subscribers about the completion status change
        OnCompletionStatusChanged?.Invoke(foodItemName, isComplete);

        // Check if all ingredients are complete
        if (AreAllIngredientsComplete())
        {
            resultText.text = "A YUMMY MEAL AWAITS YOU";
        }
        else
        {
            resultText.text = "YOU MADE DUE WITH WHAT YOU GRABBED, IT'S STILL TASTY THOUGH";
        }
    }

    public bool AreAllIngredientsComplete()
    {
        return beefCrossOutText.IsActionComplete() && onionCrossOutText.IsActionComplete() &&
            springOnionCrossOutText.IsActionComplete() && eggCrossOutText.IsActionComplete();
    }
}
