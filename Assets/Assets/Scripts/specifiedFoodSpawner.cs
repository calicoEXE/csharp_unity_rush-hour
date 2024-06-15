/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class specifiedFoodSpawner : MonoBehaviour
{
    public List<Transform> spawnPoints;
    public GameObject beefRolls;
    public GameObject warningHands;
    public float initialDelay = 5f;
    public float respawnDelay = 5f;
    public int minSpawnCount = 2;
    public int maxSpawnCount = 4;

    public void StartSpawningFood()
    {
        StartCoroutine(StartSpawningFoodCoroutine());
    }

    private IEnumerator StartSpawningFoodCoroutine()
    {
        yield return new WaitForSeconds(initialDelay);

        while (true)
        {
            int spawnCount = Random.Range(minSpawnCount, maxSpawnCount);

            for (int i = 0; i < spawnCount; i++)
            {
                SpawnFood();
            }

            yield return new WaitForSeconds(respawnDelay);
        }
    }

    private void SpawnFood()
    {
        Vector3 spawnPosition = GetRandomSpawnPosition();
        GameObject newFood = Instantiate(beefRolls, spawnPosition, Quaternion.identity);
        // wait for 3s before it spawns another gameobject ontop along to destroy with the food
        OverlayWarning(newFood.transform.position, newFood);

        Destroy(newFood, 7f);
    }

    async private void OverlayWarning(Vector3 spawnPosition, GameObject food)
    {
        await Task.Delay(4000);

        if (food != null)
        {
            CircleCollider2D collider = food.GetComponentInChildren<CircleCollider2D>();
            if (collider != null)
            {
                collider.enabled = false;
            }

            GameObject newWarning = Instantiate(warningHands, spawnPosition, Quaternion.identity);
            newWarning.transform.position = spawnPosition;

            Destroy(newWarning, 3f);
        }
    }

    private Vector3 GetRandomSpawnPosition()
    {
        Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];
        return randomSpawnPoint.position;
    }
}*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class specifiedFoodSpawner : MonoBehaviour
{
    public List<Transform> spawnPoints;
    public List<GameObject> foodItems; // List of food items to spawn
    public GameObject warningHands;
    public float initialDelay = 5f;
    public float respawnDelay = 5f;
    public int minSpawnCount = 2;
    public int maxSpawnCount = 4;
    public float gameDuration = 60f;

    private float startTime;

    public void StartSpawningFood()
    {
        startTime = Time.time;
        StartCoroutine(StartSpawningFoodCoroutine());
    }

    private IEnumerator StartSpawningFoodCoroutine()
    {
        yield return new WaitForSeconds(initialDelay);

        while (true)
        {
            // Check if the game duration has elapsed
            if (Time.time - startTime >= gameDuration)
            {
                yield break; // Exit the coroutine and stop spawning food
            }

            int spawnCount = Random.Range(minSpawnCount, maxSpawnCount);

            for (int i = 0; i < spawnCount; i++)
            {
                SpawnFood();
            }

            yield return new WaitForSeconds(respawnDelay);
        }
    }

    private void SpawnFood()
    {
        Vector3 spawnPosition = GetRandomSpawnPosition();

        // Check if there is already an item spawned at the selected spawn position
        if (IsPositionOccupied(spawnPosition))
        {
            Debug.LogWarning("Spawn position is already occupied. Skipping spawn.");
            return;
        }

        GameObject newFood = Instantiate(GetRandomFoodItem(), spawnPosition, Quaternion.identity);

        // wait for 3s before it spawns another gameobject on top along to destroy with the food
        OverlayWarning(newFood.transform.position, newFood);

        Destroy(newFood, 7f);
    }

    private bool IsPositionOccupied(Vector3 position)
    {
        // Check if there is any collider overlapping with the given position
        Collider2D[] colliders = Physics2D.OverlapPointAll(position);

        // Ignore the colliders of the specifiedFoodSpawner itself
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject != gameObject)
            {
                return true;
            }
        }

        return false;
    }


    private GameObject GetRandomFoodItem()
    {
        int randomIndex = Random.Range(0, foodItems.Count);
        return foodItems[randomIndex];
    }

    async private void OverlayWarning(Vector3 spawnPosition, GameObject food)
    {
        await Task.Delay(4000);

        if (food != null)
        {
            CircleCollider2D collider = food.GetComponentInChildren<CircleCollider2D>();
            if (collider != null)
            {
                collider.enabled = false;
            }

            GameObject newWarning = Instantiate(warningHands, spawnPosition, Quaternion.identity);
            newWarning.transform.position = spawnPosition;

            Destroy(newWarning, 3f);
        }
    }

    private Vector3 GetRandomSpawnPosition()
    {
        Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];
        return randomSpawnPoint.position;
    }
}


/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class specifiedFoodSpawner : MonoBehaviour
{
    public List<Transform> spawnPoints;
    public List<GameObject> foodItems; // List of food items to spawn
    public GameObject warningHands;
    public float initialDelay = 5f;
    public float respawnDelay = 5f;
    public int minSpawnCount = 2;
    public int maxSpawnCount = 4;

    private bool isSpawning = false;

    public GameTimer gameTimer; // Reference to the GameTimer script

    public void StartSpawningFood()
    {
        StartCoroutine(StartSpawningFoodCoroutine());
    }

    public void StopSpawningFood()
    {
        StopCoroutine(StartSpawningFoodCoroutine());
        isSpawning = false;
    }

    private IEnumerator StartSpawningFoodCoroutine()
    {
        isSpawning = true;

        yield return new WaitForSeconds(initialDelay);

        while (isSpawning)
        {
            if (!gameTimer.IsTimerRunning)
            {
                yield return new WaitUntil(() => gameTimer.IsTimerRunning);
            }

            int spawnCount = Random.Range(minSpawnCount, maxSpawnCount);

            for (int i = 0; i < spawnCount; i++)
            {
                SpawnFood();
            }

            yield return new WaitForSeconds(respawnDelay);
        }
    }



    private void SpawnFood()
    {
        Vector3 spawnPosition = GetRandomSpawnPosition();
        GameObject newFood = Instantiate(GetRandomFoodItem(), spawnPosition, Quaternion.identity);
        // wait for 3s before it spawns another game object on top along to destroy with the food
        OverlayWarning(newFood.transform.position, newFood);

        Destroy(newFood, 7f);
    }

    private GameObject GetRandomFoodItem()
    {
        int randomIndex = Random.Range(0, foodItems.Count);
        return foodItems[randomIndex];
    }

    async private void OverlayWarning(Vector3 spawnPosition, GameObject food)
    {
        await Task.Delay(4000);

        if (food != null)
        {
            CircleCollider2D collider = food.GetComponentInChildren<CircleCollider2D>();
            if (collider != null)
            {
                collider.enabled = false;
            }

            GameObject newWarning = Instantiate(warningHands, spawnPosition, Quaternion.identity);
            newWarning.transform.position = spawnPosition;

            Destroy(newWarning, 3f);
        }
    }

    private Vector3 GetRandomSpawnPosition()
    {
        Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];
        return randomSpawnPoint.position;
    }
}*/
