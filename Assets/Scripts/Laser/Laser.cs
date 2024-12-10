using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public Camera cam;
    public LineRenderer lineRenderer;
    public Transform firePoint;
    public GameObject startVFX;
    public GameObject endVFX;
    public float maxLaserLength = 10f; // Define the maximum length for the laser
    public SpriteRenderer gunSpriteRenderer; // Reference to the gun's SpriteRenderer
    public float vibrationAmount = 0.05f;
    public float vibrationSpeed = 25f;

    private List<ParticleSystem> particles = new List<ParticleSystem>();
    private Vector3 initialFirePointPosition;
    private Vector3 initialGunPosition;
    private Transform gunTransform;

    public int laserDamage = 1; // Damage the laser inflicts

    // Start is called before the first frame update
    void Start()
    {
        FillLists();
        DisableLaser();

        initialFirePointPosition = firePoint.localPosition;

        // Check if gunSpriteRenderer is assigned and set gunTransform and initialGunPosition
        if (gunSpriteRenderer != null)
        {
            gunTransform = gunSpriteRenderer.gameObject.transform;
            initialGunPosition = gunTransform.localPosition;
        }
        else
        {
            Debug.LogWarning("gunSpriteRenderer is not assigned in the Inspector.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1")) {
            EnableLaser();
        }

        if (Input.GetButton("Fire1")) {
            UpdateLaser();
        }

        if (Input.GetButtonUp("Fire1")) {
            DisableLaser();
        }

        RotateToMouse();
    }

    void EnableLaser() {
        lineRenderer.enabled = true;

        for (int i = 0; i < particles.Count; i++) {
            particles[i].Play();
        }
    }

    void UpdateLaser()
    {
        // Laser should follow the same direction as the gun
        Vector2 direction = firePoint.right;

        lineRenderer.SetPosition(0, firePoint.position);
        startVFX.transform.position = firePoint.position;

        // Cast the laser in the direction of the gun
        RaycastHit2D hit = Physics2D.Raycast((Vector2)firePoint.position, direction, maxLaserLength);

        if (hit) {
            lineRenderer.SetPosition(1, hit.point);

            // Shooting enemy
            Enemy enemy = hit.collider.GetComponent<Enemy>();

            if(enemy != null) {
                enemy.TakeDamage(laserDamage);
            }
        } else {
            Vector2 endPosition = (Vector2)firePoint.position + direction * maxLaserLength;
            lineRenderer.SetPosition(1, endPosition);
        }

        endVFX.transform.position = lineRenderer.GetPosition(1);

        // Vibrate gun while firing if gunTransform is assigned
        if (gunTransform != null) {
            float offsetX = Random.Range(-vibrationAmount, vibrationAmount);
            gunTransform.localPosition = initialGunPosition + new Vector3(offsetX, 0, 0);
        }
    }

    void DisableLaser() {
        lineRenderer.enabled = false;

        for (int i = 0; i < particles.Count; i++) {
            particles[i].Stop();
        }

        // Stop vibrating gun and reset position if gunTransform is assigned
        if (gunTransform != null) {
            gunTransform.localPosition = initialGunPosition;
        }
    }

    void RotateToMouse()
    {
        Vector2 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePosition - (Vector2)transform.position;

        // Calculate the angle and rotate the gun to point towards the mouse
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        // Flip the sprite based on the mouse's position relative to the gun
        if (gunSpriteRenderer != null) {
            bool shouldFlip = mousePosition.x < transform.position.x;
            gunSpriteRenderer.flipY = shouldFlip;

            // Update the firePoint position based on the flip state
            firePoint.localPosition = shouldFlip ? new Vector3(initialFirePointPosition.x, -initialFirePointPosition.y, initialFirePointPosition.z) : initialFirePointPosition;
        }
    }

    void FillLists() {
        for (int i = 0; i < startVFX.transform.childCount; i++) {
            var ps = startVFX.transform.GetChild(i).GetComponent<ParticleSystem>();
            if (ps != null) {
                particles.Add(ps);
            }
        }

        for (int i = 0; i < endVFX.transform.childCount; i++) {
            var ps = endVFX.transform.GetChild(i).GetComponent<ParticleSystem>();
            if (ps != null) {
                particles.Add(ps);
            }
        }
    }
}
