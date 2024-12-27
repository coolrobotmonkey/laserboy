using UnityEngine;
using System.Collections;

public abstract class Enemy : MonoBehaviour
{
    [Header("Enemy Stats")]
    public float hitPoints = 1000;
    public float damage = 10;

    [Header("Visual Feedback")]
    public Color hitColor = Color.red;
    public float colorChangeDuration = 0.1f;

    protected Color originalColor;
    protected SpriteRenderer spriteRenderer;
    //protected Transform player;
    protected EnemySpawner enemySpawner;

    protected virtual void Start()
    {
        // Initialize components and variables
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;

        enemySpawner = FindObjectOfType<EnemySpawner>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.collider.GetComponent<PlayerHealth>();
            if (playerHealth != null) // Conditional could be removed
            {
                playerHealth.TakeDamage(damage);
            }
        }
    }

    public virtual void TakeDamage(float damageAmount)
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

    protected abstract void Die(); // Force derived classes to define their own death behavior
}
