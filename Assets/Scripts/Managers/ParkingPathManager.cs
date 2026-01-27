using UnityEngine;
using System.Collections.Generic;

public class ParkingPathManager : MonoBehaviour
{
    // This list holds all the parking spots (waypoints)
    [HideInInspector]
    public List<Transform> parkingSpots = new List<Transform>();

    // This tracks which spot we are currently handing out
    // We start from the END of the list (the deepest parking spot)
    private int currentSpotIndex;

    void Awake()
    {
        // 1. Gather all waypoints (children)
        Transform[] allChildren = GetComponentsInChildren<Transform>();
        foreach (Transform t in allChildren)
        {
            if (t != transform) // Don't add the parent itself
            {
                parkingSpots.Add(t);
            }
        }

        // 2. Set the counter to the last spot (e.g., Index 9)
        currentSpotIndex = parkingSpots.Count - 1;
    }

    // This is the public function cars will call to get a ticket
    public int AssignParkingSpot()
    {
        // If we have spots left...
        if (currentSpotIndex >= 0)
        {
            int assignedIndex = currentSpotIndex;
            currentSpotIndex--; // Decrement so the next car gets the previous spot
            return assignedIndex;
        }
        else
        {
            // No spots left!
            Debug.LogWarning("Parking Lot is full!");
            return -1;
        }
    }
}