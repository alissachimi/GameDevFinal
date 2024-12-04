using UnityEngine;

public class MoveUpDown : MonoBehaviour
{
    private float speed = 1.5f; // Speed of the movement
    private float height = 2.7f; // Height of the movement

    private Vector3 startPosition;

    void Start()
    {
        // Store the starting position of the object
        startPosition = transform.position;
    }

    void Update()
    {
        // Calculate the new Y position using Mathf.PingPong
        float newY = startPosition.y + Mathf.PingPong(Time.time * speed, height * 2) - height;

        // Set the object's position
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);
    }
}
