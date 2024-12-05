using UnityEngine;

public class newCamera : MonoBehaviour
{
    public Transform target;        // The target the camera will orbit around (your character).
    public float distance = 5.0f;   // Distance from the target.
    public float rotationSpeed = 5.0f;  // Speed of the camera rotation.

    private float currentX = 0.0f;  // Horizontal rotation.
    private float currentY = 0.0f;  // Vertical rotation.
    public float minY = -20f;       // Minimum vertical angle.
    public float maxY = 60f;        // Maximum vertical angle.

    void Update()
    {
        // Get mouse input.
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // Rotate the camera based on mouse input.
        currentX += mouseX * rotationSpeed;
        currentY -= mouseY * rotationSpeed;
        currentY = Mathf.Clamp(currentY, minY, maxY);  // Clamp vertical rotation to prevent flipping.
    }

    void LateUpdate()
    {
        // Update the camera position and rotation.
        Vector3 direction = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        transform.position = target.position + rotation * direction;
        transform.LookAt(target);  // Make the camera look at the target.
    }
}