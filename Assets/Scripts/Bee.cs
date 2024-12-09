using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bee : MonoBehaviour
{

    private HashSet<int> processedBalloons = new HashSet<int>();

    public LayerMask mazeLayerMask; // Layer mask for the maze
    public GameObject starPrefab;
    public GameObject enemyColPrefab;
    public AudioClip popSound; // Assign this in the inspector
    public AudioClip enemySound;
    private AudioSource audioSource;

    void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Main.S.paused == false){
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

                // Instantiate the star prefab at the position of the balloon
                GameObject star = Instantiate(starPrefab, collision.transform.position, Quaternion.Euler(-180, 90, 90));

                // Destroy the balloon
                Destroy(collision.gameObject);

                // Add score and increment the balloon counter
                Main.S.score += 10;
                Main.S.numBalloonsPopped += 1;

                // Destroy the star after 1 second
                Destroy(star, 1.0f);
                audioSource.PlayOneShot(popSound, .3f);
                
                // update player prefs if needed
                if (Main.S.score > PlayerPrefs.GetInt("HighScore")){
                    PlayerPrefs.SetInt("HighScore", Main.S.score);
                }
            }
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            GameObject enemy = Instantiate(enemyColPrefab, this.transform.position, Quaternion.Euler(-90, 0, 0));
            
            Destroy(enemy, 1.5f);
            AudioSource.PlayClipAtPoint(enemySound, this.transform.position, 1.5f);
            if(collision.gameObject.layer == LayerMask.NameToLayer("Bomb")){
                Destroy(collision.gameObject);
            }
            Main.S.EnemyCollision();
        }
    }


}
