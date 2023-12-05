using UnityEngine;

public class MovingRotatingItem : MonoBehaviour
{
    public float moveDistance = 1f;      // Distance to move up and down
    public float moveSpeed = 2f;         // Speed of the up and down movement
    public float rotationSpeed = 50f;    // Speed of the rotation

    private float initialY;              // Initial Y position

    void Start()
    {
        // Record the initial Y position
        initialY = transform.position.y;
    }

    void Update()
    {
        // Move up and down
        float newY = initialY + Mathf.Sin(Time.time * moveSpeed) * moveDistance;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);

        // Rotate around the Z-axis
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
    }
}
