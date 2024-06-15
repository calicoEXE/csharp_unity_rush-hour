using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteDisappearingTrigger : MonoBehaviour
{
    public GameObject spriteObject;

    private bool playerInRange = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            spriteObject.SetActive(false); // Disable the sprite object to make it disappear
        }
    }
}
