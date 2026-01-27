using UnityEngine;

public class IsoCameraController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 50f;
    public float dragSpeed = 350f;
    public float smoothTime = 4f;

    [Header("Bounds (Prevent flying too high/low)")]
    public bool enableBounds = true;
    public Vector2 heightBounds = new Vector2(0f, 50f); // Min Y, Max Y
    public Vector2 mapBoundsX = new Vector2(-100f, 100f);
    public Vector2 mapBoundsZ = new Vector2(-100f, 100f);

    [Header("Zoom Settings (Orthographic)")]
    public float zoomStep = 10f;
    public float minOrthoSize = 5f;
    public float maxOrthoSize = 50f;
    public float zoomDampening = 10f;

    private Vector3 targetPosition;
    private float targetOrthoSize;
    private Camera cam;

    void Start()
    {
        cam = GetComponentInChildren<Camera>();
        targetPosition = transform.position;
        targetOrthoSize = cam.orthographicSize;
    }

    void Update()
    {
        if (Time.timeScale == 0) return;

        HandleMovement();
        HandleZoom();
        ApplyTransformations();
    }

    void HandleMovement()
    {
        // 1. KEYBOARD (WASD) - Stays flat on ground (XZ)
        float xInput = Input.GetAxisRaw("Horizontal");
        float zInput = Input.GetAxisRaw("Vertical");
        Vector3 moveDir = new Vector3(xInput, 0, zInput).normalized;
        targetPosition += moveDir * moveSpeed * Time.deltaTime;

        // 2. MOUSE DRAG - Now moves in Y-Plane (Screen Space)
        if (Input.GetMouseButton(1))
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            // We use the CAMERA's Local Up/Right vectors instead of World X/Z
            // This means dragging UP moves the camera along its own 'Up' axis (which includes Y)
            Vector3 screenMove = (transform.right * -mouseX) + (transform.up * -mouseY);

            targetPosition += screenMove * dragSpeed * Time.deltaTime;
        }

        // 3. BOUNDS CHECK (Crucial when adding Y movement)
        if (enableBounds)
        {
            float clampedX = Mathf.Clamp(targetPosition.x, mapBoundsX.x, mapBoundsX.y);
            float clampedY = Mathf.Clamp(targetPosition.y, heightBounds.x, heightBounds.y);
            float clampedZ = Mathf.Clamp(targetPosition.z, mapBoundsZ.x, mapBoundsZ.y);
            targetPosition = new Vector3(clampedX, clampedY, clampedZ);
        }
    }

    void HandleZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0)
        {
            targetOrthoSize -= scroll * zoomStep;
            targetOrthoSize = Mathf.Clamp(targetOrthoSize, minOrthoSize, maxOrthoSize);
        }
    }

    void ApplyTransformations()
    {
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smoothTime);
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetOrthoSize, Time.deltaTime * zoomDampening);
    }
}