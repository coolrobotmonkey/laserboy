using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Stats")]
    public float speed = 2f;
    public int hitPoints = 1000;

    [Header("Visual Feedback")]
    public Color hitColor = Color.red;
    public float colorChangeDuration = 0.1f;

    protected Color originalColor;
    protected SpriteRenderer spriteRenderer;
    protected Transform player;
    protected EnemySpawner enemySpawner;

    protected virtual void Start()
    {
        // Initialize components and variables
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;

        enemySpawner = FindObjectOfType<EnemySpawner>();
    }

    public virtual void TakeDamage(int damageAmount)
    {
        hitPoints -= damageAmount;

        StartCoroutine(FlashColor());

        if (hitPoints <= 0)
        {
            Die();
        }
    }

    protected virtual IEnumerator FlashColor()
    {
        spriteRenderer.color = hitColor;
        yield return new WaitForSeconds(colorChangeDuration);
        spriteRenderer.color = originalColor;
    }

    protected virtual void Die()
    {
        Destroy(gameObject);

        if (enemySpawner != null)
        {
            enemySpawner.Respawn();
        }
    }
}
