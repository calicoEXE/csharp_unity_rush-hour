/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public List<specifiedFoodSpawner> waveSpawners;
    public GameTimer gameTimer;
    private bool isDialogueEnded = false;

    private int currentWave = 0;

    private void Start()
    {
        StartNextWave();
    }

    public void StartNextWave()
    {
        currentWave++;
        Debug.Log("Wave " + currentWave + " started!");

        if (currentWave <= waveSpawners.Count)
        {
            specifiedFoodSpawner spawner = waveSpawners[currentWave - 1];

            // Check if dialogue has ended
            if (isDialogueEnded)
            {
                spawner.StartSpawningFood();
                isDialogueEnded = false;
            }
            else
            {
                // Wait for dialogue to end before starting food spawning
                StartCoroutine(WaitForDialogueEnd(spawner));
            }
        }
        else
        {
            Debug.Log("No more waves available!");
        }
    }

    public void EndCurrentWave()
    {
        Debug.Log("Wave " + currentWave + " ended!");

        if (currentWave <= waveSpawners.Count)
        {
            specifiedFoodSpawner spawner = waveSpawners[currentWave - 1];
            spawner.StopSpawningFood();
            // Add any other logic you need for ending the wave
            StartCoroutine(StartNextWaveDelay());
        }
        else
        {
            Debug.Log("No more waves available!");
        }
    }

    private IEnumerator StartNextWaveDelay()
    {
        yield return new WaitForSeconds(2f); // Delay before the next wave starts
        StartNextWave();
    }

    private IEnumerator WaitForDialogueEnd(specifiedFoodSpawner spawner)
    {
        while (!isDialogueEnded)
        {
            yield return null;
        }

        spawner.StartSpawningFood();
        isDialogueEnded = false;
    }

    // Call this method from the DialogueManager when the dialogue ends
    public void SetDialogueEnded()
    {
        isDialogueEnded = true;
    }
}*/
