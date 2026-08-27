using UnityEngine;
using UnityEngine.InputSystem;

public class MapNavigation : MonoBehaviour
{
    public RectTransform mapRoot;

    [Header("Zoom")]
    public float zoomSpeed = 0.001f;
    public float minZoom = 0.2f;
    public float maxZoom = 3.0f;
    public float zoomLerpSpeed = 5f;


    private float targetZoom = 1f;

    [Header("Pan")]
    public float panSpeed = 1.0f;
    public float keyPanSpeed = 500f;
    public float inertiaDamping = 5f;

    private Vector2 lastMousePos;
    private Vector2 inertiaVelocity;

    void Update()
    {
        HandleZoom();
        HandlePan();
        HandleKeyboardPan();
        ApplyInertia();
    }

    // ---------------------------------------------------------
    // 1. SMOOTH ZOOM + GOOGLE MAPS STYLE ZOOM TOWARD CURSOR
    // ---------------------------------------------------------
    void HandleZoom()
    {
        if (Mouse.current == null) return;

        float scroll = Mouse.current.scroll.ReadValue().y;
        if (Mathf.Abs(scroll) < 0.01f) return;

        float currentZoom = mapRoot.localScale.x;
        targetZoom = Mathf.Clamp(currentZoom + scroll * zoomSpeed, minZoom, maxZoom);

        float newScale = Mathf.Lerp(mapRoot.localScale.x, targetZoom, Time.deltaTime * zoomLerpSpeed);
        mapRoot.localScale = new Vector3(newScale, newScale, 1f);

        inertiaVelocity = Vector2.zero; // optional: kill inertia on zoom
    }


    // ---------------------------------------------------------
    // 2. MIDDLE MOUSE DRAG PANNING + INERTIA
    // ---------------------------------------------------------
    void HandlePan()
    {
        if (Mouse.current == null) return;

        Vector2 mousePos = Mouse.current.position.ReadValue();

        if (Mouse.current.middleButton.wasPressedThisFrame)
        {
            lastMousePos = mousePos;
            inertiaVelocity = Vector2.zero; // reset inertia
        }

        if (Mouse.current.middleButton.isPressed)
        {
            Vector2 delta = mousePos - lastMousePos;
            lastMousePos = mousePos;

            Vector2 movement = delta * panSpeed;
            mapRoot.anchoredPosition += movement;

            inertiaVelocity = movement; // store for inertia
        }
    }

    // ---------------------------------------------------------
    // 3. WASD + ARROW KEY PANNING (REVERSED)
    // ---------------------------------------------------------
    void HandleKeyboardPan()
    {
        if (Keyboard.current == null) return;

        Vector2 move = Vector2.zero;

        // Reverse movement: pressing W moves map DOWN (camera moves up)
        if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed)
            move.y -= 1;

        if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed)
            move.y += 1;

        if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
            move.x += 1;

        if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
            move.x -= 1;

        if (move != Vector2.zero)
        {
            Vector2 movement = move * keyPanSpeed * Time.deltaTime;
            mapRoot.anchoredPosition += movement;

            inertiaVelocity = movement; // inertia for keyboard too
        }
    }

    // ---------------------------------------------------------
    // 4. INERTIA / MOMENTUM
    // ---------------------------------------------------------
    void ApplyInertia()
    {
        if (inertiaVelocity.magnitude > 0.01f)
        {
            mapRoot.anchoredPosition += inertiaVelocity;
            inertiaVelocity = Vector2.Lerp(inertiaVelocity, Vector2.zero, Time.deltaTime * inertiaDamping);
        }
    }
}
