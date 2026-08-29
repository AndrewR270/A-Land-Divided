using UnityEngine;
using UnityEngine.UI;

public class ScenarioManager : MonoBehaviour
{
    public ScenarioData[] scenarios;

    public RawImage baseMap;
    public RawImage highlightLayer;
    public RawImage cityLayer;
    public RawImage labelLayer;

    public Transform background;

    private GameObject activeBackground;
    public ScenarioData activeScenario;

    public void LoadScenario(int id)
    {
        activeScenario = scenarios[id];

        // Load map textures
        baseMap.texture = activeScenario.baseMapImage;
        highlightLayer.texture = activeScenario.highlightLayerImage;
        cityLayer.texture = activeScenario.cityLayerImage;
        labelLayer.texture = activeScenario.labelLayerImage;

        // Load background prefab
        if (activeBackground != null)
            Destroy(activeBackground);

        activeBackground = Instantiate(activeScenario.backgroundPrefab, background);

        // Notify other systems
        BroadcastScenarioLoaded();
    }

    void BroadcastScenarioLoaded()
    {
        // MapNavigation, UI, game logic, etc.
        SendMessage("OnScenarioLoaded", activeScenario, SendMessageOptions.DontRequireReceiver);
    }
}
