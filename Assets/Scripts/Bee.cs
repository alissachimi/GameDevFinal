using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee : MonoBehaviour
{

    private HashSet<int> processedBalloons = new HashSet<int>();
    private HashSet<int> cooldownEnemies = new HashSet<int>();
    public float collisionCooldown = .5f;

    // Update is called once per frame
    void Update()
    {
        // get mouse position and set bee to it
        Vector3 mouseScreenPosition = Input.mousePosition;
        mouseScreenPosition.z = -Camera.main.transform.position.z;
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
        transform.position = mouseWorldPosition;
    }

    void OnCollisionEnter(Collision collision)
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
            int objectId = collision.gameObject.GetInstanceID();

            // Ensure each enemy collision is only processed once per cooldown period
            if (!cooldownEnemies.Contains(objectId))
            {
                cooldownEnemies.Add(objectId);
                Main.S.EnemyCollision();
                StartCoroutine(CooldownEnemyCollision(objectId));
            }
        }
    }

    private IEnumerator CooldownEnemyCollision(int enemyId)
    {
        yield return new WaitForSeconds(collisionCooldown);
        cooldownEnemies.Remove(enemyId);
    }
}
