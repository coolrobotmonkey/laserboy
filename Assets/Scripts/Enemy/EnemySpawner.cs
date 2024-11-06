using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Vector2 spawnMinBounds;
    public Vector2 spawnMaxBounds;
    
    void Start() {
        Respawn();
    }

    public void Respawn() {
        float randomX = Random.Range(spawnMinBounds.x, spawnMaxBounds.x);
        float randomY = Random.Range(spawnMinBounds.y, spawnMaxBounds.y);
        Vector3 randomVector = new Vector3(randomX, randomY, 0);

        Instantiate(enemyPrefab, randomVector, Quaternion.identity);
    }
}
