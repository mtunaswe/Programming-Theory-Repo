using UnityEngine;
using System.Collections.Generic;

public class CarPathFollower : MonoBehaviour
{
    [Header("Configuration")]
    public Transform[] waypoints; // Drag your path points here manually
    public float speed = 5f;
    public float turnSpeed = 10f;

    private int currentTargetIndex = 0;

    void Start()
    {
        // 1. SAFETY CHECK
        if (waypoints == null || waypoints.Length == 0)
        {
            Debug.LogWarning($"{name}: No waypoints assigned!");
            enabled = false; // Stop the script to prevent errors
            return;
        }

        // 2. SMART START: Find the closest point to start from
        currentTargetIndex = GetClosestWaypointIndex();
    }

    int GetClosestWaypointIndex()
    {
        int closestIndex = 0;
        float minDistance = Mathf.Infinity;

        // Loop through every point in the assigned path
        for (int i = 0; i < waypoints.Length; i++)
        {
            float dist = Vector3.Distance(transform.position, waypoints[i].position);
            
            if (dist < minDistance)
            {
                minDistance = dist;
                closestIndex = i;
            }
        }

        Debug.Log($"{name} starting at Point {closestIndex}");
        return closestIndex;
    }

    void Update()
    {
        if (waypoints.Length == 0) return;

        // 3. MOVE
        Transform targetPoint = waypoints[currentTargetIndex];
        transform.position = Vector3.MoveTowards(
            transform.position, 
            targetPoint.position, 
            speed * Time.deltaTime
        );

        // 4. ROTATE
        Vector3 direction = targetPoint.position - transform.position;
        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
        }

        // 5. NEXT POINT
        if (Vector3.Distance(transform.position, targetPoint.position) < 0.2f)
        {
            currentTargetIndex = (currentTargetIndex + 1) % waypoints.Length;
        }
    }
}