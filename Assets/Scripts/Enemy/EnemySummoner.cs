using UnityEngine;

public class EnemySummoner : MonoBehaviour
{
    [Header("Spawning Settings")]
    public GameObject enemyPrefab;        // The enemy prefab to spawn
    public float spawnCooldown = 5f;      // Time (in seconds) between spawns
    public int maxSpawnedEnemies = 6;     // Maximum number of enemies spawned by this enemy
    public Vector2[] spawnOffsets;        // Positions relative to this enemy where new enemies spawn

    [Header("Detection Settings")]
    public float detectionRange = 10f;    // Distance at which the enemy detects the player

    private Transform player;             // Reference to the player
    private float lastSpawnTime;          // Tracks the last time enemies were spawned
    private int currentSpawnedEnemies;    // Tracks the current number of spawned enemies

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        // Default spawn positions if none are specified
        if (spawnOffsets == null || spawnOffsets.Length == 0)
        {
            spawnOffsets = new Vector2[] { new Vector2(1f, 0), new Vector2(-1f, 0) };
        }
    }

    private void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // Check if the player is within detection range and cooldown has passed
        if (distanceToPlayer <= detectionRange && Time.time >= lastSpawnTime + spawnCooldown)
        {
            TrySpawnEnemies();
        }
    }

    private void TrySpawnEnemies()
    {
        // Spawn only if current spawned enemies are below the max limit
        if (currentSpawnedEnemies < maxSpawnedEnemies)
        {
            foreach (Vector2 offset in spawnOffsets)
            {
                if (currentSpawnedEnemies >= maxSpawnedEnemies) break;

                // Calculate spawn position
                Vector3 spawnPosition = transform.position + new Vector3(offset.x, offset.y, 0);

                // Instantiate the enemy
                Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

                // Increment the spawned enemies counter
                currentSpawnedEnemies++;
            }

            // Update the last spawn time
            lastSpawnTime = Time.time;
        }
    }

    public void OnSpawnedEnemyDestroyed()
    {
        // Decrement the counter when a spawned enemy is destroyed
        currentSpawnedEnemies = Mathf.Max(0, currentSpawnedEnemies - 1);
    }
}
