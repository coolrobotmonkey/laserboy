using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100;
    public float currentHealth;
    public HealthBar healthBar;

    public Color hitColor = Color.red;  // Color to change to when hit
    public float colorChangeDuration = 0.1f;  // Duration of the color change effect
    private Color originalColor;  // Original color of the player

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        // Initialize health and health bar
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(10);
        }
    }

    public void TakeDamage(float damage)
    {
        StartCoroutine(FlashColour());

        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private IEnumerator FlashColour() 
    {
        // Change the player's colour to indicate damage
        spriteRenderer.color = hitColor;
        yield return new WaitForSeconds(colorChangeDuration);
        spriteRenderer.color = originalColor;
    }

    private void Die()
    {
        Debug.Log("player died");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void AddHealth(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Ensure health stays within bounds
        healthBar.SetHealth(currentHealth);
    }
}
