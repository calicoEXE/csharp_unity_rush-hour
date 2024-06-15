using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    public GameObject foodPrefab;
    public int initialFoodCount = 5;
    public LayerMask collisionLayer;
    public Transform spawnArea;
    public float spawnRadius = 1f;

    private void Start()
    {
        for (int i = 0; i < initialFoodCount; i++)
        {
            SpawnFood();
        }
    }

    private void SpawnFood()
    {
        Vector2 spawnPosition = GetRandomSpawnPosition();
        if (CanSpawnAtPosition(spawnPosition))
        {
            GameObject newFood = Instantiate(foodPrefab, spawnPosition, Quaternion.identity);
            Destroy(newFood, 5f);
        }
    }

    private Vector2 GetRandomSpawnPosition()
    {
        float minX = spawnArea.position.x - spawnArea.localScale.x / 2f;
        float maxX = spawnArea.position.x + spawnArea.localScale.x / 2f;
        float minY = spawnArea.position.y - spawnArea.localScale.y / 2f;
        float maxY = spawnArea.position.y + spawnArea.localScale.y / 2f;

        Vector2 randomPoint;
        bool isInsideCollider;

        do
        {
            randomPoint = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
            Collider2D[] colliders = Physics2D.OverlapCircleAll(randomPoint, spawnRadius);
            isInsideCollider = false;

            foreach (Collider2D collider in colliders)
            {
                if (collisionLayer == (collisionLayer | (1 << collider.gameObject.layer)))
                {
                    isInsideCollider = true;
                    break;
                }
            }
        }
        while (isInsideCollider);

        return randomPoint;
    }

    private bool CanSpawnAtPosition(Vector2 spawnPosition)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(spawnPosition, spawnRadius, collisionLayer);
        return colliders.Length == 0;
    }
}
