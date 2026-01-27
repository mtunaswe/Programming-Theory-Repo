using UnityEngine;
using System.Collections.Generic;

public class ParkingVehicle : MonoBehaviour
{
    [Header("Configuration")]
    public ParkingPathManager assignedPath; // Drag the Path Parent here
    public float speed = 5f;
    public float turnSpeed = 10f;

    private List<Transform> waypoints;
    private int currentTargetIndex = 0;
    private int myParkingIndex = -1; // The specific index where THIS car stops
    private bool isParked = false;

    void Start()
    {
        if (assignedPath == null) return;

        // 1. Get the full list of points so we can drive on them
        waypoints = assignedPath.parkingSpots;

        // 2. SMART START: Ask the Manager for my specific stopping point
        myParkingIndex = assignedPath.AssignParkingSpot();

        // 3. OPTIMIZATION: Jump to the nearest point on the path (like before)
        // Only do this if we actually got a valid spot
        if (myParkingIndex != -1)
        {
            currentTargetIndex = GetClosestWaypointIndex();
        }
    }

    void Update()
    {
        // Stop if parked, no path, or full lot (-1)
        if (isParked || assignedPath == null || myParkingIndex == -1) return;

        MoveCar();
    }

    void MoveCar()
    {
        Transform target = waypoints[currentTargetIndex];

        // Move
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        // Rotate
        Vector3 direction = target.position - transform.position;
        if (direction != Vector3.zero)
        {
            Quaternion lookRot = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRot, Time.deltaTime * turnSpeed);
        }

        // Check Arrival at current waypoint
        if (Vector3.Distance(transform.position, target.position) < 0.2f)
        {
            // --- THE SMART CHECK ---
            
            // Have we reached OUR assigned parking spot?
            if (currentTargetIndex == myParkingIndex)
            {
                // YES: Stop here.
                Debug.Log($"{name} parked at spot {myParkingIndex}");
                isParked = true;
                transform.rotation = target.rotation; // Align perfectly
            }
            else
            {
                // NO: Keep driving to the next point
                currentTargetIndex++;
            }
        }
    }

    // Helper to find where to start driving
    int GetClosestWaypointIndex()
    {
        int closest = 0;
        float minDst = Mathf.Infinity;
        // Optimization: Don't scan past our parking spot!
        for (int i = 0; i <= myParkingIndex; i++)
        {
            float dst = Vector3.Distance(transform.position, waypoints[i].position);
            if (dst < minDst)
            {
                minDst = dst;
                closest = i;
            }
        }
        return closest;
    }
}