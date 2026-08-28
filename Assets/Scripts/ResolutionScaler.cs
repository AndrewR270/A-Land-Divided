using UnityEngine;
using System.Collections.Generic;

public class MapResolutionScaler : MonoBehaviour
{
    public MapNavigation navigator;
    
    public RectTransform canvasRect;
    public RectTransform mapBounds;
    public RectTransform map;
    public RectTransform back;

    public float mapScale;
    public float backScale;

    void OnRectTransformDimensionsChange()
    {
        ApplyBaseScale();
    }

    void Awake()
    {
        ApplyBaseScale();
    }

    void ApplyBaseScale()
    {
        float canvasHeight = canvasRect.rect.height;
        float mapHeight = mapBounds.rect.height;
        float backHeight = back.rect.height;

        mapScale = canvasHeight / mapHeight;
        backScale = canvasHeight / backHeight;

        map.localScale = new Vector3(mapScale, mapScale, 1f);
        back.localScale = new Vector3(backScale, backScale, 1f);

        navigator.updateScale();
    }
}
