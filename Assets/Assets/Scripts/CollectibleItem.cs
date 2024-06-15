using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    public string itemName;
    public AudioClip audioClip;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Inventory inventory = collision.GetComponent<Inventory>();

            if (inventory != null)
            {
                inventory.AddItem(itemName);
                AudioSource.PlayClipAtPoint(audioClip, transform.position);
                Destroy(gameObject);
            }
        }
    }
}
