using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bee : MonoBehaviour
{

    private HashSet<int> processedBalloons = new HashSet<int>();
    private PolygonCollider2D boundaryCollider;

    void Start(){
        boundaryCollider = GameObject.Find("maze1").GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()

    {

        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 newPosition = transform.position;

        newPosition = new Vector2(mousePosition.x, mousePosition.y);



        // Check for collision with boundary

        if (!boundaryCollider.OverlapPoint(newPosition))

        {
            Debug.Log("has collided");

            // If collision detected, adjust the position to stay within bounds

            newPosition = ClampPositionToBounds(newPosition, boundaryCollider);

        }



        transform.position = newPosition;

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
