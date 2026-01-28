using UnityEngine;

public class DynamicCameraSize : MonoBehaviour
{
    public float targetAspectRatio = 16f / 9f;
    public float defaultSize = 5f; 

    void Start()
    {
        // Run this logic ONCE to set the initial "Safe Zoom"
        float currentAspect = (float)Screen.width / (float)Screen.height;
        
        if (currentAspect < targetAspectRatio)
        {
            // If screen is narrow (Mobile Portrait), zoom out automatically 
            // so we don't cut off the sides of the city.
            GetComponent<Camera>().orthographicSize = defaultSize * (targetAspectRatio / currentAspect);
        }
        else
        {
            // If screen is wide (PC), stick to the design default
            GetComponent<Camera>().orthographicSize = defaultSize;
        }
    }
}