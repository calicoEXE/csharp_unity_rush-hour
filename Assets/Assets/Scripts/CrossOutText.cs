using UnityEngine;
using TMPro;

public class CrossOutText : MonoBehaviour
{
    private TextMeshProUGUI textMeshPro;
    public string foodItemName;
    public int requiredAmount;

    public event System.Action OnActionComplete;

    private void Start()
    {
        // Get the TextMeshProUGUI component
        textMeshPro = GetComponent<TextMeshProUGUI>();

        // Subscribe to the inventory's item added event
        Inventory.OnItemAdded += CheckTextCompletion;
    }

    private void OnDestroy()
    {
        // Unsubscribe from the inventory's item added event
        Inventory.OnItemAdded -= CheckTextCompletion;
    }

    private void CheckTextCompletion()
    {
        int currentAmount = Inventory.Instance.GetItemCount(foodItemName);
        ToggleCompletionStatus(currentAmount);
    }

    public void ToggleCompletionStatus(int currentAmount)
    {
        bool isActionComplete = currentAmount >= requiredAmount;

        // Apply the crossed-out effect based on the completion status
        if (isActionComplete)
        {
            CrossText();
        }
        else
        {
            //RemoveCrossOut();
        }

        // Invoke the event if the action is complete
        if (isActionComplete)
        {
            OnActionComplete?.Invoke();
        }
    }

    public bool IsActionComplete()
    {
        return textMeshPro.fontStyle.HasFlag(FontStyles.Strikethrough);
    }

    public void CrossText()
    {
        // Apply the crossed-out effect by enabling the Strikethrough property
        textMeshPro.fontStyle |= FontStyles.Strikethrough;
    }

    public void RemoveCrossOut()
    {
        // Remove the crossed-out effect by disabling the Strikethrough property
        textMeshPro.fontStyle &= ~FontStyles.Strikethrough;
    }
}
