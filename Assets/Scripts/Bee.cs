using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bee : MonoBehaviour
{

    private HashSet<int> processedBalloons = new HashSet<int>();

    public LayerMask mazeLayerMask; // Layer mask for the maze

    void Update()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 currentPosition = transform.position;

        Vector2 direction = (mousePosition - currentPosition).normalized;
        float distance = Vector2.Distance(mousePosition, currentPosition);

        // Raycast to check if there is a wall between the bee and the mouse position
        RaycastHit2D hit = Physics2D.Raycast(currentPosition, direction, distance, mazeLayerMask);
        if (hit.collider != null)
        {
            // Wall detected, stop the bee from moving through the wall
            return;
        }

        transform.position = mousePosition;
    }



    // Helper function to clamp the player position within the collider bounds

    Vector3 ClampPositionToBounds(Vector3 newPosition, Collider2D boundary)
    {
        // Get the closest point on the polygon collider to the new position
        Vector2 closestPoint = boundary.ClosestPoint(newPosition);

        // Return the closest point as the new position if the new position is outside the collider
        return closestPoint;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Balloon"))
        {
            int objectId = collision.gameObject.GetInstanceID();
            
            // ensures each balloon is only processed once
            if (!processedBalloons.Contains(objectId))
            {
                processedBalloons.Add(objectId);
                Destroy(collision.gameObject);
                Main.S.score += 10;
            }
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            Main.S.EnemyCollision();
        }
    }


}
