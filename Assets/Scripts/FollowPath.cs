using UnityEngine;

public class FollowPathWithDisappearance : MonoBehaviour
{
    public Transform[] waypoints; // Array of waypoints for the path
    private float speed = 1.0f; // Speed of the movement
    public float teleportDelay = 0.5f; // Delay before teleporting to the first waypoint

    private int currentWaypointIndex = 0; // Start at the first waypoint
    private float progress = 0.0f; // Progress between waypoints
    private Renderer objectRenderer;

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
    }

    void Update()
    {
        if (waypoints.Length == 0) return;

        // Move the object along the path
        MoveAlongPath();
    }

    private void MoveAlongPath()
    {
        // Get the current and next waypoints
        Transform currentWaypoint = waypoints[currentWaypointIndex];
        Transform nextWaypoint = waypoints[(currentWaypointIndex + 1) % waypoints.Length];

        // Increment progress over time
        progress += speed * Time.deltaTime;

        // Interpolate position between the current and next waypoints
        transform.position = Vector3.Lerp(currentWaypoint.position, nextWaypoint.position, progress);

        // Check if the object has reached the next waypoint
        if (progress >= 1.0f)
        {
            // Move to the next waypoint
            currentWaypointIndex++;
            currentWaypointIndex = currentWaypointIndex % waypoints.Length;

            // If we've reached the last waypoint, teleport the object back to the start
            if ((currentWaypointIndex + 1) % waypoints.Length == 0)
            {
                // Start the teleportation process
                StartCoroutine(TeleportToStart());
            }
            else
            {
                progress = 0.0f; // Reset progress for the next segment
            }
        }
    }

    private System.Collections.IEnumerator TeleportToStart()
    {
        // Make the object disappear
        objectRenderer.enabled = false;

        // Wait for the delay
        yield return new WaitForSeconds(teleportDelay);

        // Immediately set the position to the first waypoint (no interpolation)
        transform.position = waypoints[0].position;

        // Reset the waypoint index and progress
        currentWaypointIndex = 0;
        progress = 0.0f;

        // Make the object reappear
        objectRenderer.enabled = true;
    }
}
