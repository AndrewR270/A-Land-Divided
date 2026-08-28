using UnityEngine;
using UnityEngine.InputSystem;

public class MapNavigation : MonoBehaviour
{
    public RectTransform mapRoot;
    public RectTransform mapBounds;

    [Header("Zoom")]
    public float zoomSpeed;
    public float minZoom;
    public float maxZoom;
    public float zoomLerpSpeed;

    private float targetZoom = 1f;

    [Header("Pan")]
    public float panSpeed = 1.0f;
    public float keyPanSpeed = 500f;
    public float inertiaDamping = 5f;

    private Vector2 lastMousePos;
    private Vector2 inertiaVelocity;

    private bool hasZoomFocus;
    private Vector2 zoomFocusCanvas;
    private Vector2 zoomFocusMap;

    // ---------------------------------------------------------
    // NEW: Resolution‑normalized base scale
    // ---------------------------------------------------------
    // NEW: resolution‑normalized zoom limits
    private float actualMinZoom;
    private float actualMaxZoom;

    void ApplyBaseScale()
    {
        if (mapRoot == null) return;

        RectTransform canvasRect = mapRoot.parent as RectTransform;
        if (canvasRect == null) return;

        float canvasHeight = canvasRect.rect.height;
        float mapHeight = mapBounds.rect.height;

        float baseScale = canvasHeight / mapHeight;

        mapRoot.localScale = new Vector3(baseScale, baseScale, 1f);

        // resolution‑normalized zoom limits
        actualMinZoom = baseScale * minZoom;
        actualMaxZoom = baseScale * maxZoom;

        targetZoom = baseScale;
    }

    void Awake()
    {
        ApplyBaseScale(); // ensure correct baseline before zoom/pan begins
    }

    void OnRectTransformDimensionsChange()
    {
        ApplyBaseScale(); // resolution changed → recompute baseline
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
        if (mapRoot == null) return;

        float scroll = Mouse.current != null ? Mouse.current.scroll.ReadValue().y : 0f;
        float currentZoom = mapRoot.localScale.x;

        if (Mouse.current == null) return;

        if (Mathf.Abs(scroll) >= 0.01f ||
            (!hasZoomFocus && Mathf.Abs(targetZoom - currentZoom) >= 0.0001f))
        {
            RectTransform canvasRect = mapRoot.parent as RectTransform;
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

            zoomFocusMap = (zoomFocusCanvas - mapRoot.anchoredPosition) / currentZoom;
            hasZoomFocus = true;

            if (Mathf.Abs(scroll) >= 0.01f)
            {
                float zoomSteps = Mathf.Sign(scroll) * Mathf.Max(1f, Mathf.Abs(scroll) / 120f);
                float zoomMultiplier = zoomSpeed > 1f ? zoomSpeed : 1.1f;
                targetZoom = Mathf.Clamp(
                    currentZoom * Mathf.Pow(zoomMultiplier, zoomSteps),
                    actualMinZoom,
                    actualMaxZoom
                );
            }
        }

        if (!hasZoomFocus || Mathf.Abs(targetZoom - currentZoom) < 0.0001f)
            return;

        float newScale = Mathf.Lerp(currentZoom, targetZoom, Time.deltaTime * zoomLerpSpeed);

        mapRoot.anchoredPosition += zoomFocusMap * (currentZoom - newScale);
        mapRoot.localScale = new Vector3(newScale, newScale, 1f);

        if (Mathf.Abs(targetZoom - newScale) < 0.001f)
        {
            mapRoot.localScale = new Vector3(targetZoom, targetZoom, 1f);
            hasZoomFocus = false;
        }
    }

    // ---------------------------------------------------------
    // PAN
    // ---------------------------------------------------------
    void HandlePan()
    {
        if (Mouse.current == null || mapRoot == null) return;

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
            mapRoot.anchoredPosition += movement;

            inertiaVelocity = movement;
        }
    }

    // ---------------------------------------------------------
    // KEYBOARD PAN
    // ---------------------------------------------------------
    void HandleKeyboardPan()
    {
        if (Keyboard.current == null || mapRoot == null) return;

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
            Vector2 movement = move * keyPanSpeed * Time.deltaTime;
            mapRoot.anchoredPosition += movement;

            inertiaVelocity = movement;
        }
    }

    // ---------------------------------------------------------
    // INERTIA
    // ---------------------------------------------------------
    void ApplyInertia()
    {
        if (mapRoot == null)
        {
            inertiaVelocity = Vector2.zero;
            return;
        }

        if (inertiaVelocity.magnitude > 0.01f)
        {
            mapRoot.anchoredPosition += inertiaVelocity;
            inertiaVelocity = Vector2.Lerp(inertiaVelocity, Vector2.zero, Time.deltaTime * inertiaDamping);
        }
    }

    // ---------------------------------------------------------
    // CLAMP
    // ---------------------------------------------------------
    void ClampMapPosition()
    {
        if (mapRoot == null || mapBounds == null)
            return;

        float scale = Mathf.Abs(mapRoot.localScale.x);
        float halfWidth = scale * mapBounds.rect.width * 0.5f;
        float halfHeight = scale * mapBounds.rect.height * 0.5f;

        mapRoot.anchoredPosition = new Vector2(
            Mathf.Clamp(mapRoot.anchoredPosition.x, -halfWidth, halfWidth),
            Mathf.Clamp(mapRoot.anchoredPosition.y, -halfHeight, halfHeight)
        );
    }
}
