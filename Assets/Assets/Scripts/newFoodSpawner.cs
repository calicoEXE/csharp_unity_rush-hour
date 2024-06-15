using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newFoodSpawner : MonoBehaviour
{
    public GameObject foodPrefab;
    public int initialFoodCount = 5;
    public Transform spawnArea;
    public float minSpawnDelay = 1f;
    public float maxSpawnDelay = 4f;
    public List<Collider2D> specificColliders;

    private void Start()
    {
        for (int i = 0; i < initialFoodCount; i++)
        {
            SpawnFood();
        }

        StartCoroutine(SpawnFoodWithDelay());
    }

    private IEnumerator SpawnFoodWithDelay()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));

            int spawnCount = Random.Range(2, 3);

            for (int i = 0; i < spawnCount; i++)
            {
                Vector2 spawnPosition = GetRandomSpawnPosition();
                GameObject newFood = Instantiate(foodPrefab, spawnPosition, Quaternion.identity);

                if (IsCollidingWithSpecificColliders(newFood))
                {
                    Debug.Log("Food dead");
                    Destroy(newFood);
                    SpawnFood(); // Spawn additional food
                }
                else
                {
                    Debug.Log("Food dead, much wow");
                    Destroy(newFood, 5f);
                }
            }
        }
    }

    private void SpawnFood()
    {
        Vector2 spawnPosition = GetRandomSpawnPosition();
        GameObject newFood = Instantiate(foodPrefab, spawnPosition, Quaternion.identity);

        if (IsCollidingWithSpecificColliders(newFood))
        {
            Destroy(newFood);
            SpawnFood(); // Spawn additional food
        }
        else
        {
            Destroy(newFood, 5f);
        }
    }

    private bool IsCollidingWithSpecificColliders(GameObject beefRolls)
    {
        CircleCollider2D[] colliders = beefRolls.GetComponents<CircleCollider2D>();

        foreach (CircleCollider2D collider in colliders)
        {
            foreach (CircleCollider2D specificCollider in specificColliders)
            {
                if (collider.IsTouching(specificCollider))
                {
                    return true;
                }
            }
        }

        return false;
    }

    private Vector2 GetRandomSpawnPosition()
    {
        float minX = -13f + 3f;
        float maxX = 11f + 6f;
        float minY = 1f - 5f;
        float maxY = -11f - 5f;

        Vector2 randomPoint = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        return randomPoint;
    }
}
