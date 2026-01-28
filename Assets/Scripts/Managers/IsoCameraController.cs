using UnityEngine;
using UnityEngine.EventSystems; 

public class IsoCameraController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 50f;
    public float dragSpeed = 300f;
    public float smoothTime = 4f;

    [Header("Mobile Settings")]
    public float mobilePanSpeed = 0.5f;
    public float mobileZoomSpeed = 0.1f;

    [Header("Bounds")]
    public bool enableBounds = true;
    public Vector2 heightBounds = new Vector2(0f, 100f);
    public Vector2 mapBoundsX = new Vector2(-140f, 100f);
    public Vector2 mapBoundsZ = new Vector2(-50f, 200f);

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
        
        // Mobile "Safe Zoom" initialization
        if (Application.isMobilePlatform)
        {
            // Optional: Start slightly zoomed out on mobile
            targetOrthoSize = Mathf.Clamp(targetOrthoSize + 5, minOrthoSize, maxOrthoSize);
        }
    }

    void Update()
    {
        if (Time.timeScale == 0) return;

        // PRIORITY: If touching screen, use Mobile logic. Otherwise use PC logic.
        if (Input.touchCount > 0)
        {
            HandleTouchInput();
        }
        else
        {
            HandlePCMovement();
            HandlePCZoom();
        }

        ApplyTransformations();
    }

    // --- MOBILE LOGIC ---
    void HandleTouchInput()
    {
        // 1. Avoid UI Clicks (Touching buttons shouldn't move camera)
        if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)) return;

        // 2. PANNING (One Finger)
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {
                // Dragging on mobile feels best when you "pull the ground"
                // So we invert the direction (-)
                float xMove = -touch.deltaPosition.x * mobilePanSpeed * Time.deltaTime;
                float zMove = -touch.deltaPosition.y * mobilePanSpeed * Time.deltaTime;

                // Move relative to camera orientation (so Up is "North" relative to screen)
                Vector3 move = (transform.right * xMove) + (transform.forward * zMove);
                
                // Lock Y for panning (Zoom handles height)
                move.y = 0; 
                
                targetPosition += move * 50f; // Multiplier to match PC feel
            }
        }
        // 3. ZOOMING (Two Fingers)
        else if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            // Calculate distance between fingers in previous frame vs current frame
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

            float difference = prevMagnitude - currentMagnitude;

            // Apply zoom
            targetOrthoSize += difference * mobileZoomSpeed;
        }
        
        ApplyBounds(); // Ensure mobile doesn't drag us off map
    }

    // --- PC LOGIC (Your Original Code) ---
    void HandlePCMovement()
    {
        // Keyboard
        float xInput = Input.GetAxisRaw("Horizontal");
        float zInput = Input.GetAxisRaw("Vertical");
        Vector3 moveDir = new Vector3(xInput, 0, zInput).normalized;
        targetPosition += moveDir * moveSpeed * Time.deltaTime;

        // Mouse Drag
        if (Input.GetMouseButton(1))
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");
            Vector3 screenMove = (transform.right * -mouseX) + (transform.up * -mouseY);
            targetPosition += screenMove * dragSpeed * Time.deltaTime;
        }

        ApplyBounds();
    }

    void HandlePCZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0)
        {
            targetOrthoSize -= scroll * zoomStep;
        }
    }

    // --- SHARED HELPERS ---
    void ApplyBounds()
    {
        if (enableBounds)
        {
            float clampedX = Mathf.Clamp(targetPosition.x, mapBoundsX.x, mapBoundsX.y);
            float clampedY = Mathf.Clamp(targetPosition.y, heightBounds.x, heightBounds.y);
            float clampedZ = Mathf.Clamp(targetPosition.z, mapBoundsZ.x, mapBoundsZ.y);
            targetPosition = new Vector3(clampedX, clampedY, clampedZ);
        }
        
        // Clamp Zoom Target
        targetOrthoSize = Mathf.Clamp(targetOrthoSize, minOrthoSize, maxOrthoSize);
    }

    void ApplyTransformations()
    {
        // Smoothly move the actual object
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smoothTime);
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetOrthoSize, Time.deltaTime * zoomDampening);
    }
}