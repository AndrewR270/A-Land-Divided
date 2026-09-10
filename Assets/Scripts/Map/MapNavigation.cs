using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

/*

    Controls appearance of the map: panning, zooming, inertia, clamping, fading.
    Adjusts position and scale of the map images on the Canvas.

*/

public class MapNavigation : MonoBehaviour
{
    public RectTransform Map;
    public RectTransform MapBorder;
    public RectTransform BaseMap;
    public ResolutionScaler Scaler;

    [Header("Zoom")]
    public float MIN_ZOOM;
    public float MAX_ZOOM;
    public float zoomSpeed;
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

    [Header("Layers")]
    public RawImage textLayer;
    public RawImage cityLayer;

    private Texture2D ColorMap;

    private void OnEnable()
    {
        ScenarioManager.ScenarioLoaded += OnScenarioLoaded;
    }

    private void OnDisable()
    {
        ScenarioManager.ScenarioLoaded -= OnScenarioLoaded;
    }

    public void OnScenarioLoaded(ScenarioData scenario)
    {
        ColorMap = scenario.colorMap;
    }

    public void updateScale() { 
        targetZoom = Scaler.MapScale;
        keyPanning = keyPanSpeed * Scaler.MapScale * 2;
    }

    void Update()
    {
        MousePan();
        KeyboardPan();
        Zoom();
        ApplyInertia();
        ClampPosition();
        FadeLayers();
        ClickMap();
    }

    /*

        Panning functions.
        These adjust the x and y coordinates of the map.

    */

    void MousePan()
    {
        if (Mouse.current == null) return;
        Vector2 mousePos = Mouse.current.position.ReadValue();

        if (Mouse.current.middleButton.wasPressedThisFrame) { 
            lastMousePos = mousePos; 
            inertiaVelocity = Vector2.zero; 
        }

        if (Mouse.current.middleButton.isPressed)
        {
            Vector2 delta = mousePos - lastMousePos;
            lastMousePos = mousePos;

            Vector2 movement = delta * panSpeed;
            Map.anchoredPosition += movement;

            inertiaVelocity = movement;
        }
    }

