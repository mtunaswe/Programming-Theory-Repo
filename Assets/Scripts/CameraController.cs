using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 40f; // Speed for WASD
    public float dragSpeed = 1000f; // Speed for Mouse Drag
    public float smoothTime = 2f; // How "heavy" the camera feels (Higher = snappier, Lower = floatier)

    private Vector3 targetPosition;

    void Start()
    {
        // Initialize target to current position so it doesn't jump at start
        targetPosition = transform.position;
    }

    void Update()
    {
        // ABSTRACTION: Pause camera if game is over
        if (Time.timeScale == 0) return;

        HandleInput();
        MoveCamera();
    }

    void HandleInput()
    {
        // 1. KEYBOARD INPUT (WASD)
        float xInput = Input.GetAxisRaw("Horizontal"); // Raw = instant response
        float zInput = Input.GetAxisRaw("Vertical");

        Vector3 moveDir = new Vector3(xInput, 0, zInput).normalized;
        
        // Update the TARGET position, not the actual position yet
        targetPosition += moveDir * moveSpeed * Time.deltaTime;

        // 2. MOUSE DRAG INPUT (Right Click)
        if (Input.GetMouseButton(1)) 
        {
            // Input.GetAxis("Mouse X/Y") handles smoothing automatically
            float mouseX = Input.GetAxis("Mouse X"); 
            float mouseY = Input.GetAxis("Mouse Y");

            // Invert logic: Dragging mouse LEFT should pull map RIGHT
            Vector3 dragDir = new Vector3(-mouseX, 0, -mouseY);
            
            // Apply drag to target
            targetPosition += dragDir * dragSpeed * Time.deltaTime;
        }
    }

    void MoveCamera()
    {
        // LERP: Smoothly slide from current position to target position
        // This removes the "jitter" and makes it feel professional
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smoothTime);
    }
}