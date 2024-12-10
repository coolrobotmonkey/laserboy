using UnityEngine;

// Will tell the EnemySummoner that this has been destroyed and therefore it can free up a spot to summon more enemies
public class SpawnedEnemy : MonoBehaviour
{
    private void OnDestroy()
    {
        EnemySummoner spawner = FindObjectOfType<EnemySummoner>();
        if (spawner != null)
        {
            spawner.OnSpawnedEnemyDestroyed();
        }
    }
}
