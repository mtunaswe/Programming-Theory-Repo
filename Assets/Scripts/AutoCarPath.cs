using UnityEngine;
using System.Collections.Generic;

public class AutoCarPath : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 5f;
    public float turnSpeed = 10f;
    
    private List<Transform> waypoints = new List<Transform>();
    private int currentTargetIndex = 0;
    private bool pathFound = false;

    void Start()
    {
        FindNearestPath();
    }

    void FindNearestPath()
    {
        // 1. FIND ALL PATHS
        GameObject[] allPaths = GameObject.FindGameObjectsWithTag("RoadPath");
        
        if (allPaths.Length == 0)
        {
            Debug.LogWarning("No paths found! Did you forget to tag them 'RoadPath'?");
            return;
        }

        // 2. FIND NEAREST PATH OBJECT
        GameObject nearestPath = null;
        float minDistance = Mathf.Infinity;

        foreach (GameObject path in allPaths)
        {
            float dist = Vector3.Distance(transform.position, path.transform.position);
            if (dist < minDistance)
            {
                minDistance = dist;
                nearestPath = path;
            }
        }

        // 3. EXTRACT WAYPOINTS & INITIALIZE SMART START
        if (nearestPath != null)
        {
            // Get all children (excluding parent)
            Transform[] potentialPoints = nearestPath.GetComponentsInChildren<Transform>();
            foreach (Transform t in potentialPoints)
            {
                if (t != nearestPath.transform)
                {
                    waypoints.Add(t);
                }
            }

            if (waypoints.Count > 0)
            {
                // --- NEW LOGIC ADDED HERE ---
                // Instead of starting at 0, calculate the best index based on current position
                currentTargetIndex = GetClosestWaypointIndex();
                
                pathFound = true;
                Debug.Log($"{name} attached to {nearestPath.name} starting at Point {currentTargetIndex}");
            }
        }
    }

    // This helper function scans the now-filled 'waypoints' list
    int GetClosestWaypointIndex()
    {
        int closestIndex = 0;
        float minDistance = Mathf.Infinity;

        for (int i = 0; i < waypoints.Count; i++)
        {
            float dist = Vector3.Distance(transform.position, waypoints[i].position);
            
            if (dist < minDistance)
            {
                minDistance = dist;
                closestIndex = i;
            }
        }
        return closestIndex;
    }

    void Update()
    {
        if (!pathFound) return;
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

        // Check Arrival
        if (Vector3.Distance(transform.position, target.position) < 0.2f)
        {
            currentTargetIndex = (currentTargetIndex + 1) % waypoints.Count;
        }
    }
}