    void KeyboardPan()
    {
        if (Keyboard.current == null) return;

        Vector2 move = Vector2.zero;

        if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed) { move.y -= 1; }
        if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed) { move.y += 1; }
        if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) { move.x += 1; }
        if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) { move.x -= 1; }

        if (move != Vector2.zero)
        {
            Vector2 movement = move * keyPanning * Time.deltaTime;
            Map.anchoredPosition += movement;
            inertiaVelocity = movement;
        }
    }

    /*

        Zoom function.
        This increases map scale centering on the mouse position.

    */

    void Zoom()
    {
        if (Mouse.current == null) return;

        float scroll = Mouse.current != null ? Mouse.current.scroll.ReadValue().y : 0f;
        float currentZoom = Map.localScale.x;

        if (Mathf.Abs(scroll) >= 0.01f || (!hasZoomFocus && Mathf.Abs(targetZoom - currentZoom) >= 0.0001f))
        {
            RectTransform canvasRect = Map.parent as RectTransform;
            Canvas canvas = canvasRect.GetComponentInParent<Canvas>();
            Camera eventCamera = canvas.renderMode != RenderMode.ScreenSpaceOverlay ? canvas.worldCamera : null;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvasRect,
                Mouse.current.position.ReadValue(),
                eventCamera,
                out zoomFocusCanvas
            );

            zoomFocusMap = (zoomFocusCanvas - Map.anchoredPosition) / currentZoom;
            hasZoomFocus = true;

            if (Mathf.Abs(scroll) >= 0.01f)
            {
                float zoomSteps = Mathf.Sign(scroll) * Mathf.Max(1f, Mathf.Abs(scroll) / 120f);
                float zoomMultiplier = zoomSpeed > 1f ? zoomSpeed : 1.1f;
                targetZoom = Mathf.Clamp(
                    currentZoom * Mathf.Pow(zoomMultiplier, zoomSteps),
                    Scaler.MapScale * MIN_ZOOM,
                    Scaler.MapScale * MAX_ZOOM
                );
            }
        }

        if (!hasZoomFocus || Mathf.Abs(targetZoom - currentZoom) < 0.0001f) { return; }

        float newScale = Mathf.Lerp(currentZoom, targetZoom, Time.deltaTime * zoomLerpSpeed);
        Map.anchoredPosition += zoomFocusMap * (currentZoom - newScale);
        Map.localScale = new Vector3(newScale, newScale, 1f);

        if (Mathf.Abs(targetZoom - newScale) < 0.001f)
        {
            Map.localScale = new Vector3(targetZoom, targetZoom, 1f);
            hasZoomFocus = false;
        }
    }

    /*

        Style functions. Inertia makes map movement smoother, 
        clamp prevents moving off map, and fading adjusts layer opacity at zoom levels.

    */

    void ApplyInertia()
    {
        if (inertiaVelocity.magnitude > 0.01f)
        {
            Map.anchoredPosition += inertiaVelocity;
            inertiaVelocity = Vector2.Lerp(inertiaVelocity, Vector2.zero, Time.deltaTime * inertiaDamping);
        }
    }

    void ClampPosition()
    {
        float scale = Mathf.Abs(Map.localScale.x);
        float halfWidth = scale * MapBorder.rect.width * 0.5f;
        float halfHeight = scale * MapBorder.rect.height * 0.5f;

        Map.anchoredPosition = new Vector2(
            Mathf.Clamp(Map.anchoredPosition.x, -halfWidth, halfWidth),
            Mathf.Clamp(Map.anchoredPosition.y, -halfHeight, halfHeight)
        );
    }

    void FadeLayers()
    {
        float zoom = Map.localScale.x / Scaler.MapScale;
        Color c;

        // Fade Text Layer

        float textAlpha = 0f;

        if (zoom >= 0.4f && zoom < 4f) { textAlpha = Mathf.InverseLerp(0.4f, 4f, zoom); }
        else if (zoom >= 4f) { textAlpha = 1f; }

        c = textLayer.color;
        c.a = textAlpha;
        textLayer.color = c;

        // Fade City Layer

        float cityAlpha = 0f;

        if (zoom >= 4f && zoom < 8f) { cityAlpha = Mathf.InverseLerp(4f, 8f, zoom); }
        else if (zoom >= 8f) { cityAlpha = 1f; }

        c = cityLayer.color;
        c.a = cityAlpha;
        cityLayer.color = c;
    }

    /*

        Click function. This takes the map position and click,
        and finds the color of the corresponding point on the map colors texture.

    */

    void ClickMap()
    {
        if (Mouse.current == null) return;
        if (!Mouse.current.leftButton.wasPressedThisFrame) return;

        RectTransform mapRect = BaseMap;
        Canvas canvas = mapRect.GetComponentInParent<Canvas>();
        Camera eventCamera = canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : canvas.worldCamera;

        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            mapRect,
            Mouse.current.position.ReadValue(),
            eventCamera,
            out localPoint
        );

        // Convert local → UV
        Rect rect = mapRect.rect;
        float u = (localPoint.x - rect.x) / rect.width;
        float v = (localPoint.y - rect.y) / rect.height;

        // clicked outside map
        if (u < 0 || u > 1 || v < 0 || v > 1) { return; }

        // Convert UV → pixel
        int x = (int)(u * ColorMap.width);
        int y = (int)(v * ColorMap.height);

        Color32 clickedColor = ColorMap.GetPixel(x, y);

        if (clickedColor.r == 0 && clickedColor.g == 0 && clickedColor.b == 0) return;

        // Convert color → provinceID
        if (!MapColors.Colors.TryGetValue(clickedColor, out string provinceID)) { Debug.LogWarning("Unknown."); return; }
        else { Debug.Log($"Clicked Color {clickedColor} → ProvinceID {provinceID}"); }


        // Province p = ScenarioManager.Instance.GetProvinceByID(provinceID);
        // if (p == null)
        // {
        //     Debug.LogWarning("Province not found: " + provinceID);
        //     return;
        // }

        // UIManager.Instance.ShowProvince(p);
    }
}
