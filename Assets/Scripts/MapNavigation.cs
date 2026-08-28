using UnityEngine;
using UnityEngine.InputSystem;

public class MapNavigation : MonoBehaviour
{
    public RectTransform map;
    public RectTransform mapBounds;
    public MapResolutionScaler scaler;

    [Header("Zoom")]
    public float zoomSpeed;
    public float minZoom;
    public float maxZoom;
    public float zoomLerpSpeed;

    private float targetZoom;

    [Header("Pan")]
    public float panSpeed;
    public float keyPanSpeed;
    public float inertiaDamping;

    private float keyPanning;

    private Vector2 lastMousePos;
    private Vector2 inertiaVelocity;

    private bool hasZoomFocus;
    private Vector2 zoomFocusCanvas;
    private Vector2 zoomFocusMap;

    // ---------------------------------------------------------
    // NEW: Resolution‑normalized base scale
    // ---------------------------------------------------------
    // NEW: resolution‑normalized zoom limits

    public void updateScale() { 
        targetZoom = scaler.mapScale;
        keyPanning = keyPanSpeed * scaler.mapScale * 2;
    }

    // ---------------------------------------------------------
    // UPDATE LOOP
    // ---------------------------------------------------------
    void Update()
    {
        HandlePan();
        HandleKeyboardPan();
        HandleZoom();
        ApplyInertia();
        ClampMapPosition();
    }

    // ---------------------------------------------------------
    // ZOOM
    // ---------------------------------------------------------
    void HandleZoom()
    {
        float scroll = Mouse.current != null ? Mouse.current.scroll.ReadValue().y : 0f;
        float currentZoom = map.localScale.x;

        if (Mouse.current == null) return;

        if (Mathf.Abs(scroll) >= 0.01f ||
            (!hasZoomFocus && Mathf.Abs(targetZoom - currentZoom) >= 0.0001f))
        {
            RectTransform canvasRect = map.parent as RectTransform;
            if (canvasRect == null) return;

            Canvas canvas = canvasRect.GetComponentInParent<Canvas>();
            Camera eventCamera = canvas != null && canvas.renderMode != RenderMode.ScreenSpaceOverlay
                ? canvas.worldCamera
                : null;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvasRect,
                Mouse.current.position.ReadValue(),
                eventCamera,
                out zoomFocusCanvas
            );

            zoomFocusMap = (zoomFocusCanvas - map.anchoredPosition) / currentZoom;
            hasZoomFocus = true;

            if (Mathf.Abs(scroll) >= 0.01f)
            {
                float zoomSteps = Mathf.Sign(scroll) * Mathf.Max(1f, Mathf.Abs(scroll) / 120f);
                float zoomMultiplier = zoomSpeed > 1f ? zoomSpeed : 1.1f;
                targetZoom = Mathf.Clamp(
                    currentZoom * Mathf.Pow(zoomMultiplier, zoomSteps),
                    scaler.mapScale * minZoom,
                    scaler.mapScale * maxZoom
                );
            }
        }

        if (!hasZoomFocus || Mathf.Abs(targetZoom - currentZoom) < 0.0001f)
            return;

        float newScale = Mathf.Lerp(currentZoom, targetZoom, Time.deltaTime * zoomLerpSpeed);

        map.anchoredPosition += zoomFocusMap * (currentZoom - newScale);
        map.localScale = new Vector3(newScale, newScale, 1f);

        if (Mathf.Abs(targetZoom - newScale) < 0.001f)
        {
            map.localScale = new Vector3(targetZoom, targetZoom, 1f);
            hasZoomFocus = false;
        }
    }

    // ---------------------------------------------------------
    // PAN
    // ---------------------------------------------------------
    void HandlePan()
    {
        if (Mouse.current == null) return;

        Vector2 mousePos = Mouse.current.position.ReadValue();

        if (Mouse.current.middleButton.wasPressedThisFrame)
        {
            lastMousePos = mousePos;
            inertiaVelocity = Vector2.zero;
        }

        if (Mouse.current.middleButton.isPressed)
        {
            Vector2 delta = mousePos - lastMousePos;
            lastMousePos = mousePos;

            Vector2 movement = delta * panSpeed;
            map.anchoredPosition += movement;

            inertiaVelocity = movement;
        }
    }

    // ---------------------------------------------------------
    // KEYBOARD PAN
    // ---------------------------------------------------------
    void HandleKeyboardPan()
    {
        if (Keyboard.current == null) return;

        Vector2 move = Vector2.zero;

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
            Vector2 movement = move * keyPanning * Time.deltaTime;
            map.anchoredPosition += movement;

            inertiaVelocity = movement;
        }
    }

    // ---------------------------------------------------------
    // INERTIA
    // ---------------------------------------------------------
    void ApplyInertia()
    {
        if (inertiaVelocity.magnitude > 0.01f)
        {
            map.anchoredPosition += inertiaVelocity;
            inertiaVelocity = Vector2.Lerp(inertiaVelocity, Vector2.zero, Time.deltaTime * inertiaDamping);
        }
    }

    // ---------------------------------------------------------
    // CLAMP
    // ---------------------------------------------------------
    void ClampMapPosition()
    {
        if (mapBounds == null) return;

        float scale = Mathf.Abs(map.localScale.x);
        float halfWidth = scale * mapBounds.rect.width * 0.5f;
        float halfHeight = scale * mapBounds.rect.height * 0.5f;

        map.anchoredPosition = new Vector2(
            Mathf.Clamp(map.anchoredPosition.x, -halfWidth, halfWidth),
            Mathf.Clamp(map.anchoredPosition.y, -halfHeight, halfHeight)
        );
    }
}
