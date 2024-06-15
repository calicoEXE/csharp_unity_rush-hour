using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private Dictionary<string, int> items = new Dictionary<string, int>();

    // Create a singleton instance of the Inventory
    public static Inventory Instance { get; private set; }

    // Event for item added
    public static event Action OnItemAdded;

    private void Awake()
    {
        // Ensure only one instance of the Inventory exists
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddItem(string itemName)
    {
        if (items.ContainsKey(itemName))
        {
            items[itemName]++;
        }
        else
        {
            items[itemName] = 1;
        }

        Debug.Log("Added item: " + itemName);

        // Trigger the item added event
        OnItemAdded?.Invoke();
    }

    public bool HasItem(string itemName)
    {
        return items.ContainsKey(itemName) && items[itemName] > 0;
    }

    public int GetItemCount(string itemName)
    {
        return items.ContainsKey(itemName) ? items[itemName] : 0;
    }
}
