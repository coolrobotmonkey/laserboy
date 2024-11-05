using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    public Transform target;          // Reference to the player
    public float smoothSpeed = 0.125f; // Smoothness of the camera movement
    public Vector3 offset;             // Offset of the camera position relative to the player

    private void LateUpdate()
    {
        if (target != null)
        {
            // Define the target position with the offset applied
            Vector3 desiredPosition = target.position + offset;
            
            // Smoothly interpolate to the target position
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            
            // Update the camera position
            transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, transform.position.z);
        }
    }
}
