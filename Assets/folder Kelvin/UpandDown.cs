using UnityEngine;
using UnityEngine.UI;

public class MovingText : MonoBehaviour
{
    public float moveDistance = 1f;      // Distance to move up and down
    public float moveSpeed = 2f;         // Speed of the up and down movement

    private float initialY;              // Initial Y position

    void Start()
    {
        // Record the initial Y position
        initialY = transform.position.y;
    }

    void Update()
    {
        // Move text up and down
        float newY = initialY + Mathf.Sin(Time.time * moveSpeed) * moveDistance;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
