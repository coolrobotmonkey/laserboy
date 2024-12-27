using UnityEngine;

public class EnemyMoveTowardsPlayer : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    private Vector3 movement;
    private Transform player; // Included in enemy abstract class in case you wanted to not have movement be separated
    private Animator anim;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        anim = GetComponent<Animator>();

    }

    private void Update()
    {
        if (player != null)
        {
            // Calculate direction and move towards the player
            Vector3 direction = (player.position - transform.position).normalized;
            movement = direction;
            transform.position += direction * speed * Time.deltaTime;

            if (anim != null)
            {
                // Update animator parameters (if Animator exists)
                if (movement.x != 0 || movement.y != 0)
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
}