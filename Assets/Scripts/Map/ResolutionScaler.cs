using UnityEngine;
using System.Collections.Generic;

/*

    Calculates height of viewport (canvas) and map (map border) and scales the map to fit the viewport.
    Also scales the background to fit the viewport.

*/

public class ResolutionScaler : MonoBehaviour
{
    public MapNavigation Navigator;
    
    public RectTransform Canvas;
    public RectTransform MapBorder;
    public RectTransform Map;
    public RectTransform Background;

    public float MapScale;
    public float BackgroundScale;

    void OnRectTransformDimensionsChange() { ApplyBaseScale(); }

   private void OnEnable()
    {
        ScenarioManager.ScenarioLoaded += OnScenarioLoaded;
    }

    private void OnDisable()
    {
        ScenarioManager.ScenarioLoaded -= OnScenarioLoaded;
    }

    private void OnScenarioLoaded(ScenarioData scenario)
    {
        // Now textures are loaded and SetNativeSize() has run
        ApplyBaseScale();
    }


    void ApplyBaseScale()
    {
        float canvasHeight = Canvas.rect.height;
        float mapHeight = MapBorder.rect.height;
        float backHeight = Background.rect.height;

        MapScale = canvasHeight / mapHeight;
        BackgroundScale = canvasHeight / backHeight;

        Map.localScale = new Vector3(MapScale, MapScale, 1f);
        Background.localScale = new Vector3(BackgroundScale, BackgroundScale, 1f);

        Navigator.updateScale();
    }
}
