using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizantalMovement : MonoBehaviour
{
    public float speed = 1.0f; // Speed of the movement
    public float distance = 1.3f; // Distance of the movement

    private Vector3 startPosition;
    private float elapsedTime = 0.0f;

    void Start()
    {
        // Store the starting position of the object
        startPosition = transform.position;
    }

    void Update()
    {
        if (Main.S.paused == false){
            // Update elapsed time
            elapsedTime += Time.deltaTime * speed;

            // Calculate the proportion of the journey completed (0 to 1)
            float t = Mathf.PingPong(elapsedTime, 1.0f);

            // Use Mathf.SmoothStep to ease the movement
            float easedT = Mathf.SmoothStep(0.0f, 1.0f, t);

            // Calculate the new X position
            float newX = Mathf.Lerp(startPosition.x - distance, startPosition.x + distance, easedT);

            // Set the object's position
            transform.position = new Vector3(newX, startPosition.y, startPosition.z);
        }
    }
}
