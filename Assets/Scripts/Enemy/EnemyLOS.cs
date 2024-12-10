using UnityEngine;

public class EnemyLOS : MonoBehaviour
{
    [Header("Detection Settings")]
    public float detectionRange = 5f;           // How far can the enemy detect the player
    public LayerMask obstacleLayers;             // Layers that can block line of sight (e.g., walls)
    
    [Header("Movement Settings")]
    public float speed = 2f;                     // Movement speed of the enemy
    private Transform player;
    private Vector3 movement;
    Animator anim;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        // If there's no player, do nothing
        if (player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // Check if player is within detection range
        if (distanceToPlayer <= detectionRange)
        {
            // Perform a line-of-sight check
            if (HasLineOfSightToPlayer())
            {
                // Move towards the player since they're in range and line of sight is clear
                MoveTowardsPlayer();
            }
            else
            {
                // Player is in range but line of sight is blocked, so do nothing
            }
        }
        else
        {
            // Player is not in detection range, so do nothing
        }
    }

    private bool HasLineOfSightToPlayer()
    {
        // Determine the direction to the player
        Vector2 direction = (player.position - transform.position).normalized;
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // Raycast from enemy to player to check for obstacles
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, distanceToPlayer, obstacleLayers);

        // If the ray hits nothing, line of sight is clear
        if (hit.collider == null)
        {
            return true;
        }

        // If the ray hit something, it means there's an obstacle blocking line of sight
        return false;
    }

    private void MoveTowardsPlayer()
    {
        // Calculate direction towards the player
        Vector3 direction = (player.position - transform.position).normalized;
        movement = direction;
        // Move enemy towards the player
        transform.position += direction * speed * Time.deltaTime;

        if (anim != null)
        {
            UpdateAnimator(movement);
        }
    }

    private void UpdateAnimator(Vector3 movement)
    {
        // If the Animator exists, update its parameters
        if (anim != null)
        {
            if (movement.magnitude > 0.01f)
            {
                anim.SetFloat("Horizontal", movement.x);
                anim.SetFloat("Vertical", movement.y);
                anim.SetBool("IsMoving", true);
            }
            else
            {
                anim.SetBool("IsMoving", false);
            }
        }
    }
}
