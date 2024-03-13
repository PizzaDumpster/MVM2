using System.Collections;
using UnityEngine;

public class PillarMovement : MonoBehaviour
{
    public GameObject _Pillar;
    public Vector3[] waypoints; // Array of waypoints defining the movement path
    public float dropSpeed = 5f; // Speed at which the pillar drops
    public float riseSpeed = 2f; // Speed at which the pillar rises
    public float pauseDuration = 1f; // Duration to wait at the top and bottom positions

    public int currentWaypointIndex = 0; // Index of the current waypoint
    public bool isDropping = false; // Flag indicating if the pillar is dropping
    public bool isRising = false; // Flag indicating if the pillar is rising
    public bool isPaused = false; // Flag indicating if the pillar is paused

    void Start()
    {
        // Start dropping the pillar initially
        Drop();
    }

    void Update()
    {
        if (isDropping)
        {
            MoveTowardsWaypoint(dropSpeed);
        }
        else if (isRising)
        {
            MoveTowardsWaypoint(-riseSpeed);
        }
    }

    void MoveTowardsWaypoint(float speed)
    {
        Vector3 targetPosition = waypoints[currentWaypointIndex];
        float distance = Vector3.Distance(_Pillar.transform.localPosition, targetPosition);

        if (distance <= Mathf.Abs(speed * Time.deltaTime))
        {
            // If close enough to the target position, directly set position to the target
            _Pillar.transform.localPosition = targetPosition;

            // Check if at the top or bottom waypoint
            if (currentWaypointIndex == 0 || currentWaypointIndex == waypoints.Length - 1)
            {
                StartCoroutine(WaitAndChangeDirection());
            }
            else
            {
                // Move to the next waypoint
                currentWaypointIndex += (int)Mathf.Sign(speed);
            }
        }
        else
        {
            // Move towards the target position
            _Pillar.transform.localPosition += (targetPosition - _Pillar.transform.localPosition).normalized * speed * Time.deltaTime;
        }
    }

    IEnumerator WaitAndChangeDirection()
    {
        isDropping = false;
        isRising = false;
        isPaused = true;

        yield return new WaitForSeconds(pauseDuration);

        isPaused = false;

        if (currentWaypointIndex == 0)
        {
            currentWaypointIndex = 1;
            isRising = true; // Start rising after pausing at the bottom waypoint
        }
        else
        {
            currentWaypointIndex = 0;
            isDropping = true; // Start dropping after pausing at the top waypoint
        }
    }

    void Drop()
    {
        isDropping = true;
    }
}
