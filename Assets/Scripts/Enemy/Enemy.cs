using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 2f;          // Movement speed of the enemy
    public int hitPoints = 1000;
    private SpriteRenderer spriteRenderer;
    public Color originalColor;  // Original color of the enemy
    public Color hitColor = Color.red;  // Color to change to when hit
    public float colorChangeDuration = 0.1f;  // Duration of the color change effect

    private Transform player;
    private EnemySpawner enemySpawner;

    private void Start()
    {
        // Get the SpriteRenderer component and store the original color
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        enemySpawner = FindObjectOfType<EnemySpawner>();
    }

    private void Update()
    {
        if (player != null)
        {
            // Calculate the direction from the enemy to the player
            Vector3 direction = (player.position - transform.position).normalized;
            
            // Move the enemy towards the player
            transform.position += direction * speed * Time.deltaTime;
        }
    }

    public void TakeDamage() {
        hitPoints -= 1;

        StartCoroutine(FlashColour());

        // Die if lost all hitpoints
        if(hitPoints <= 0) {
            Destroy(gameObject);

            if(enemySpawner != null) {
                enemySpawner.Respawn();
            }
        }
    }

    private IEnumerator FlashColour() {
        spriteRenderer.color = hitColor;

        yield return new WaitForSeconds(colorChangeDuration);

        spriteRenderer.color = originalColor;
    }
}
