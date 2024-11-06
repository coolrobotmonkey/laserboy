using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    public Transform target;          // Reference to the player
    public float smoothSpeed = 0.125f; // Smoothness of the camera movement
    public Vector3 offset;             // Offset of the camera position relative to the player

    // Bounding box limits
    public Vector2 minBounds;          // Minimum x and y values the camera can go to
    public Vector2 maxBounds;          // Maximum x and y values the camera can go to

    private void LateUpdate()
    {
        if (target != null)
        {
            // Define the target position with the offset applied
            Vector3 desiredPosition = target.position + offset;
            
            // Smoothly interpolate to the target position
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // Clamp the camera position to be within the bounding box
            float clampedX = Mathf.Clamp(smoothedPosition.x, minBounds.x, maxBounds.x);
            float clampedY = Mathf.Clamp(smoothedPosition.y, minBounds.y, maxBounds.y);

            // Update the camera position with the clamped values
            transform.position = new Vector3(clampedX, clampedY, transform.position.z);
        }
    }
}
