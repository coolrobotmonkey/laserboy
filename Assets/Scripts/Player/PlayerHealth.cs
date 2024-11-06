using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public List<GameObject> hearts;
    public Color hitColor = Color.red;  // Color to change to when hit
    public float colorChangeDuration = 0.1f;  // Duration of the color change effect

    private Color originalColor;  // Original color of the player

    private SpriteRenderer spriteRenderer;

    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    public void OnCollisionEnter2D(UnityEngine.Collision2D collision)
    {
        if(collision.collider.CompareTag("Enemy")) {
            TakeDamage();
        }
    }

    public void TakeDamage() {
        StartCoroutine(FlashColour());

        int heartCount = 0;
        foreach(GameObject heart in hearts) {
            if(heart.activeInHierarchy) {
                heartCount++;
            }
        }

        if(heartCount == 1) {
            SceneManager.LoadScene(0);
        }

        hearts[heartCount - 1].SetActive(false);
    }

    private IEnumerator FlashColour() {
        spriteRenderer.color = hitColor;

        yield return new WaitForSeconds(colorChangeDuration);

        spriteRenderer.color = originalColor;
    }
}
